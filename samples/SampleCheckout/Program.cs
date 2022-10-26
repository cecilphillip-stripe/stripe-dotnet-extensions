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
app.MapStripeWebhookHandler<MyHandler>();
app.Run();

public class MyHandler: StripeWebhookHandler
{
    private readonly PaymentIntentService _service;

    public MyHandler(PaymentIntentService service)
    {
        _service = service;
    }

    public override async Task OnPaymentIntentCreatedAsync(Event e)
    {
        PaymentIntent paymentIntent = (PaymentIntent)e.Data.Object;
        await _service.ConfirmAsync(paymentIntent.Id);
    }
}