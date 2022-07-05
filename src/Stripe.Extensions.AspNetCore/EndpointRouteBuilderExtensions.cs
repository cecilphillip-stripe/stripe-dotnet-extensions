using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe.Extensions.DependencyInjection;
using Stripe.Extensions.Webhooks;

namespace Stripe.Extensions.AspNetCore;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointConventionBuilder MapStripeWebhooks(this IEndpointRouteBuilder endpoints,
        string path = "/stripe/webhooks", string webhookSecret = null!)
    {
        return endpoints.MapPost(path, async (context) =>
        {
            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
                .CreateLogger("Stripe.Extensions.AspNetCore");
            
            try
            {
                using var reader = new StreamReader(context.Request.Body);
                var json = await reader.ReadToEndAsync().ConfigureAwait(false);
                var signatureHeader = context.Request.Headers["Stripe-Signature"];

                if (string.IsNullOrEmpty(webhookSecret))
                {
                    var stripeOptions = context.RequestServices.GetRequiredService<IOptions<StripeOptions>>();
                    webhookSecret = stripeOptions.Value.WebhookSecret;
                }

                var stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, webhookSecret);
                
                var eventProcessor = context.RequestServices.GetRequiredService<IWebhookEventProcessor>();
                await eventProcessor.ProcessWebhookAsync(new WebhookEventContext(stripeEvent, context.Request.Headers));
                context.Response.StatusCode = StatusCodes.Status200OK;
            }
            catch (StripeException ex)
            {
                CustomLogger.LogStripeError(logger,ex);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            catch (Exception ex)
            {
                CustomLogger.LogUnableToProcessEvent(logger,ex);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        });
    }
}