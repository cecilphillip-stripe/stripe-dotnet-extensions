using System.Globalization;
using System.Text;
using System.Text.Json.Nodes;

var spec = await new HttpClient().GetStringAsync("https://raw.githubusercontent.com/stripe/openapi/master/openapi/spec3.sdk.json");

var specJson = JsonNode.Parse(spec);
var events = specJson!
    ["paths"]!
    ["/v1/webhook_endpoints"]!
    ["post"]!
    ["requestBody"]!
    ["content"]!
    ["application/x-www-form-urlencoded"]!
    ["schema"]!
    ["properties"]!
    ["enabled_events"]!
    ["items"]!
    ["enum"]!.AsArray().Select(e => e!.ToString());


StringBuilder builder = new StringBuilder();
builder.AppendLine("namespace Stripe.AspNetCore;");
builder.AppendLine("public partial class StripeWebhookHandler");
builder.AppendLine("{");
TextInfo info = CultureInfo.CurrentCulture.TextInfo;
var methods = events.Where(e => e != "*").Select(e =>
{
    var methodName = "On" + info.ToTitleCase(e)
        .Replace("_", string.Empty)
        .Replace(".", string.Empty) + "Async";

    return new { MethodName =  methodName, EventName = e};
}).ToArray();

foreach (var method in methods)
{
    builder.AppendLine($@"    /// Fired when the {method.EventName} event is received.");
    builder.AppendLine($@"    public virtual Task {method.MethodName}(Event e) => UnhandledEventAsync(e);");
    builder.AppendLine();
}

builder.AppendLine($@"    protected virtual Task ExecuteAsync(Event e) => e.Type switch");
builder.AppendLine($@"    {{");
foreach (var method in methods)
{
    builder.AppendLine($@"        ""{method.EventName}"" => {method.MethodName}(e),");
}
builder.AppendLine($@"        _ => UnknownEventAsync(e),");
builder.AppendLine($@"    }};");

builder.AppendLine("}");

File.WriteAllText("../Stripe.AspNetCore/StripeWebhookHandler.Generated.cs", builder.ToString());