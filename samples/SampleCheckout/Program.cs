using Stripe;
using Stripe.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddStripe();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
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
        Customer customer = (Customer)e.Data.Object;
        await _customerService.UpdateAsync(customer.Id, new CustomerUpdateOptions()
        {
            Description = "New customer"
        });
    }
}
