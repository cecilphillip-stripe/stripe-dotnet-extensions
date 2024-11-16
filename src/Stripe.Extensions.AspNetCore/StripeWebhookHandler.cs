using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Stripe.Extensions.AspNetCore;

public abstract partial class StripeWebhookHandler(StripeWebhookContext context, ILogger logger)
{
    protected StripeWebhookContext Context { get; } = context;
    
    public async Task<IResult> ExecuteAsync()
    {
        var httpContext = Context.HttpContext;
        var response = httpContext.Response;
        
        Event stripeEvent;
        try
        {
            var options = Context.StripeOptions;
            if (string.IsNullOrEmpty(options.WebhookSecret))
            {
                var ex = new InvalidOperationException(
                    "WebhookSecret is required to validate events. " +
                    "You can set it using Stripe:WebhookSecret configuration section or " +
                    "by passing the value to .AddStripe(o => o.WebhookSecret = \"your_secret\") call");

                logger.WebhookSecretValidationFailed("Webhook Secret Validation Failed!", ex);
                throw ex;
            }
            
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
            logger.EventParsingError(e);
            response.StatusCode = 400;
            return Results.BadRequest();
        }

        try
        {
            await ExecuteAsync(stripeEvent).ConfigureAwait(false);
            return Results.Accepted();
        }
        catch (Exception e)
        {
            logger.ExecutionError(stripeEvent.Type, e);
            response.StatusCode = 500;
            return Results.BadRequest();
        }
    }

    private Task UnhandledEventAsync(Event e,
        [CallerMemberName] string? handlerMethod = null)
    {
        logger.UnhandledEvent(e.Type, handlerMethod ?? "<unknown>", null);
        return Task.CompletedTask;
    }

    protected virtual Task UnknownEventAsync(Event e)
    {
        logger.UnknownEvent(e.Type, null);
        return Task.CompletedTask;
    }
}