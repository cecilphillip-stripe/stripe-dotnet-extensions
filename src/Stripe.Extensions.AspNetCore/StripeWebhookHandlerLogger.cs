using Microsoft.Extensions.Logging;

namespace Stripe.Extensions.AspNetCore;

internal static partial class StripeWebhookHandlerLogger
{
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Error,
        Message = "Exception occured while parsing the Stripe WebHook event payload.")]
    public static partial void EventParsingError(this ILogger logger, Exception ex);

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Error,
        Message = "Exception occured while executing event handler for {EventType}")]
    public static partial void ExecutionError(this ILogger logger, string eventType, Exception? ex);

    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Warning,
        Message = "Event type {EventType} is not supported by this version of the library, consider upgrading." +
                  "You can override the UnknownEventAsync method to suppress this log message."
    )]
    public static partial void UnknownEvent(this ILogger logger, string eventType, Exception? ex);

    [LoggerMessage(
        Level = LogLevel.Warning,
        EventId = 4,
        Message =
            "Event type {EventType} does not have a handler. Override the {MethodName} method to handle the event.")]
    public static partial void UnhandledEvent(this ILogger logger, string eventType, string methodName, Exception? ex);

    [LoggerMessage(
        EventId = 5,
        Level = LogLevel.Error,
        Message = "Webhook secret validation failed for event type {EventType}.")]
    public static partial void WebhookSecretValidationFailed(this ILogger logger, string eventType, Exception ex);
}