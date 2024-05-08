# Stripe .NET Extensions

![logo](https://cdn.brandfolder.io/KGT2DTA4/at/bskj2q8srfqx3cvfqvhk73pc/Stripe_wordmark_-_blurple_small.png?width=437&height=208)

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

Use the `IServiceCollection.AddStripe()` extension method to register Stripe services in Dependency Injection container:

```C#
// Startup-based apps
public void ConfigureServices(IServiceCollection services)
{
    services.AddStripe();
}

// Minimal API based apps
builder.Services.AddStripe();
```

Now Stripe services can be injected into application components.

```C#
public class HomeController : Controller
{
    private readonly ProductService _productService;

    public HomeController(ProductService productService)
    {
        _productService = productService;
    }
}
```

### Configuration

The Stripe [API keys](https://docs.stripe.com/keys#obtain-api-keys) need to be configured before calls can be made using the SDK.
The extension packages will look for a `Stripe` configuration section by default.


```json
{
  "Stripe": {
    "SecretKey" : "<secret key>",
    "WebhookSecret": "<webhook secret>"
  }
}
```

There is also the option of updating the settings directly via a `.AddStripe` call

```C#
builder.Services.AddStripe(o => {
    o.SecretKey = "<secret key>";
    o.WebhookSecret = "<webhook secret>";
});
```

or by passing an instance of `IConfiguration`. 

```C#
builder.Services.AddStripe(configuration.GetSection("CustomSection"));
```

### Webhook handling

The Stripe.Extensions.AspNetCore package simplifies Webhook handling by automating the event parsing, signature validation and logging.
All that's needed is to override the appropriate events of the handler class.

First, define a handler class that inherits from [StripeWebhookHandler](./src/Stripe.Extensions.AspNetCore/StripeWebhookHandler.cs):

```C#
public class MyWebhookHandler: StripeWebhookHandler {}
```

The `StripeWebhookHandler` class defines virtual methods for all known webhook events.
To handle an event override the corresponding `On*Async` method.

```C#
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

```C#
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

```C#
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

```C#
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