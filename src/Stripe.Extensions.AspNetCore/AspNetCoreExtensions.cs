using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe.Extensions.DependencyInjection;
using Stripe.Extensions.Webhooks;

namespace Stripe.Extensions.AspNetCore;

public static class AspNetCoreExtensions
{
    public static IEndpointConventionBuilder MapStripeWebhooks(this IEndpointRouteBuilder endpoints,
        string path = "/stripe/webhooks", string webhookSecret = null!)
    {
        return endpoints.MapPost(path, async (context) =>
        {
            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
                .CreateLogger("Stripe.Extensions.AspNetCore");

            var (stripeEvent, error) = await context.Request.ValidateStripeWebhook();

            switch (error)
            {
                case StripeException stripeException:
                    logger.StripeErrorOccurred(stripeException);
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                case Exception:
                    logger.UnableToProcessEvent(error);
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
            }

            var eventProcessor = context.RequestServices.GetRequiredService<IWebhookEventProcessor>();
            await eventProcessor.ProcessWebhookAsync(new WebhookEventContext(stripeEvent, context.Request.Headers));
            context.Response.StatusCode = StatusCodes.Status200OK;
        });
    }

    public static Task<(Event stripeEvent, Exception error)> ValidateStripeWebhook(this HttpRequest request,
        bool throwOnApiVersionMismatch = false)
    {
        var stripeOptions = request.HttpContext.RequestServices.GetRequiredService<IOptions<StripeOptions>>();
        var webhookSecret = stripeOptions.Value.WebhookSecret;

        return request.ValidateStripeWebhook(webhookSecret, throwOnApiVersionMismatch);
    }

    public static async Task<(Event stripeEvent, Exception error)> ValidateStripeWebhook(this HttpRequest request,
        string webhookSecret, bool throwOnApiVersionMismatch = false)
    {
        try
        {
            using var reader = new StreamReader(request.Body);
            var json = await reader.ReadToEndAsync().ConfigureAwait(false);

            if (!request.Headers.TryGetValue(Constants.STRIPE_WEBHOOK_HEADER_NAME, out var signatureHeader))
                return (null, new Exception("The webhook secret was not present in the request"));

            EventUtility.ValidateSignature(json, signatureHeader, webhookSecret);
            var stripeEvent = EventUtility.ParseEvent(json, throwOnApiVersionMismatch);
            return (stripeEvent, null);
        }
        catch (Exception ex)
        {
            return (null, ex);
        }
    }
}