using Stripe;
using Stripe.Extensions.AspNetCore;
using Stripe.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddStripe(configureOptions: opts =>
    {
        opts.ApiKey = opts.WebhookSecret = "ok_test_123";
    })
    .AddStandardResilienceHandler();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapDefaultControllerRoute();
app.MapStripeWebhookHandler<MyWebhookHandler>();
app.Run();

public class MyWebhookHandler: StripeWebhookHandler
{
    private readonly StripeClient _stripeClient;
    public MyWebhookHandler(IServiceProvider stripeServiceProvider, StripeWebhookContext context): base(context)
    {
        Logger = stripeServiceProvider.GetRequiredService<ILogger<MyWebhookHandler>>();
        _stripeClient = stripeServiceProvider.GetRequiredService<StripeClient>();
        //_stripeClient = stripeServiceProvider.GetKeyedService<StripeClient>(Context.StripeOptions.ClientName);
    }

    public override async Task OnCustomerCreatedAsync(Event e)
    {
        var customer = (e.Data.Object as Customer)!;
        await _stripeClient.V1.Customers.UpdateAsync(customer.Id, new CustomerUpdateOptions()
        {
            Description = "New customer"
        });
    }
}
