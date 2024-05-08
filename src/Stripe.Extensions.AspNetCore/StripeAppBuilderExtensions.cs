using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Stripe.Extensions.DependencyInjection;

namespace Stripe.Extensions.AspNetCore;

public static class StripeAppBuilderExtensions
{
    public static IEndpointRouteBuilder MapStripeWebhookHandler<T>(this IEndpointRouteBuilder endpointRouteBuilder)
        where T : StripeWebhookHandler
    {
        return endpointRouteBuilder.MapStripeWebhookHandler<T>("/stripe/webhook");
    }

    public static IEndpointRouteBuilder MapStripeWebhookHandler<T>(this IEndpointRouteBuilder endpointRouteBuilder, string pattern)
        where T: StripeWebhookHandler
    {
        var options = endpointRouteBuilder.ServiceProvider.GetService<IOptions<StripeOptions>>();
        if (options == null)
        {
            throw new InvalidOperationException("Stripe services were not registered. Please call services.AddStripe()");
        }

        var handlerFactory = ActivatorUtilities.CreateFactory(typeof(T), new[]{typeof(StripeWebhookContext)});
        endpointRouteBuilder.MapPost(pattern, async context =>
        {
            var stripeWebhookContext = new StripeWebhookContext(context, options.Value);
            var handler = (T)handlerFactory(context.RequestServices, new object?[]{stripeWebhookContext} );
            await handler.ExecuteAsync();
        });

        return endpointRouteBuilder;
    }
}
