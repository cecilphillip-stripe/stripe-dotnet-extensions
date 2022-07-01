using System.Reflection;
using Microsoft.Extensions.Configuration;
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
                StripeConfiguration.ApiKey = stripeConfigSection["SecretKey"];

                configureOptions(provider, options);
            });

        var asm = Assembly.GetExecutingAssembly().GetName();
        appInfo = appInfo ?? new AppInfo
        {
            Name = asm.Name,
            Version = asm.Version?.ToString()
        };
        StripeConfiguration.AppInfo = appInfo;

        services.AddHttpClient("Stripe");
        services.AddTransient<IStripeClient, StripeClient>(s =>
        {
            var clientFactory = s.GetRequiredService<IHttpClientFactory>();
            var httpClient = new SystemNetHttpClient(
                httpClient: clientFactory.CreateClient("Stripe"),
                maxNetworkRetries: StripeConfiguration.MaxNetworkRetries,
                appInfo: appInfo,
                enableTelemetry: StripeConfiguration.EnableTelemetry);

            return new StripeClient(apiKey: StripeConfiguration.ApiKey, httpClient: httpClient);
        });
        return services;
    }
}