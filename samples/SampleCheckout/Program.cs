using SampleCheckout.Processors;
using Stripe.Extensions.AspNetCore;
using Stripe.Extensions.DependencyInjection;
using Stripe.Extensions.Webhooks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddStripe();
//builder.Services.AddSingleton<IWebhookEventProcessor, CatchAllEventProcessor>();
builder.Services.AddSingleton<IWebhookEventProcessor, ObservableEventProcessor>(provider =>
{
    var processor = new ObservableEventProcessor();
    var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger("SampleCheckout");
    processor.WebhookEventsObservable.Subscribe(hookCtx =>
    {
        logger.LogInformation("Received WebhookEvent type {WebhookEventType}", hookCtx.WebhookEvent.Type);
    });
    
    return processor;
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.MapStripeWebhooks();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();