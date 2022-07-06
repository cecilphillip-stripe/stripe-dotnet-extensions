using Stripe.Checkout;

namespace Stripe.Extensions.Webhooks;

public interface IWebhookEventProcessor
{
    Task ProcessWebhookAsync(WebhookEventContext webhookContext);
}

public abstract class WebhookEventProcessor : IWebhookEventProcessor
{
    public virtual Task ProcessWebhookAsync(WebhookEventContext webhookContext)
    {
        if(webhookContext is null)
            throw new ArgumentNullException(nameof(webhookContext));
        
        return webhookContext.WebhookEvent.Type switch
        {
            Events.AccountUpdated => ProcessAccountUpdatedEventAsync(webhookContext,
                webhookContext.WebhookEvent.Data.Object as Account),
            Events.BalanceAvailable => ProcessBalanceAvailableEventAsync(webhookContext,
                webhookContext.WebhookEvent.Data.Object as Balance),
            Events.CheckoutSessionCompleted => ProcessCheckoutSessionCompletedEventAsync(webhookContext,
                webhookContext.WebhookEvent.Data.Object as Stripe.Checkout.Session),
            Events.CheckoutSessionExpired => ProcessCheckoutSessionExpiredEventAsync(webhookContext,
                webhookContext.WebhookEvent.Data.Object as Stripe.Checkout.Session),
            Events.CheckoutSessionAsyncPaymentSucceeded => ProcessCheckoutSessionAsyncPaymentSucceededEventAsync(
                webhookContext, webhookContext.WebhookEvent.Data.Object as Stripe.Checkout.Session),
            Events.CheckoutSessionAsyncPaymentFailed => ProcessCheckoutSessionAsyncPaymentFailedEventAsync(
                webhookContext, webhookContext.WebhookEvent.Data.Object as Stripe.Checkout.Session),
            _ => Task.CompletedTask
        };
    }

    protected virtual Task ProcessCheckoutSessionAsyncPaymentSucceededEventAsync(WebhookEventContext webhookContext,
        Session checkoutSession) => Task.CompletedTask;

    protected virtual Task ProcessCheckoutSessionAsyncPaymentFailedEventAsync(WebhookEventContext webhookContext,
        Session checkoutSession) => Task.CompletedTask;

    protected virtual Task ProcessCheckoutSessionExpiredEventAsync(WebhookEventContext webhookContext,
        Session checkoutSession) => Task.CompletedTask;

    protected virtual Task ProcessCheckoutSessionCompletedEventAsync(WebhookEventContext webhookContext,
        Session checkoutSession) => Task.CompletedTask;

    protected virtual Task ProcessAccountUpdatedEventAsync(WebhookEventContext webhookContext, Account? account) =>
        Task.CompletedTask;

    protected virtual Task ProcessBalanceAvailableEventAsync(WebhookEventContext webhookContext, Balance? account) =>
        Task.CompletedTask;
}