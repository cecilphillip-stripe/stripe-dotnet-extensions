using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Stripe.Extensions.DependencyInjection;

public interface IStripeClientBuilder : IHttpClientBuilder
{
    StripeOptions? Options { get; set; }
    Action<StripeOptions>? ConfigureOptionsAction { get; set; }

    StripeClient Build(IServiceProvider serviceProvider);
}

internal sealed class StripeClientBuilder(IHttpClientBuilder httpClientBuilder) : IStripeClientBuilder
{
    public string Name => httpClientBuilder.Name;
    public IServiceCollection Services => httpClientBuilder.Services;

    public StripeOptions? Options { get; set; }
    public Action<StripeOptions>? ConfigureOptionsAction { get; set; }

    public StripeClient Build(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope(); //IOptionsSnapshot requires scope
        
        var stripeOptions = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<StripeOptions>>().Get(Name);
        if(Options is not null)
        {
            stripeOptions = Options;
        }
        
        stripeOptions.ClientName = Name;

        ConfigureOptionsAction?.Invoke(stripeOptions);
        
        if (string.IsNullOrEmpty(stripeOptions.SecretKey))
        {
            throw new InvalidOperationException("SecretKey is required to make requests to Stripe API. " +
                                                "You can set it using Stripe:SecretKey configuration section or " +
                                                "by passing the value to .AddStripe(\"key\") call");
        }

        var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var systemHttpClient = new SystemNetHttpClient(
            httpClient: clientFactory.CreateClient(Name),
            maxNetworkRetries: stripeOptions.MaxNetworkRetries,
            appInfo: stripeOptions.AppInfo,
            enableTelemetry: stripeOptions.EnableTelemetry);

        return new StripeClient(apiKey: stripeOptions.SecretKey, httpClient: systemHttpClient);
    }
}

public static class StripeClientBuilderExtensions
{
    public static IStripeClientBuilder WithOptions(this IStripeClientBuilder builder, StripeOptions options)
    {
        builder.Options = options;
        return builder;
    }

    public static IStripeClientBuilder WithOptions(this IStripeClientBuilder builder, Action<StripeOptions> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        builder.ConfigureOptionsAction += action;

        return builder;
    }
}