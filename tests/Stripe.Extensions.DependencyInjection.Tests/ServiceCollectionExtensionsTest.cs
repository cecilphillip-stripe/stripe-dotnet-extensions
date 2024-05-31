using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Stripe.Extensions.DependencyInjection.Tests;

public class ServiceCollectionExtensionsTest
{
    [Fact]
    public void CanResolveStripeServicesFromStripeServiceProvider()
    {
        var collection = new ServiceCollection();

        collection.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                { "Stripe:SecretKey", "MyKey" }
            }).Build());

        collection.AddStripe();

        var provider = collection.BuildServiceProvider();
        var stripeServiceProvider = provider.GetRequiredService<IStripeServiceProvider>();
        Assert.NotNull(stripeServiceProvider);

        Assert.NotNull(stripeServiceProvider.GetService<ProductService>());
        Assert.NotNull(stripeServiceProvider.GetService<AccountService>());
        Assert.NotNull(stripeServiceProvider.GetService<CouponService>());
    }
    
    [Fact]
    public void CanResolveStripeServicesFromStripeServiceProviderWithNamedClient()
    {
        const string clientOneKey = "ClientOne";
        const string otherStripe = "OtherStripe";

        var collection = new ServiceCollection();

        collection.AddStripe(clientOneKey).WithOptions(options => options.SecretKey = clientOneKey);
        collection.AddStripe(otherStripe).WithOptions(options => options.SecretKey = otherStripe);

        var provider = collection.BuildServiceProvider();
        var stripeServiceProvider = provider.GetRequiredService<IStripeServiceProvider>();
        Assert.NotNull(stripeServiceProvider);

        Assert.NotNull(stripeServiceProvider.GetService<ProductService>(clientOneKey));
        Assert.NotNull(stripeServiceProvider.GetService<AccountService>(otherStripe));
        Assert.Null(stripeServiceProvider.GetService<CouponService>());
    }

    [Fact]
    public void CannotResolveNonStripeServicesFromStripeServiceProvider()
    {
        var collection = new ServiceCollection();

        collection.AddStripe().WithOptions(opts => opts.SecretKey = "SecretKey");

        var provider = collection.BuildServiceProvider();
        var stripeServiceProvider = provider.GetRequiredService<IStripeServiceProvider>();

        Assert.NotNull(stripeServiceProvider);

        Assert.Null(stripeServiceProvider.GetService<StripeOptions>());
        Assert.Null(stripeServiceProvider.GetService<Object>());
        Assert.Null(stripeServiceProvider.GetService<Product>());
    }

    [Fact]
    public void CanResolveStripeClientFromServiceProvider()
    {
        var collection = new ServiceCollection();

        collection.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                { "Stripe:SecretKey", "MyKey" }
            }).Build());

        collection.AddStripe();

        var provider = collection.BuildServiceProvider();
        var stripedClient = provider.GetRequiredService<IStripeClient>();
        Assert.NotNull(stripedClient);

        var keyedStripeClient =
            provider.GetKeyedService<IStripeClient>(StripeOptions.DefaultClientConfigurationSectionName);
        Assert.NotNull(keyedStripeClient);

        Assert.StrictEqual(stripedClient, keyedStripeClient);
    }

    [Fact]
    public void CanRegisterMultipleStripeClients()
    {
        const string clientOneKey = "ClientOne";
        const string otherStripe = "OtherStripe";

        var collection = new ServiceCollection();

        collection.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                { "Stripe:SecretKey", "MyKey" },
                { $"{clientOneKey}:SecretKey", clientOneKey },
                { $"{otherStripe}:SecretKey", otherStripe }
            }).Build());

        collection.AddStripe("ClientOne");
        collection.AddStripe("OtherStripe");

        var provider = collection.BuildServiceProvider();

        Assert.NotNull(provider.GetKeyedService<IStripeClient>(clientOneKey));
        Assert.NotNull(provider.GetKeyedService<IStripeClient>(otherStripe));

        var stripedClient = provider.GetService<IStripeClient>();
        Assert.Null(stripedClient);
    }

    [Fact]
    public void KeyedClientsRetrieveUniqueConfiguration()
    {
        const string clientOneKey = "ClientOne";
        const string otherStripe = "OtherStripe";

        var collection = new ServiceCollection();

        collection.AddStripe(clientOneKey).WithOptions(options => options.SecretKey = clientOneKey);
        collection.AddStripe(otherStripe).WithOptions(options => options.SecretKey = otherStripe);

        var provider = collection.BuildServiceProvider();

        var clientOne = provider.GetKeyedService<IStripeClient>(clientOneKey);
        Assert.NotNull(clientOne);
        Assert.Equal(clientOneKey, clientOne.ApiKey);

        var otherClient = provider.GetKeyedService<IStripeClient>(otherStripe);
        Assert.NotNull(otherClient);
        Assert.Equal(otherStripe, otherClient.ApiKey);
    }
    
    [Fact]
    public void UniqueConfigurationMerge()
    {
        const string clientOneKey = "ClientOne";

        var collection = new ServiceCollection();
        collection.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                { "Stripe:SecretKey", "MyKey" },
                { $"{clientOneKey}:SecretKey", "one" }
            }).Build());
        
        collection.AddStripe(clientOneKey).WithOptions(new StripeOptions
        {
            SecretKey = clientOneKey
        });

        var provider = collection.BuildServiceProvider();

        var clientOne = provider.GetKeyedService<IStripeClient>(clientOneKey);
        Assert.NotNull(clientOne);
        Assert.Equal(clientOneKey, clientOne.ApiKey);
        
        var stripeServiceProvider = provider.GetRequiredService<IStripeServiceProvider>();
        Assert.NotNull(stripeServiceProvider);
        
        var productService = stripeServiceProvider.GetService<ProductService>(clientOneKey);
        Assert.NotNull(productService);
        Assert.Equal(clientOneKey, productService.Client.ApiKey );
    }

    [Fact]
    public void ThrowsInvalidOperationExceptionIfSecretKeyIsNotProvided()
    {
        var collection = new ServiceCollection();
        collection.AddStripe();

        var provider = collection.BuildServiceProvider();
        var stripeServiceProvider = provider.GetRequiredService<IStripeServiceProvider>();
        var exception = Assert.Throws<InvalidOperationException>(() => stripeServiceProvider.GetService<PriceService>());
        Assert.StartsWith("SecretKey is required to make requests to Stripe API. ", exception.Message);
    }

    [Fact]
    public void ReadsKeyInformationFromDefaultConfigSection()
    {
        var collection = new ServiceCollection();

        collection.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                { "Stripe:SecretKey", "MyKey" }
            }).Build());

        collection.AddStripe();

        var provider = collection.BuildServiceProvider();
        var stripeServiceProvider = provider.GetRequiredService<IStripeServiceProvider>();

        var priceService = stripeServiceProvider.GetService<PriceService>()!;
        Assert.Equal("MyKey", priceService.Client.ApiKey);
    }

    [Fact]
    public void UsesApiKeyProvidedDuringRegistration()
    {
        var collection = new ServiceCollection();

        collection.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                { "Stripe:SecretKey", "MyKey" }
            }).Build());

        collection.AddStripe().WithOptions(opts => opts.SecretKey = "AnotherKey");

        var provider = collection.BuildServiceProvider();
        var stripeServiceProvider = provider.GetRequiredService<IStripeServiceProvider>();

        var priceService = stripeServiceProvider.GetService<PriceService>()!;
        Assert.Equal("AnotherKey", priceService.Client.ApiKey);
    }

    [Fact]
    public void AddStripeThrowsExceptionIfServiceCollectionNull()
    {
        ServiceCollection? collection = null;
        Assert.Throws<ArgumentNullException>(() => collection!.AddStripe());
    }
    
    [Fact]
    public void AddStripeThrowsExceptionIfClientNameNull()
    {
        var collection = new ServiceCollection();
        Assert.Throws<ArgumentNullException>(() => collection.AddStripe(null!));
    }
    
    [Fact]
    public void WithOptionsThrowsExceptionIfActionNull()
    {
        var collection = new ServiceCollection();
        Assert.Throws<ArgumentNullException>(() => collection.AddStripe().WithOptions((Action<StripeOptions>)null!));
    }
}