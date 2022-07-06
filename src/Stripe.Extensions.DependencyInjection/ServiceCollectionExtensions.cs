using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    private const string HttpClientName = "Stripe";

    public static IServiceCollection AddStripe(this IServiceCollection services)
        => services.AddStripe(options => { });

    public static IServiceCollection AddStripe(this IServiceCollection services, string apiKey)
        => services.AddStripe(options => options.SecretKey = apiKey);

    public static IServiceCollection AddStripe(this IServiceCollection services, Action<StripeOptions> configureOptions)
        => services.AddStripe((options, _) => configureOptions(options));

    public static IServiceCollection AddStripe(this IServiceCollection services, IConfiguration config)
    {
        if (config == null) throw new ArgumentNullException(nameof(config));
        return services.AddStripe(config.Bind);
    }

    public static IServiceCollection AddStripe(this IServiceCollection services,
        Action<StripeOptions, IServiceProvider> configureOptions, AppInfo? appInfo = default)
    {
        services.AddOptions<StripeOptions>()
            .Configure(options =>
            {
                var asm = Assembly.GetExecutingAssembly().GetName();
                options.AppInfo ??= new AppInfo
                {
                    Name = asm.Name,
                    Version = asm.Version?.ToString()
                };
            })
            .Configure<IServiceProvider>((options, provider) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                configuration?.GetSection(StripeOptions.ConfigurationSectionName).Bind(options);
            })
            .Configure(configureOptions);

        services.AddHttpClient(HttpClientName);
        services.AddSingleton<IStripeClient, StripeClient>(s =>
        {
            var stripeOptions = s.GetRequiredService<IOptions<StripeOptions>>().Value;
            var clientFactory = s.GetRequiredService<IHttpClientFactory>();
            var systemHttpClient = new SystemNetHttpClient(
                httpClient: clientFactory.CreateClient(HttpClientName),
                maxNetworkRetries: stripeOptions.MaxNetworkRetries,
                appInfo: appInfo,
                enableTelemetry: stripeOptions.EnableTelemetry);

            return new StripeClient(apiKey: stripeOptions.SecretKey, httpClient: systemHttpClient);
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
                    collection.TryAdd(ServiceDescriptor.Singleton(type, type));
                    return;
                }
            }
        }
    }
}