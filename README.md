# Stripe ASP.NET Core integration

Stripe ASP.NET Core provides Stripe SDK integration for dependency injection, configuration, logging and Webhook handling. 

## Install

```xml
<ItemGroup>
    <PackageReference Include="Stripe.AspNetCore" Version="0.1.0" />
</ItemGroup>
```

## Usage

Use the `services.AddStripe()` method to register Stripe services in Dependency Injection container:

```C#
// Startup-based apps
public void ConfigureServices(IServiceCollection services)
{
    services.AddStripe();
}

// Minimal API based apps
builder.Services.AddStripe();
```

You can now reference Stripe services from you controllers and other places that support dependency injection:

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

The API Key needs to be set calls can be made using Stripe SDK.
You can store the API key in the `Stripe` section of the `appsettings.json` or by passing the configuration action to `.AddStripe` call.


```json
{
  "Stripe": {
    "SecretKey" : "<secret key>",
    "WebhookSecret": "<webhook secret>"
  }
}
```

```C#
builder.Services.AddStripe(o => {
    o.SecretKey = "<secret key>";
    o.WebhookSecret = "<webhook secret>";
});
```

### Webhook handling

Stripe ASP.NET Core integration simplifies Webhook handling by automating the event parsing, signature validation and logging.
All you need to do is override appropriate events of the handler class.

Let's start by defining a handler class and inheriting from `StripeWebhookHandler`:

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
        
    }
}
```

The last step is to register the webhook handler with ASP.NET Core routing by calling `MapStripeWebhookHandler`.
NOTE: the Stripe Webhook handler uses ASP.NET Core routing, adding `app.UseRouting()` might be required if you application didn't use routing before.

```C#
// Startup-based apps
public void Configure(IApplicationBuilder app)
{
    app.UseRouting();
    app.UseEndpoints(b => b.MapStripeWebhookHandler<MyWebhookHandler>());
}

// Minimal API based apps
app.UseRouting();
app.MapStripeWebhookHandler<MyWebhookHandler>();
```

### Dependency Injection in Webhook handler

The Stripe Webhook handler supports constructor dependency injection. You can inject Stripe or other services by defining them as constructor parameters.

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

The `StripeWebhookHandler` also simplifies unit testing of your webhook handling logic.
For example, this is how you can unit-test the logic of the handler from the previous section using XUnit and Moq:

!!!!!! TODO VALIDATE THIS WORKS VVV
```C#
[Fact]
public async Task UpdatesCustomerOnCreation()
{
    var serviceMock = new Mock<CustomerService>();
    var handler = new MyWebhookHandler(serviceMock.Object);
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
    await handler.OnCustomerCreatedAsync(e);

    // Verify that the customer was updated with a new description
    serviceMock.Verify(s => s.UpdateAsync(
        "cus_123",
        It.Is<CustomerUpdateOptions>(o => o.Description == "New customer")));
}
```
