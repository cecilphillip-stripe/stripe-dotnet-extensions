using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static partial class StripeServiceCollectionExtensions
{
    private const string HttpClientName = "Stripe";

    public static IStripeClientBuilder AddStripe(this IServiceCollection services)
        => services.AddStripe(_ => { });

    public static IStripeClientBuilder AddStripe(this IServiceCollection services, string secretKey)
        => services.AddStripe(options => options.SecretKey = secretKey);

    public static IStripeClientBuilder AddStripe(this IServiceCollection services, Action<StripeOptions> configureOptions)
        => services.AddStripe((options, _) => configureOptions(options));

    public static IStripeClientBuilder AddStripe(this IServiceCollection services, IConfiguration config)
    {
        if (config == null) throw new ArgumentNullException(nameof(config));
        return services.AddStripe(config.Bind);
    }

    public static IStripeClientBuilder AddStripe(this IServiceCollection services,
        Action<StripeOptions, IServiceProvider> configureOptions)
    {
        services.AddOptions<StripeOptions>()
            .Configure(options =>
            {
                var asm = Assembly.GetAssembly(typeof(StripeOptions)).GetName();
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

        var clientBuilder = services.AddHttpClient(HttpClientName);
        
        services.AddSingleton<IStripeClient, StripeClient>(s =>
        {
            var stripeOptions = s.GetRequiredService<IOptions<StripeOptions>>().Value;
            if (string.IsNullOrEmpty(stripeOptions.SecretKey))
            {
                throw new InvalidOperationException("SecretKey is required to make requests to Stripe API. " +
                                                    "You can set it using Stripe:SecretKey configuration section or " +
                                                    "by passing the value to .AddStripe(\"key\") call");
            }
            var clientFactory = s.GetRequiredService<IHttpClientFactory>();
            var systemHttpClient = new SystemNetHttpClient(
                httpClient: clientFactory.CreateClient(HttpClientName),
                maxNetworkRetries: stripeOptions.MaxNetworkRetries,
                appInfo: stripeOptions.AppInfo,
                enableTelemetry: stripeOptions.EnableTelemetry);

            return new StripeClient(apiKey: stripeOptions.SecretKey, httpClient: systemHttpClient);
        });

        RegisterStripeServices(services);
        return new StripeClientBuilder(clientBuilder);
    }
}

internal sealed class StripeClientBuilder(IHttpClientBuilder httpClientBuilder) : IStripeClientBuilder
{
    public string Name => httpClientBuilder.Name;
    public IServiceCollection Services => httpClientBuilder.Services;
}

public interface IStripeClientBuilder: IHttpClientBuilder { }