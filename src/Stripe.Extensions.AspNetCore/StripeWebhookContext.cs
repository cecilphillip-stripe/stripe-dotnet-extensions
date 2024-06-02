using Microsoft.AspNetCore.Http;
using Stripe.Extensions.DependencyInjection;

namespace Stripe.Extensions.AspNetCore;

public class StripeWebhookContext(HttpContext httpContext, StripeOptions stripeOptions, IStripeClient stripeClient)
{
    public HttpContext HttpContext { get; } = httpContext;
    public StripeOptions StripeOptions { get; } = stripeOptions;
    public IStripeClient? Client { get; } = stripeClient;
}
