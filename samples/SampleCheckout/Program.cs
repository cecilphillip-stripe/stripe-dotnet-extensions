using Stripe;
using Stripe.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddStripe(o => o.SecretKey = "...");

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
app.MapStripeWebhookHandler<MyCoolHandler>();
app.Run();

public class MyCoolHandler: StripeWebhookHandler
{
    public override Task OnCustomerCreatedAsync(Event e)
    {
        return base.OnCustomerCreatedAsync(e);
    }
}