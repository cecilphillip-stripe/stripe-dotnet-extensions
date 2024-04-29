using Stripe;
using Stripe.Extensions.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddStripe();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapDefaultControllerRoute();
app.MapStripeWebhookHandler<MyWebhookHandler>();
app.Run();

public class MyWebhookHandler: StripeWebhookHandler
{
    private readonly CustomerService _customerService;
    public MyWebhookHandler(CustomerService customerService, StripeWebhookContext context): base(context)
    {
        _customerService = customerService;
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
