using System.Reflection;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Extensions.DependencyInjection;
using static Stripe.Extensions.DependencyInjection.StripeOptions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static partial class StripeServiceCollectionExtensions
{
    public static IStripeClientBuilder AddStripe(this IServiceCollection services,
        string clientName = DefaultClientConfigurationSectionName)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        if (clientName is null)
            throw new ArgumentNullException(nameof(clientName));

        // Register the named stripe client with configuration section
        services.AddOptions<StripeOptions>(clientName)
            .Configure(ConfigureStripeOptions)
            .Configure<IServiceProvider>((options, provider) =>
                BindOptionsConfiguration(clientName, options, provider));
        
        AddStripeServiceProvider(services);
        return services.RegisterClientServices(clientName);
        
        static void ConfigureStripeOptions(StripeOptions options)
        {
            var asm = Assembly.GetAssembly(typeof(StripeOptions)).GetName();
            options.AppInfo ??= new AppInfo
            {
                Name = asm.Name,
                Version = asm.Version?.ToString()
            };
        }

        static void BindOptionsConfiguration(string clientName, StripeOptions options, IServiceProvider provider)
        {
            var configuration = provider.GetService<IConfiguration>();
            var configSection = configuration?.GetSection(clientName);

            if (configSection == null) return;
            configSection.Bind(options);
        }
    }
    
    private static IStripeClientBuilder RegisterClientServices(this IServiceCollection services, string clientName)
    {
        var httpClientBuilder = services.AddHttpClient(clientName);
        var stripeClientBuilder=  new StripeClientBuilder(httpClientBuilder);
      
        services.AddKeyedScoped<IStripeClient, StripeClient>(clientName, (serviceProvider, _) => stripeClientBuilder.Build(serviceProvider));      
        
        if (clientName == DefaultClientConfigurationSectionName)
        {
            services.AddScoped<IStripeClient>(serviceProvider => serviceProvider.GetRequiredKeyedService<IStripeClient>(clientName));
        }
        
        return stripeClientBuilder;
    }

    private static IServiceCollection AddStripeServiceProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IStripeServiceProvider, StripeServiceProvider>();
        return serviceCollection;
    }
}