using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Raiqub.Generators.InterpolationCodeWriter.CSharp.Tests.Fakes;

public class SourceProductionContextRunner
{
    public static string GeneratorBasePath { get; } = typeof(TestSourceGenerator).FullName!;

    public static GeneratorDriverRunResult Execute(
        Action<SourceProductionContext> generateAction,
        CancellationToken cancellationToken = default
    )
    {
        var generatorDriver = CSharpGeneratorDriver.Create(new TestSourceGenerator(generateAction));
        var compilation = CreateCompilation();
        return generatorDriver.RunGenerators(compilation, cancellationToken).GetRunResult();
    }

    private static CSharpCompilation CreateCompilation()
    {
        return CSharpCompilation.Create(
            "TestAssembly",
            syntaxTrees: [CSharpSyntaxTree.ParseText("// Empty compilation")],
            references: AppDomain
                .CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                .Select(a => MetadataReference.CreateFromFile(a.Location))
        );
    }

    private sealed class TestSourceGenerator(Action<SourceProductionContext> generateAction) : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var valueProvider = context.SyntaxProvider.CreateSyntaxProvider((_, _) => true, (_, _) => 0).Collect();

            context.RegisterSourceOutput(valueProvider, (productionContext, _) => generateAction(productionContext));
        }
    }
}
