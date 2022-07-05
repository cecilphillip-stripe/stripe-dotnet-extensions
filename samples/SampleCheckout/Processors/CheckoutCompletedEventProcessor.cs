using Stripe.Checkout;
using Stripe.Extensions.Webhooks;

namespace SampleCheckout.Processors;

public class CheckoutCompletedEventProcessor :WebhookEventProcessor
{
    private readonly ILogger<CheckoutCompletedEventProcessor> _logger;

    public CheckoutCompletedEventProcessor(ILogger<CheckoutCompletedEventProcessor> logger)
    {
        _logger = logger;
    }
    protected override Task ProcessCheckoutSessionCompletedEventAsync(WebhookEventContext webhookContext, Session checkoutSession)
    {
        _logger.LogInformation("Received checkout event {CheckoutEventID}", checkoutSession.Id);
        return Task.CompletedTask;
    }
}