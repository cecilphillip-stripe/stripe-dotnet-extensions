using Microsoft.Extensions.DependencyInjection;

namespace Stripe.Extensions.DependencyInjection;

internal class StripeServiceProvider(IServiceProvider serviceProvider) : IStripeServiceProvider
{
    public T? GetService<T>(string clientName = StripeOptions.DefaultClientConfigurationSectionName) where T : class
    {
        var baseType = typeof(T).BaseType;
        if (baseType is { IsGenericType: true })
        {
            var result = baseType.GetGenericTypeDefinition().IsAssignableFrom(typeof(Service<>));
            if (result)
            {
                try
                {
                    var stripeClient = serviceProvider.GetRequiredKeyedService<IStripeClient>(clientName);

                    var instance = ActivatorUtilities.CreateInstance<T>(serviceProvider, stripeClient);
                    return instance;
                }
                catch (InvalidOperationException ex) when (ex.Message.StartsWith("No service for type 'Stripe.IStripeClient'")) {}
            }
        }
        return null;
    }
}

public interface IStripeServiceProvider
{
    T? GetService<T>(string clientName = StripeOptions.DefaultClientConfigurationSectionName)
        where T : class;
}