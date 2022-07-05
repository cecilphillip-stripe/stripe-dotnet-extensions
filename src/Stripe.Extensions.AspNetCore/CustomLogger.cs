using Microsoft.Extensions.Logging;

namespace Stripe.Extensions.AspNetCore;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal static partial class CustomLogger
{
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Error,
        Message = "")]
    public static partial void LogStripeError(ILogger logger, StripeException exception);

    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Error,
        Message = "Exception processing Stripe webhook event.")]
    public static partial void LogUnableToProcessEvent(ILogger logger, Exception exception);
}