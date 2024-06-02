using Microsoft.Extensions.Http.Resilience;
using Stripe;
using Stripe.Extensions.AspNetCore;
using Stripe.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddStripe()
    .AddStandardResilienceHandler();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapDefaultControllerRoute();
app.MapStripeWebhookHandler<MyWebhookHandler>();
app.Run();

public class MyWebhookHandler: StripeWebhookHandler
{
    private readonly CustomerService? _customerService;
    public MyWebhookHandler(IStripeServiceProvider stripeServiceProvider, StripeWebhookContext context): base(context)
    {
        _customerService = stripeServiceProvider.GetService<CustomerService>(Context.StripeOptions.ClientName);
    }

    public override async Task OnCustomerCreatedAsync(Event e)
    {
        var customer = (e.Data.Object as Customer)!;
        await _customerService.UpdateAsync(customer.Id, new CustomerUpdateOptions()
        {
            Description = "New customer"
        });
    }
}
