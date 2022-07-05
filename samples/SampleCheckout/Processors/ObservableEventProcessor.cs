using System.Reactive.Linq;
using System.Reactive.Subjects;
using Stripe.Extensions.Webhooks;

namespace SampleCheckout.Processors;

public class ObservableEventProcessor: WebhookEventProcessor
{
    private Subject<WebhookEventContext> _webhookEvents = new Subject<WebhookEventContext>();
    public IObservable<WebhookEventContext> WebhookEventsObservable => this._webhookEvents.AsObservable();

    protected void WebhookReceived(WebhookEventContext webhookContext) => this._webhookEvents.OnNext(webhookContext);
    
    public override Task ProcessWebhookAsync(WebhookEventContext webhookContext)
    {
        WebhookReceived(webhookContext);
        return Task.CompletedTask;
    }
}