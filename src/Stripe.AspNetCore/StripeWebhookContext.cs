using Microsoft.AspNetCore.Http;
using Stripe.Extensions.DependencyInjection;

namespace Stripe.AspNetCore;

public class StripeWebhookContext
{
    public HttpContext HttpContext { get; }
    public StripeOptions StripeOptions { get; }

    public StripeWebhookContext(HttpContext httpContext, StripeOptions stripeOptions)
    {
        HttpContext = httpContext;
        StripeOptions = stripeOptions;
    }
}