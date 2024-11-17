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

public class MyWebhookHandler(StripeClient stripeClient, StripeWebhookContext context, ILogger<MyWebhookHandler> logger)
    : StripeWebhookHandler(context, logger)
{
    public override async Task OnCustomerCreatedAsync(Event e)
    {
        var customer = (e.Data.Object as Customer)!;
        await stripeClient.V1.Customers.UpdateAsync(customer.Id, new CustomerUpdateOptions()
        {
            Description = "New customer"
        });
    }
}
