using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Stripe.Extensions.Webhooks;

namespace Stripe.Extensions.AspNetCore;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointConventionBuilder MapStripeWebhooks(this IEndpointRouteBuilder endpoints, string path = "/stripe/webhooks",
        string webhookSecret = null!)
    {
        return endpoints.MapPost(path, async (context) =>
        {
            try
            {
                using var reader = new StreamReader(context.Request.Body);
                var json = await reader.ReadToEndAsync().ConfigureAwait(false);
                var signatureHeader = context.Request.Headers["Stripe-Signature"];
                var stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, webhookSecret);
                
                var eventProcessor = context.RequestServices.GetRequiredService<IWebhookEventProcessor>();
                await eventProcessor.ProcessWebhookAsync(new(stripeEvent, context.Request.Headers));
                context.Response.StatusCode = StatusCodes.Status200OK;
            }
            catch (StripeException e)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        });
    }
}