using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MrMeeseeks.SourceGeneratorUtility;

namespace Stripe.Extensions.AspNetCore.SourceGenerators;


[Generator]
public class StripeWebhookHandlerGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {

    }

    public void Execute(GeneratorExecutionContext context)
    {
        var myFiles = context.AdditionalFiles.Where(at => at.Path.EndsWith("spec3.sdk.json"));
        var registrationCode = string.Empty;

        var generatedCode = SourceText.From($@"
using Stripe;

namespace Stripe.AspNetCore;

    public partial class StripeWebhookHandler
    {{
       
            // generated code
            {registrationCode}
    
    }}
", Encoding.UTF8);

        context.AddSource("Stripe.AspNetCore.Generated.cs", generatedCode);
    }

}
