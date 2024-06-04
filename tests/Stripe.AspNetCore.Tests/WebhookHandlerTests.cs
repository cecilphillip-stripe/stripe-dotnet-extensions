using System.Net;
using System.Security.Cryptography;
using System.Text;
using MELT;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stripe.Extensions.AspNetCore;
using Stripe.Extensions.DependencyInjection;
using Xunit;

namespace Stripe.AspNetCore.Tests;

public class WebhookHandlerTests
{
    private readonly string _secret = "secret_key";
    private readonly List<Event> _invocations = new();

    public IWebHostBuilder BuildHostBuilder(string clientName = StripeOptions.DefaultClientConfigurationSectionName, Action<StripeOptions>? configureOptions = null)
    {
        return new WebHostBuilder()
            .ConfigureServices(s => s
                .AddStripe(clientName, configureOptions)
                .Services
                .AddRouting()
                .AddLogging(l => l.AddTest())
                .AddSingleton(_invocations)
            )
            .Configure(app =>
                app.UseRouting().UseEndpoints(b => b.MapStripeWebhookHandler<MockHandler>()));
    }

    [Fact]
    public async Task LogsFailedEventParsing()
    {
        using var testServer = new TestServer(BuildHostBuilder(configureOptions: opts =>
        {
            opts.SecretKey = _secret;
            opts.WebhookSecret = _secret;
        }));
        
        var testSink = testServer.Host.Services.GetRequiredService<ITestLoggerSink>();
        using var httpClient = testServer.CreateClient();
        var response = await httpClient.PostAsync("/stripe/webhook", new StringContent("{}"));
        Assert.Equal((HttpStatusCode)400, response.StatusCode);

        Assert.Contains(testSink.LogEntries, e =>
            e.LogLevel == LogLevel.Warning &&
            e.Message == "Exception occured while parsing the Stripe WebHook event payload.");
    }

