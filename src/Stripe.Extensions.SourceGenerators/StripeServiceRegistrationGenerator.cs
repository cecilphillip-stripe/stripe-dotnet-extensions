using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MrMeeseeks.SourceGeneratorUtility;

namespace Stripe.Extensions.SourceGenerators;

[Generator]
[Obsolete("This generator is no longer needed")]
public class StripeServiceRegistrationGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var assemblySymbol =
            context.Compilation.SourceModule.ReferencedAssemblySymbols.First(q =>
                q.Name.StartsWith("Stripe"));

        //TODO: log and return if not found
        var typeCache = new NamedTypeCache(context, new CheckInternalsVisible(context));
        var stripeServiceTypes = typeCache.ForAssembly(assemblySymbol)
            .Where(sym => sym.Name.EndsWith("Service") && !sym.IsAbstract)
            .OrderBy(sym => sym.Name);


        var registrationCode = GenerateRegistrationCode(stripeServiceTypes);

        var generatedCode = SourceText.From($@"
using Stripe;

namespace Microsoft.Extensions.DependencyInjection;

    public static partial class StripeServiceCollectionExtensions
    {{
        private static void RegisterStripeServices(IServiceCollection services)
        {{
            // generated code
            {registrationCode}
        }}
    }}
", Encoding.UTF8);

        context.AddSource("Stripe.Extensions.Registration.g.cs", generatedCode);
    }

    private string GenerateRegistrationCode(IEnumerable<INamedTypeSymbol> serviceTypes)
    {
        var displayFormat = new SymbolDisplayFormat(
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle
                    .NameAndContainingTypesAndNamespaces);

        // Generate code to register each service type with Transient lifetime
        var registrationCode = string.Join("\n            ", serviceTypes
            .Select(serviceType =>
                $"services.AddTransient<{serviceType.ToDisplayString(displayFormat)}>(provider => new {serviceType.ToDisplayString(displayFormat)}(provider.GetService<IStripeClient>()));"));

        return registrationCode;
    }
}
