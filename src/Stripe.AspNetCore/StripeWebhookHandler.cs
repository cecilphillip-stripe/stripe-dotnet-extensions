using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Stripe.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace Stripe.AspNetCore;

public abstract partial class StripeWebhookHandler
{
    private ILogger<StripeWebhookHandler>? _logger;
    private StripeWebhookContext? _context;

    public StripeWebhookContext Context
    {
        get
        {
            if (_context == null) throw new InvalidOperationException();

            return _context;
        }
        set => _context = value;
    }

    private ILogger<StripeWebhookHandler> Logger
    {
        get
        {
            if (_logger == null)
            {
                _logger = Context.HttpContext.RequestServices.GetService<ILogger<StripeWebhookHandler>>() ??
                          NullLogger<StripeWebhookHandler>.Instance;
            }

            return _logger;
        }
    }

    public async Task ExecuteAsync()
    {
        HttpContext httpContext = Context.HttpContext;
        HttpResponse response = httpContext.Response;

        Event stripeEvent;
        try
        {
            using StreamReader stream = new StreamReader(httpContext.Request.Body);
            HttpRequest request = httpContext.Request;
            string body = await stream.ReadToEndAsync();
            StripeOptions options = Context.StripeOptions;
            stripeEvent = EventUtility.ConstructEvent(
                body,
                request.Headers["Stripe-Signature"],
                options.WebhookSecret,
                300, // default tolerance
                options.ThrowOnWebhookApiVersionMismatch);
        }
        catch (Exception e)
        {
            EventParsingError(Logger, e);
            response.StatusCode = 500;
            return;
        }

        try
        {
            await ExecuteInternalAsync(stripeEvent).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            ExecutionError(Logger, stripeEvent.Type, e);
            response.StatusCode = 500;
        }
    }

    protected virtual Task UnhandledEventAsync(Event e, [CallerMemberName] string? handlerMethod = null)
    {
        UnhandledEvent(Logger, e.Type, handlerMethod ?? "<unknown>", null);
        return Task.CompletedTask;
    }

    protected virtual Task UnknownEventAsync(Event e)
    {
        UnknownEvent(Logger, e.Type, null);
        return Task.CompletedTask;
    }

    private static Action<ILogger, Exception> EventParsingError = LoggerMessage.Define(
        LogLevel.Warning,
        1,
        "Exception occured while parsing the Stripe WebHook event payload.");
    
    private static Action<ILogger, string, Exception> ExecutionError = LoggerMessage.Define<string>(
        LogLevel.Warning,
        2,
        "Exception occured while executing event handler for {event_type}");

    private static Action<ILogger, string, Exception?> UnknownEvent = LoggerMessage.Define<string>(
        LogLevel.Warning,
        3,
        "Event type {event_type} is not supported by this version of the library, consider upgrading." + 
        "You can override the UnknownEventAsync method to suppress this log message.");
    
    private static Action<ILogger, string, string, Exception?> UnhandledEvent = LoggerMessage.Define<string, string>(
        LogLevel.Warning,
        3,
        "Event type {event_type} does not have a handler. Override the {method_name} method to handle the event.");
}