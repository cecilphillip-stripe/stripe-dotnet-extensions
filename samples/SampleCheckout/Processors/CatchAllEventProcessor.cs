using Stripe.Extensions.Webhooks;

namespace SampleCheckout.Processors;

public class CatchAllEventProcessor : IWebhookEventProcessor
{
    private readonly ILogger<CatchAllEventProcessor> _logger;

    public CatchAllEventProcessor(ILogger<CatchAllEventProcessor> logger)
    {
        _logger = logger;
    }

    public Task ProcessWebhookAsync(WebhookEventContext webhookContext)
    {
        _logger.LogInformation("Received event type {EventType}", webhookContext.WebhookEvent.Type);
        return Task.CompletedTask;
    }
}