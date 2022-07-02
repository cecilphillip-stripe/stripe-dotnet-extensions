using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStripe(this IServiceCollection services, Action<StripeOptions> configureOptions)
        => services.AddStripe((_, options) => configureOptions(options));

    public static IServiceCollection AddStripe(this IServiceCollection services, IConfiguration config)
    {
        if (config == null) throw new ArgumentNullException(nameof(config));
        return services.AddStripe(config.Bind);
    }

    public static IServiceCollection AddStripe(this IServiceCollection services,
        Action<IServiceProvider, StripeOptions> configureOptions, AppInfo? appInfo = default)
    {
        services.AddOptions<StripeOptions>()
            .Configure<IServiceProvider>((options, provider) =>
            {
                var stripeConfigSection = provider.GetRequiredService<IConfiguration>()
                    .GetSection(StripeOptions.ConfigurationSectionName);

                configureOptions(provider, options);
                
                StripeConfiguration.ApiKey = options.SecretKey;
                StripeConfiguration.EnableTelemetry = options.EnableTelemetry;
                StripeConfiguration.MaxNetworkRetries = options.MaxNetworkRetries;
            });

        var asm = Assembly.GetExecutingAssembly().GetName();
        appInfo ??= new AppInfo
        {
            Name = asm.Name,
            Version = asm.Version?.ToString()
        };
        StripeConfiguration.AppInfo = appInfo;

        services.AddHttpClient("Stripe");
        services.AddTransient<IStripeClient, StripeClient>(s =>
        {
            var stripeOptions = s.GetRequiredService<IOptions<StripeOptions>>();
            var clientFactory = s.GetRequiredService<IHttpClientFactory>();
            var systemHttpClient = new SystemNetHttpClient(
                httpClient: clientFactory.CreateClient("Stripe"),
                maxNetworkRetries: stripeOptions.Value.MaxNetworkRetries,
                appInfo: appInfo,
                enableTelemetry: stripeOptions.Value.EnableTelemetry);

            return new StripeClient(apiKey: stripeOptions.Value.SecretKey, httpClient: systemHttpClient);
        });

        RegisterStripeServices(services);
        return services;
    }

    private static void RegisterStripeServices(IServiceCollection collection)
    {
        var stripeServiceTypes = typeof(StripeClient).Assembly.DefinedTypes
            .Select(info => info.AsType())
            .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic &&
                        t.Name.EndsWith("Service", StringComparison.Ordinal));

        foreach (var type in stripeServiceTypes)
            TryAddType(collection, type);
    }

    private static void TryAddType(IServiceCollection collection, Type type)
    {
        var constructorInfo = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
        foreach (var constructor in constructorInfo)
        {
            var parameters = constructor.GetParameters();
            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType == typeof(IStripeClient))
                {
                    collection.TryAdd(ServiceDescriptor.Transient(type, type));
                    return;
                }
            }
        }
    }
}