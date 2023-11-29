// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using Stripe;

Console.WriteLine("Hello, World!");

namespace Stripe.AspNetCore
{
    public abstract partial class StripeWebhookHandler
    {

        protected virtual Task UnknownEventAsync(Event e)
        {
            return Task.CompletedTask;
        }
        protected virtual Task UnhandledEventAsync(Event e,
            [CallerMemberName] string? handlerMethod = null)
        {
            return Task.CompletedTask;
        }
    }
}
