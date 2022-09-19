using Microsoft.Extensions.Primitives;

namespace Stripe.Extensions.Webhooks;

public class WebhookEventContext
{
   public Stripe.Event WebhookEvent { get; }
   public IDictionary<string, StringValues> Headers { get; }
   
   public WebhookEventContext(Stripe.Event WebhookEvent,
      IDictionary<string, StringValues> Headers)
   {
      this.WebhookEvent = WebhookEvent;
      this.Headers = Headers;
   }
   
   public void Deconstruct(out Stripe.Event WebhookEvent, out IDictionary<string, StringValues> Headers)
   {
      WebhookEvent = this.WebhookEvent;
      Headers = this.Headers;
   }
}