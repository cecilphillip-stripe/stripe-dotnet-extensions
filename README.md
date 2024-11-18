# Stripe .NET Extensions

![logo](https://cdn.brandfolder.io/KGT2DTA4/at/bskj2q8srfqx3cvfqvhk73pc/Stripe_wordmark_-_blurple_small.png?width=437&height=208)

![](https://github.com/cecilphillip-stripe/stripe-dotnet-extensions/actions/workflows/build.yml/badge.svg)

The Stripe .NET Extension packages provide a collection of convenient features 
to help improve the experience integrating Stripe in .NET applications. 

- Stripe.Extensions.DependencyInjection - provides configuration and dependency injection support for the [Stripe .NET SDK](https://github.com/stripe/stripe-dotnet).
- Stripe.Extensions.AspNetCore - provides webhook handling helpers for Stripe [events](https://docs.stripe.com/api/events/types) in ASP.NET Core applications.


## Install

```shell
dotnet add package Stripe.Extensions.DependencyInjection
dotnet add package Stripe.Extensions.AspNetCore
```

## Usage

Use the `AddStripe()` extension method to register Stripe services in Dependency Injection container:

```csharp
// Startup-based apps
public void ConfigureServices(IServiceCollection services)
{
    services.AddStripe();
}

// Minimal API based apps
builder.Services.AddStripe();
```

The `AddStripe()` extension also supports registering named Stripe clients.

```csharp
builder.Services.AddStripe(); // default client
builder.Services.AddStripe("client1"); // client1
builder.Services.AddStripe("client2"); // client2
```

### Configuration

The Stripe [API keys](https://docs.stripe.com/keys#obtain-api-keys) need to be configured before calls can be made using the SDK.
By default, the extension packages will look for a `Stripe` configuration section when calling `AddStripe()` 
without a client name. For named clients, the configuration section should match the client name.

To configure the default client: 
```json
{
  "Stripe": {
    "SecretKey" : "<secret key>",
    "WebhookSecret": "<webhook secret>"
  }
}
```
To configure a client named `client1`:
```json
{
  "client1": {
    "SecretKey" : "<secret key>",
    "WebhookSecret": "<webhook secret>"
  }
}
```

Configuration can also be attached to each registered client by using the `WithOptions` method.
It can accept an instance of `StripeOptions` or a configuration delegate.

```csharp
// using StripeOptions
builder.Services.AddStripe()
    .WithOptions(new StripeOptions {
        SecretKey = "<secret key>",
        WebhookSecret = "<webhook secret>"
    });

// using configuration delegate
builder.Services.AddStripe("client1")
    .WithOptions( options => { 
        options.SecretKey = "<secret key>";
        options.WebhookSecret = "<webhook secret>";
    });
```
> If both configuration options are used, `WithOptions` will take precedence and override the values
provided in the configuration section.

### Registering services
`AddStripe` registers the following services in the DI container:
- `IStripeClient` - the main Stripe client used to make API calls.
- `IStripeServiceProvider` - a service provider that can be used to resolve Stripe services.

Retrieving the default client registered with `AddStripe()`: 
```csharp
public class HomeController : Controller
{
    private readonly IStripeClient _stripeClient;

    public HomeController(IStripeClient stripeClient)
    {
        _stripeClient = stripeClient;
    }
    
    public async Task<IActionResult> Index()
    {
        var customerService = new CustomerService(_stripeClient);
        var customer = await customerService.GetAsync("cus_NffrFeUfNV2Hib");        
        ...
        return View();
    } 
}
```

Retrieving a client registered with `AddStripe("client1")`:
```csharp
public class HomeController : Controller
{
    private readonly IStripeClient _stripeClient;

    public HomeController([FromKeyedServices("client1")]IStripeClient stripeClient)
    {
        _stripeClient = stripeClient;
    }
}
```

The `IStripeServiceProvider` service is provided as a convenience helper to resolve Stripe named services. 
```csharp
public class HomeController : Controller
{
    private readonly IStripeServiceProvider _stripeServiceProvider;

    public HomeController(IStripeServiceProvider stripeServiceProvider)
    {
        _stripeServiceProvider = stripeServiceProvider;
    }
    
    public async Task<IActionResult> Index()
    {
        // default client
        var customerService = _stripeServiceProvider.GetService<CustomerService>();
        
        // named client
        var nameCustomerService = _stripeServiceProvider.GetService<CustomerService>("client1"); 
        ...
        return View();
    } 
}
```

### Webhook handling

The Stripe.Extensions.AspNetCore package simplifies Webhook handling by automating the event parsing, signature validation and logging.
All that's needed is to override the appropriate events of the handler class.

First, define a handler class that inherits from [StripeWebhookHandler](./src/Stripe.Extensions.AspNetCore/StripeWebhookHandler.cs):

```csharp
public class MyWebhookHandler: StripeWebhookHandler {}
```

The `StripeWebhookHandler` class defines virtual methods for all known webhook events.
To handle an event override the corresponding `On*Async` method.

```csharp
public class MyWebhookHandler: StripeWebhookHandler
{
    public override Task OnCustomerCreatedAsync(Event e)
    {
        // handle customer.create event
        var customer = (e.Data.Object as Customer);        
    }
}
```

The last step is to register the webhook handler with ASP.NET Core routing by calling `MapStripeWebhookHandler`.
> NOTE: the Stripe Webhook handler uses ASP.NET Core routing, so adding a call to `app.UseRouting()` might be required.

```csharp
// Startup-based apps
public void Configure(IApplicationBuilder app)
{
    app.UseRouting();
    app.UseEndpoints(b => b.MapStripeWebhookHandler<MyWebhookHandler>());
}

// Minimal API based apps
app.MapStripeWebhookHandler<MyWebhookHandler>();
```

### Dependency Injection in StripeWebhookHandler

The `StripeWebhookHandler` also supports constructor dependency injection, so Stripe or other services can be injected by defining them as constructor parameters.

```csharp
public class MyWebhookHandler: StripeWebhookHandler
{
    private readonly CustomerService _customerService;
    public MyWebhookHandler(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public override async Task OnCustomerCreatedAsync(Event e)
    {
        Customer customer = (Customer)e.Data.Object;
        await _customerService.UpdateAsync(customer.Id, new CustomerUpdateOptions()
        {
            Description = "New customer"
        });
    }
}
```

## Unit testing

The `StripeWebhookHandler` also simplifies unit testing of webhook handling logic.
For example, here is how a unit-test might be written to test the logic of the handler from the previous section:

```csharp
[Fact]
public async Task UpdatesCustomerOnCreation()
{
     var serviceMock = new Mock<CustomerService>();
        var handler = new MyWebhookHandler(serviceMock.Object);
        // Prepare the event
        var e = new Event()
        {
            Data = new EventData()
            {
                Object = new Customer()
                {
                    Id = "cus_123"
                }
            }
        };

        // Invoke the handler
        await handler.OnCustomerCreatedAsync(e);

        // Verify that the customer was updated with a new description
        serviceMock.Verify(s => s.UpdateAsync(
            "cus_123",
            It.Is<CustomerUpdateOptions>(o => o.Description == "New customer"),
            It.IsAny<RequestOptions>(),
            It.IsAny<CancellationToken>()));
}
```


### Useful links
- [Stripe Docs](https://docs.stripe.com)
- [Stripe API Reference](https://docs.stripe.com/api)

To keep track of major Stripe API updates and versions, reference the 
[API upgrades page](https://docs.stripe.com/upgrades#api-versions) in the Stripe documentation. 
For a detailed list of API changes, please refer to the [API Changelog](https://docs.stripe.com/changelog).