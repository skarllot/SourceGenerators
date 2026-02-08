using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Raiqub.Generators.InterpolationCodeWriter.CSharp.Tests.Fakes;

namespace Raiqub.Generators.InterpolationCodeWriter.CSharp.Tests;

public class CodeWriterDispatcherGenericTest
{
    [Fact]
    public void GenerateSources_WithNoModels_DoesNothing()
    {
        var dispatcher = new CodeWriterDispatcher<TestModel>([]);
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(Array.Empty<TestModel>(), context)
        );

        Assert.Empty(runResult.GeneratedTrees);
        Assert.Empty(runResult.Diagnostics);
    }

    [Fact]
    public void GenerateSources_WithNoCodeWriters_DoesNothing()
    {
        var dispatcher = new CodeWriterDispatcher<TestModel>([]);
        var models = new[] { new TestModel("Test") };
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(models, context)
        );

        Assert.Empty(runResult.GeneratedTrees);
        Assert.Empty(runResult.Diagnostics);
    }

    [Fact]
    public void GenerateSources_WithSingleCodeWriter_GeneratesSource()
    {
        var codeWriter = new TestCodeWriter();
        var dispatcher = new CodeWriterDispatcher<TestModel>([codeWriter]);
        var models = new[] { new TestModel("TestClass") };
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(models, context)
        );

        Assert.Single(runResult.GeneratedTrees);
        Assert.EndsWith("TestClass.g.cs", runResult.GeneratedTrees[0].FilePath);
        Assert.Contains("// TestClass", runResult.GeneratedTrees[0].GetText().ToString());
        Assert.Empty(runResult.Diagnostics);
    }

    [Fact]
    public void GenerateSources_WithMultipleModels_GeneratesMultipleSources()
    {
        var codeWriter = new TestCodeWriter();
        var dispatcher = new CodeWriterDispatcher<TestModel>([codeWriter]);
        var models = new[] { new TestModel("Class1"), new TestModel("Class2") };
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(models, context)
        );

        Assert.Equal(2, runResult.GeneratedTrees.Length);
        Assert.Contains(
            runResult.GeneratedTrees,
            s => s.FilePath.EndsWith("Class1.g.cs", StringComparison.Ordinal)
        );
        Assert.Contains(
            runResult.GeneratedTrees,
            s => s.FilePath.EndsWith("Class2.g.cs", StringComparison.Ordinal)
        );
        Assert.Empty(runResult.Diagnostics);
    }

    [Fact]
    public void GenerateSources_WithMultipleCodeWriters_GeneratesAllSources()
    {
        var codeWriter1 = new TestCodeWriter();
        var codeWriter2 = new TestCodeWriter(suffix: "2");
        var dispatcher = new CodeWriterDispatcher<TestModel>([codeWriter1, codeWriter2]);
        var models = new[] { new TestModel("TestClass") };
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(models, context)
        );

        Assert.Equal(2, runResult.GeneratedTrees.Length);
        Assert.Contains(
            runResult.GeneratedTrees,
            s => s.FilePath.EndsWith("TestClass.g.cs", StringComparison.Ordinal)
        );
        Assert.Contains(
            runResult.GeneratedTrees,
            s => s.FilePath.EndsWith("TestClass2.g.cs", StringComparison.Ordinal)
        );
        Assert.Empty(runResult.Diagnostics);
    }

    [Fact]
    public void GenerateSources_WhenCanGenerateForReturnsFalse_SkipsGeneration()
    {
        var codeWriter = new TestCodeWriter(shouldGenerate: false);
        var dispatcher = new CodeWriterDispatcher<TestModel>([codeWriter]);
        var models = new[] { new TestModel("TestClass") };
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(models, context)
        );

        Assert.Empty(runResult.GeneratedTrees);
        Assert.Empty(runResult.Diagnostics);
    }

    [Fact]
    public void GenerateSources_WithImmutableArray_GeneratesSources()
    {
        var codeWriter = new TestCodeWriter();
        var dispatcher = new CodeWriterDispatcher<TestModel>([codeWriter]);
        var models = ImmutableArray.Create(new TestModel("TestClass"));
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(models, context)
        );

        Assert.Single(runResult.GeneratedTrees);
        Assert.EndsWith("TestClass.g.cs", runResult.GeneratedTrees[0].FilePath);
        Assert.Empty(runResult.Diagnostics);
    }

    [Fact]
    public void GenerateSources_WhenCodeWriterThrowsWithoutHandler_ReportsDiagnostic()
    {
        var codeWriter = new ThrowingCodeWriter();
        var dispatcher = new CodeWriterDispatcher<TestModel>([codeWriter]);
        var models = new[] { new TestModel("TestClass") };

        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(models, context)
        );

        Assert.Empty(runResult.GeneratedTrees);
        Assert.Single(runResult.Diagnostics);
        Assert.Equal("CS8785", runResult.Diagnostics[0].Id); // Generator failed to generate source
    }

    [Fact]
    public void GenerateSources_WhenCodeWriterThrowsWithHandler_ReportsDiagnostic()
    {
        var codeWriter = new ThrowingCodeWriter();
        var dispatcher = new CodeWriterDispatcher<TestModel>(
            exceptionHandler: (ex, model) =>
                Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "TEST001",
                        "Test Error",
                        $"{ex.Message} for {model.Name}",
                        "Test",
                        DiagnosticSeverity.Error,
                        true
                    ),
                    Location.None
                ),
            codeWriters: codeWriter
        );
        var models = new[] { new TestModel("TestClass") };
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(models, context)
        );

        Assert.Empty(runResult.GeneratedTrees);
        Assert.Single(runResult.Diagnostics);
        Assert.Equal("TEST001", runResult.Diagnostics[0].Id);
        Assert.Contains("TestClass", runResult.Diagnostics[0].GetMessage(formatProvider: null));
    }

    [Fact]
    public void GenerateSources_WithExceptionHandler_ContinuesAfterException()
    {
        var throwingWriter = new ThrowingCodeWriter();
        var successWriter = new TestCodeWriter();
        var dispatcher = new CodeWriterDispatcher<TestModel>(
            exceptionHandler: (_, _) =>
                Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "TEST001",
                        "Test",
                        "Test",
                        "Test",
                        DiagnosticSeverity.Error,
                        true
                    ),
                    Location.None
                ),
            throwingWriter,
            successWriter
        );
        var models = new[] { new TestModel("TestClass") };
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(models, context)
        );

        Assert.Single(runResult.GeneratedTrees);
        Assert.EndsWith("TestClass.g.cs", runResult.GeneratedTrees[0].FilePath);
        Assert.Single(runResult.Diagnostics);
        Assert.Equal("TEST001", runResult.Diagnostics[0].Id);
    }

    [Fact]
    public void GenerateSources_WithMultipleModels_ContinuesAfterExceptionForOneModel()
    {
        var codeWriter = new TestCodeWriter();
        var dispatcher = new CodeWriterDispatcher<TestModel>(
            exceptionHandler: (_, _) =>
                Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "TEST001",
                        "Test",
                        "Test",
                        "Test",
                        DiagnosticSeverity.Error,
                        true
                    ),
                    Location.None
                ),
            codeWriters: codeWriter
        );
        var models = new[]
        {
            new TestModel("Class1"),
            new TestModel("Throw"),
            new TestModel("Class2"),
        };
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(models, context)
        );

        Assert.Equal(2, runResult.GeneratedTrees.Length);
        Assert.Contains(
            runResult.GeneratedTrees,
            s => s.FilePath.EndsWith("Class1.g.cs", StringComparison.Ordinal)
        );
        Assert.Contains(
            runResult.GeneratedTrees,
            s => s.FilePath.EndsWith("Class2.g.cs", StringComparison.Ordinal)
        );
        Assert.DoesNotContain(
            runResult.GeneratedTrees,
            s => s.FilePath.EndsWith("Throw.g.cs", StringComparison.Ordinal)
        );
        Assert.Single(runResult.Diagnostics);
        Assert.Equal("TEST001", runResult.Diagnostics[0].Id);
    }

    [Fact]
    public void GenerateSources_WhenCancellationRequested_ThrowsOperationCanceledException()
    {
        var codeWriter = new TestCodeWriter();
        var dispatcher = new CodeWriterDispatcher<TestModel>([codeWriter]);
        var models = new[] { new TestModel("TestClass") };
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        Assert.Throws<OperationCanceledException>(() =>
            SourceProductionContextRunner.Execute(
                context => dispatcher.GenerateSources(models, context),
                cts.Token
            )
        );
    }

    private sealed record TestModel(string Name);

    private sealed class TestCodeWriter : ICodeWriter<TestModel>
    {
        private readonly bool _shouldGenerate;
        private readonly string _suffix;

        public TestCodeWriter(bool shouldGenerate = true, string suffix = "")
        {
            _shouldGenerate = shouldGenerate;
            _suffix = suffix;
        }

        public bool CanGenerateFor(TestModel model) => _shouldGenerate && model.Name != "Skip";

        public string GetFileName(TestModel model) => $"{model.Name}{_suffix}.g.cs";

        public void Write(SourceTextWriter writer, TestModel model)
        {
            if (model.Name == "Throw")
                throw new InvalidOperationException("Test exception");

            writer.Write($"// {model.Name}{_suffix}");
        }
    }

    private sealed class ThrowingCodeWriter : ICodeWriter<TestModel>
    {
        public bool CanGenerateFor(TestModel model) => true;

        public string GetFileName(TestModel model) => $"{model.Name}.g.cs";

        public void Write(SourceTextWriter writer, TestModel model)
        {
            throw new InvalidOperationException("Test exception");
        }
    }
}
