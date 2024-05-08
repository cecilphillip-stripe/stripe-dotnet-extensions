namespace Stripe.Extensions.DependencyInjection;

public class StripeOptions
{
    public const string ConfigurationSectionName = "Stripe";
    public string PublicKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string WebhookSecret { get; set; } = string.Empty;
    public bool ThrowOnWebhookApiVersionMismatch { get; set; } = true;
    public bool EnableTelemetry { get; set; } = true;
    public AppInfo? AppInfo { get; set; }
    public int MaxNetworkRetries { get; set; } = SystemNetHttpClient.DefaultMaxNumberRetries;
}
