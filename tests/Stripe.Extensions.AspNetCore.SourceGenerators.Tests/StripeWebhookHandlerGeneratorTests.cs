using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace Stripe.Extensions.AspNetCore.SourceGenerators.Tests;

public class StripeWebhookHandlerGeneratorTests
{
    [Fact]
    public void GeneratorOutputsAspNetCore()
    {
        // Run generator using a driver with an empty compilation
        var generator = new StripeWebhookHandlerGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);

        var compilation = CSharpCompilation.Create(nameof(StripeWebhookHandlerGeneratorTests));
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var newCompilation, out var diagnostics);

        
        // Retrieve all files in the compilation.
        var generatedFiles = newCompilation.SyntaxTrees
            .Select(t => Path.GetFileName(t.FilePath))
            .ToArray();

        Assert.Equivalent(new[] { "Stripe.Extensions.AspNetCore.g.cs" }, generatedFiles);
        
        Assert.Empty(diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));
    }
}