    [Fact]
    public async Task ThrowsUsefulErrorMessageIfWebhookSecretNotSet()
    {
        using (TestServer testServer = new TestServer(BuildHostBuilder(configureOptions: opts =>
               {
                   opts.SecretKey = _secret;
                   opts.WebhookSecret = null!;
               })))
        {
            using HttpClient httpClient = testServer.CreateClient();
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await httpClient.PostAsync("/stripe/webhook", BuildPayload()));
            Assert.Contains("WebhookSecret is required to validate events", exception.Message);
        }
    }

    [Fact]
    public async Task LogsUnregisteredEvent()
    {
        ITestLoggerSink testSink;

        using (TestServer testServer = new TestServer(BuildHostBuilder(configureOptions: opts =>
               {
                   opts.SecretKey = _secret;
                   opts.WebhookSecret = _secret;
               })))
        {
            testSink = testServer.Host.Services.GetRequiredService<ITestLoggerSink>();
            using HttpClient httpClient = testServer.CreateClient();
            var response = await httpClient.PostAsync("/stripe/webhook", BuildPayload("customer.deleted"));
            Assert.Equal((HttpStatusCode)200, response.StatusCode);
        }

        Assert.Contains(testSink.LogEntries, e =>
            e.LogLevel == LogLevel.Warning &&
            e.Message ==
            "Event type customer.deleted does not have a handler. Override the OnCustomerDeletedAsync method to handle the event.");
    }

    [Fact]
    public async Task LogsUnknownEvent()
    {
        ITestLoggerSink testSink;

        using (TestServer testServer = new TestServer(BuildHostBuilder(configureOptions: opts =>
               {
                   opts.SecretKey = _secret;
                   opts.WebhookSecret = _secret;
               })))
        {
            testSink = testServer.Host.Services.GetRequiredService<ITestLoggerSink>();
            using HttpClient httpClient = testServer.CreateClient();
            var response = await httpClient.PostAsync("/stripe/webhook", BuildPayload("customer.happy"));
            Assert.Equal((HttpStatusCode)200, response.StatusCode);
        }

        Assert.Contains(testSink.LogEntries, e =>
            e.LogLevel == LogLevel.Warning &&
            e.Message ==
            "Event type customer.happy is not supported by this version of the library, consider upgrading.You can override the UnknownEventAsync method to suppress this log message.");
    }

    [Fact]
    public async Task LogsWhenHandlerThrows()
    {
        ITestLoggerSink testSink;

        using (TestServer testServer = new TestServer(BuildHostBuilder(configureOptions: opts =>
               {
                   opts.SecretKey = _secret;
                   opts.WebhookSecret = _secret;
               })))
        {
            testSink = testServer.Host.Services.GetRequiredService<ITestLoggerSink>();
            using HttpClient httpClient = testServer.CreateClient();
            var response = await httpClient.PostAsync("/stripe/webhook", BuildPayload("customer.updated"));
            Assert.Equal((HttpStatusCode)500, response.StatusCode);
        }

        Assert.Contains(testSink.LogEntries, e =>
            e.LogLevel == LogLevel.Warning &&
            e.Exception != null &&
            e.Message == "Exception occured while executing event handler for customer.updated");
    }

    [Fact]
    public async Task RunsEventCallback()
    {
        ITestLoggerSink testSink;

        using (TestServer testServer = new TestServer(BuildHostBuilder(configureOptions: opts =>
               {
                   opts.SecretKey = _secret;
                   opts.WebhookSecret = _secret;
               })))
        {
            testSink = testServer.Host.Services.GetRequiredService<ITestLoggerSink>();
            using HttpClient httpClient = testServer.CreateClient();
            var response = await httpClient.PostAsync("/stripe/webhook", BuildPayload());
            Assert.Equal((HttpStatusCode)200, response.StatusCode);
        }

        Assert.Contains(_invocations, e => e.Type == "customer.created");
        Assert.DoesNotContain(testSink.LogEntries, e => e.LogLevel >= LogLevel.Warning);
    }

    [Fact]
    public async Task CanDisableApiVersionCheck()
    {
        ITestLoggerSink testSink;

        using (TestServer testServer = new TestServer(
                   BuildHostBuilder(configureOptions: options =>
                   {
                       options.SecretKey = _secret;
                       options.WebhookSecret = _secret;
                       options.ThrowOnWebhookApiVersionMismatch = false;
                   })))
        {
            testSink = testServer.Host.Services.GetRequiredService<ITestLoggerSink>();
            using HttpClient httpClient = testServer.CreateClient();
            var response = await httpClient.PostAsync("/stripe/webhook", BuildPayload(apiVersion: "01-02-1234"));
            Assert.Equal((HttpStatusCode)200, response.StatusCode);
        }

        Assert.Contains(_invocations, e => e.Type == "customer.created");
        Assert.DoesNotContain(testSink.LogEntries, e => e.LogLevel >= LogLevel.Warning);
    }

    [Fact]
    public async Task VersionCheckIsOnByDefault()
    {
        ITestLoggerSink testSink;

        using (TestServer testServer = new TestServer(BuildHostBuilder(configureOptions: options =>
               {
                   options.SecretKey = _secret;
                   options.WebhookSecret = _secret;
               })))
        {
            testSink = testServer.Host.Services.GetRequiredService<ITestLoggerSink>();
            using HttpClient httpClient = testServer.CreateClient();
            await httpClient.PostAsync("/stripe/webhook", BuildPayload(apiVersion: "01-02-1234"));
        }

        Assert.Contains(testSink.LogEntries, e =>
            e.LogLevel == LogLevel.Warning &&
            e.Exception?.Message.Contains("API version") == true &&
            e.Message == "Exception occured while parsing the Stripe WebHook event payload.");
    }

    private class MockHandler : StripeWebhookHandler
    {
        private readonly List<Event> _invocations;

        public MockHandler(List<Event> invocations, StripeWebhookContext stripeWebhookContext) : base(
            stripeWebhookContext)
        {
            _invocations = invocations;
        }

        public override Task OnCustomerCreatedAsync(Event e)
        {
            _invocations.Add(e);
            return Task.CompletedTask;
        }

        public override Task OnCustomerUpdatedAsync(Event e)
        {
            throw new Exception();
        }
    }

    private static readonly UTF8Encoding SafeUtf8
        = new(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

    private static string ComputeSignature(string secret, string timestamp, string payload)
    {
        var secretBytes = SafeUtf8.GetBytes(secret);
        var payloadBytes = SafeUtf8.GetBytes($"{timestamp}.{payload}");

        using (var cryptographer = new HMACSHA256(secretBytes))
        {
            var hash = cryptographer.ComputeHash(payloadBytes);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
        }
    }

    private StringContent BuildPayload(string eventType = "customer.created", string? apiVersion = null)
    {
        apiVersion ??= StripeConfiguration.ApiVersion;

        var payload = "{" +
                      "\"id\": \"evt_123\"," +
                      "\"object\": \"event\"," +
                      "\"account\": \"acct_123\"," +
                      $"\"api_version\": \"{apiVersion}\"," +
                      "\"created\": 1533204620," +
                      "\"data\": {" +
                      "\"object\": {" +
                      "\"id\": \"cus_123\"," +
                      "\"object\": \"customer\"," +
                      "\"created\": 123456789," +
                      "\"email\": \"test@stripe.com\"," +
                      "\"livemode\": false," +
                      "\"metadata\": {}" +
                      "}" +
                      "}," +
                      "\"livemode\": false," +
                      "\"pending_webhooks\": 1," +
                      "\"request\": {" +
                      "\"id\": \"req_123\"," +
                      "\"idempotency_key\": \"idempotency-key-123\"" +
                      "}," +
                      $"\"type\": \"{eventType}\"" +
                      "}";

        var eventTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var signature = $"t={eventTimestamp},v1={ComputeSignature(_secret, eventTimestamp, payload)}";

        return new StringContent(payload)
        {
            Headers = { { "Stripe-Signature", signature } },
        };
    }
}