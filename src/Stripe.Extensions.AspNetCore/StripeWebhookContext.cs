using Microsoft.AspNetCore.Http;
using Stripe.Extensions.DependencyInjection;

namespace Stripe.Extensions.AspNetCore;

public class StripeWebhookContext(HttpContext httpContext, StripeOptions stripeOptions, StripeClient? stripeClient)
{
    public HttpContext HttpContext { get; } = httpContext;
    public StripeOptions StripeOptions { get; } = stripeOptions;
    public StripeClient? Client { get; } = stripeClient;
}
