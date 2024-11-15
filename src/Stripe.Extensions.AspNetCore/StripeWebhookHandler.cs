using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Stripe.Extensions.AspNetCore;

public abstract partial class StripeWebhookHandler(StripeWebhookContext context)
{
    protected ILogger? Logger { get; init; }

    protected StripeWebhookContext Context { get; } = context;

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

            Logger?.WebhookSecretValidationFailed("Webhook Secret Validation Failed!", ex);
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
            Logger?.EventParsingError( e);
            response.StatusCode = 400;
            return;
        }

        try
        {
            await ExecuteAsync(stripeEvent).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Logger?.ExecutionError( stripeEvent.Type, e);
            response.StatusCode = 500;
        }
    }

    private Task UnhandledEventAsync(Event e,
        [CallerMemberName] string? handlerMethod = null)
    {
        Logger?.UnhandledEvent( e.Type, handlerMethod ?? "<unknown>", null);
        return Task.CompletedTask;
    }

    protected virtual Task UnknownEventAsync(Event e)
    {
       Logger?.UnknownEvent( e.Type, null);
        return Task.CompletedTask;
    }
}

internal static partial class StripeWebhookHandlerLogger
{
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Warning,
        Message = "Exception occured while parsing the Stripe WebHook event payload.")]
    public static partial void EventParsingError(this ILogger logger, Exception ex);
    
    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Warning,
        Message =  "Exception occured while executing event handler for {EventType}")]
    public static partial void ExecutionError(this ILogger logger, string eventType, Exception? ex);
    
    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Warning,
        Message =  "Event type {EventType} is not supported by this version of the library, consider upgrading." +
                   "You can override the UnknownEventAsync method to suppress this log message."
        )]
    public static partial void UnknownEvent(this ILogger logger, string eventType, Exception? ex);
    
    [LoggerMessage(
        Level = LogLevel.Warning,
        EventId = 4,
        Message = "Event type {EventType} does not have a handler. Override the {MethodName} method to handle the event.")]
    public static partial void UnhandledEvent(this ILogger logger, string eventType, string methodName, Exception? ex);
    
    [LoggerMessage(
        EventId = 5,
        Level = LogLevel.Error,
    Message = "Webhook secret validation failed for event type {EventType}.")]
    public static partial void WebhookSecretValidationFailed(this ILogger logger, string eventType, Exception ex);
   
}
