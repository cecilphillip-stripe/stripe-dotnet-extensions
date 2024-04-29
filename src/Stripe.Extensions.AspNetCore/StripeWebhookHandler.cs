using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Stripe.Extensions.AspNetCore;

public abstract partial class StripeWebhookHandler
{
    private ILogger<StripeWebhookHandler>? _logger;

    private ILogger<StripeWebhookHandler> Logger =>
        _logger ??= Context.HttpContext.RequestServices
            .GetRequiredService<ILogger<StripeWebhookHandler>>();

    protected StripeWebhookContext Context { get; }

    protected StripeWebhookHandler(StripeWebhookContext context) => Context = context;

    public async Task ExecuteAsync()
    {
        var httpContext = Context.HttpContext;
        var response = httpContext.Response;

        var options = Context.StripeOptions;
        if (string.IsNullOrEmpty(options.WebhookSecret))
        {
            var ex = new InvalidOperationException(
                "WebhookSecret is required to validate events. " +
                "You can set it using Stripe:WebhookSecret configuration section or " +
                "by passing the value to .AddStripe(o => o.WebhookSecret = \"whse_123\") call");

            WebhookSecretValidationFailed(Logger, "Webhook Secret Validation Failed!",
                nameof(ExecuteAsync), ex);
            throw ex;
        }

        Event stripeEvent;
        try
        {
            using var stream = new StreamReader(httpContext.Request.Body);
            var request = httpContext.Request;
            var body = await stream.ReadToEndAsync();

            stripeEvent = EventUtility.ConstructEvent(
                body,
                request.Headers["Stripe-Signature"],
                options.WebhookSecret,
                300, // default tolerance
                options.ThrowOnWebhookApiVersionMismatch);
        }
        catch (Exception e)
        {
            EventParsingError(Logger, e);
            response.StatusCode = 400;
            return;
        }

        try
        {
            await ExecuteAsync(stripeEvent).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            ExecutionError(Logger, stripeEvent.Type, e);
            response.StatusCode = 500;
        }
    }

    private Task UnhandledEventAsync(Event e,
        [CallerMemberName] string? handlerMethod = null)
    {
        UnhandledEvent(Logger, e.Type, handlerMethod ?? "<unknown>", null);
        return Task.CompletedTask;
    }

    protected virtual Task UnknownEventAsync(Event e)
    {
        UnknownEvent(Logger, e.Type, null);
        return Task.CompletedTask;
    }

    private static Action<ILogger, Exception> EventParsingError = LoggerMessage.Define(
        LogLevel.Warning,
        1,
        "Exception occured while parsing the Stripe WebHook event payload.");

    private static Action<ILogger, string, Exception> ExecutionError = LoggerMessage.Define<string>(
        LogLevel.Warning,
        2,
        "Exception occured while executing event handler for {event_type}");

    private static Action<ILogger, string, Exception?> UnknownEvent = LoggerMessage.Define<string>(
        LogLevel.Warning,
        3,
        "Event type {event_type} is not supported by this version of the library, consider upgrading." +
        "You can override the UnknownEventAsync method to suppress this log message.");

    private static Action<ILogger, string, string, Exception?> UnhandledEvent =
        LoggerMessage.Define<string, string>(
            LogLevel.Warning,
            4,
            "Event type {event_type} does not have a handler. Override the {method_name} method to handle the event.");

    private static Action<ILogger, string, string, Exception?> WebhookSecretValidationFailed =
        LoggerMessage.Define<string, string>(
            LogLevel.Error,
            5,
            "Event type {event_type} does not have a handler. Override the {method_name} method to handle the event.");
}
