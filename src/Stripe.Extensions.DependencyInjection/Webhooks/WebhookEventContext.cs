using Microsoft.Extensions.Primitives;

namespace Stripe.Extensions.Webhooks;

public class WebhookEventContext
{
    public Stripe.Event WebhookEvent { get; }
    public IDictionary<string, StringValues> Headers { get; }
    
    public WebhookEventContext(Stripe.Event webhookEvent, IDictionary<string, StringValues> headers)
    {
        this.WebhookEvent = webhookEvent;
        this.Headers = headers;
    }
}