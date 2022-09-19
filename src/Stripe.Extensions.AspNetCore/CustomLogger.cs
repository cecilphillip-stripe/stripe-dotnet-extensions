using Microsoft.Extensions.Logging;

namespace Stripe.Extensions.AspNetCore;


public static class CustomLogger
{
    private static readonly Action<ILogger, Exception?> _unableToProcessEvent;
    private static readonly Action<ILogger,Exception?> _webhookSecretNotConfigured;
    private static readonly Action<ILogger,string,Exception?> _stripeError;

    static  CustomLogger()
    {
        //TODO: We should provide more detail in the StripeError log
        _stripeError = LoggerMessage.Define<string>(LogLevel.Error, new(1, nameof(StripeErrorOccurred)), "A Stripe error was thrown processing Stripe webhook event: {StripeErrorType}") ;
        _unableToProcessEvent = LoggerMessage.Define(LogLevel.Error, new(3, nameof(UnableToProcessEvent)), "An Exception was thrown processing Stripe webhook event");
        _webhookSecretNotConfigured = LoggerMessage.Define(LogLevel.Error, new(5, nameof(WebhookSecretNotConfigured)), "Webhook secret was not provided");
    }

    public static void StripeErrorOccurred(this ILogger logger, StripeException exception) => _stripeError(logger, exception.StripeError.Type, exception);

    public static  void UnableToProcessEvent(this ILogger logger, Exception exception) => _unableToProcessEvent(logger, exception);
    
    public static void WebhookSecretNotConfigured(this ILogger logger) => _webhookSecretNotConfigured(logger, null);

}