namespace Stripe.Extensions.Webhooks;

public interface IWebhookEventProcessor
{
    Task ProcessWebhookAsync(WebhookEventContext webhookContext);
}

public abstract class WebhookEventProcessor: IWebhookEventProcessor
{
    public virtual Task ProcessWebhookAsync(WebhookEventContext webhookContext) =>
        webhookContext.WebhookEvent.Type switch
        {
            Events.AccountUpdated => ProcessAccountUpdatedEventAsync(webhookContext, webhookContext.WebhookEvent.Data.Object as Account),
            Events.BalanceAvailable => ProcessBalanceAvailableEventAsync(webhookContext, webhookContext.WebhookEvent.Data.Object as Balance),
            _ => Task.CompletedTask
        };

    protected virtual Task ProcessAccountUpdatedEventAsync(WebhookEventContext webhookContext, Account? account) => Task.CompletedTask;
    protected virtual Task ProcessBalanceAvailableEventAsync(WebhookEventContext webhookContext, Balance? account) => Task.CompletedTask;
}