using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Stripe.AspNetCore;
using Stripe.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Routing;

public static class StripeAppBuilderExtensions
{
    public static IEndpointRouteBuilder MapStripeWebhookHandler<T>(this IEndpointRouteBuilder endpointRouteBuilder)
        where T : StripeWebhookHandler
    {
        return endpointRouteBuilder.MapStripeWebhookHandler<T>("/webhook");
    }
    
    public static IEndpointRouteBuilder MapStripeWebhookHandler<T>(this IEndpointRouteBuilder endpointRouteBuilder, string pattern)
        where T: StripeWebhookHandler
    {
        var options = endpointRouteBuilder.ServiceProvider.GetService<IOptions<StripeOptions>>();
        if (options == null)
        {
            throw new InvalidOperationException("Stripe services were not registered. Please call services.AddStripe()");
        }

        var handlerFactory = ActivatorUtilities.CreateFactory(typeof(T), Array.Empty<Type>());
        endpointRouteBuilder.Map(pattern, async context =>
        {
            var handler = (T)handlerFactory(context.RequestServices, Array.Empty<object>());
            handler.Context = new StripeWebhookContext(context, options.Value);
            await handler.ExecuteAsync();
        });

        return endpointRouteBuilder;
    }
}