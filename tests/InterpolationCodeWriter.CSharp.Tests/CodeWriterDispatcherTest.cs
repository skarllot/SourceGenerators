using Microsoft.CodeAnalysis;
using Raiqub.Generators.InterpolationCodeWriter.CSharp.Tests.Fakes;

namespace Raiqub.Generators.InterpolationCodeWriter.CSharp.Tests;

public class CodeWriterDispatcherTest
{
    [Fact]
    public void GenerateSources_WithNoCodeWriters_DoesNothing()
    {
        var dispatcher = new CodeWriterDispatcher([]);
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(context)
        );

        Assert.Empty(runResult.GeneratedTrees);
        Assert.Empty(runResult.Diagnostics);
    }

    [Fact]
    public void GenerateSources_WithSingleCodeWriter_GeneratesSource()
    {
        var codeWriter = new TestCodeWriter("Test.g.cs", "// Test content");
        var dispatcher = new CodeWriterDispatcher([codeWriter]);
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(context)
        );

        Assert.Single(runResult.GeneratedTrees);
        Assert.EndsWith("Test.g.cs", runResult.GeneratedTrees[0].FilePath);
        Assert.Contains("// Test content", runResult.GeneratedTrees[0].GetText().ToString());
        Assert.Empty(runResult.Diagnostics);
    }

    [Fact]
    public void GenerateSources_WithMultipleCodeWriters_GeneratesAllSources()
    {
        var codeWriter1 = new TestCodeWriter("Test1.g.cs", "// Test 1");
        var codeWriter2 = new TestCodeWriter("Test2.g.cs", "// Test 2");
        var dispatcher = new CodeWriterDispatcher([codeWriter1, codeWriter2]);
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(context)
        );

        Assert.Equal(2, runResult.GeneratedTrees.Length);
        Assert.Contains(
            runResult.GeneratedTrees,
            s => s.FilePath.EndsWith("Test1.g.cs", StringComparison.Ordinal)
        );
        Assert.Contains(
            runResult.GeneratedTrees,
            s => s.FilePath.EndsWith("Test2.g.cs", StringComparison.Ordinal)
        );
        Assert.Empty(runResult.Diagnostics);
    }

    [Fact]
    public void GenerateSources_WhenCodeWriterThrowsWithoutHandler_ReportsDiagnostic()
    {
        var codeWriter = new ThrowingCodeWriter();
        var dispatcher = new CodeWriterDispatcher([codeWriter]);

        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(context)
        );

        Assert.Empty(runResult.GeneratedTrees);
        Assert.Single(runResult.Diagnostics);
        Assert.Equal("CS8785", runResult.Diagnostics[0].Id); // Generator failed to generate source
    }

    [Fact]
    public void GenerateSources_WhenCodeWriterThrowsWithHandler_ReportsDiagnostic()
    {
        var codeWriter = new ThrowingCodeWriter();
        var dispatcher = new CodeWriterDispatcher(
            exceptionHandler: ex =>
                Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "TEST001",
                        "Test Error",
                        ex.Message,
                        "Test",
                        DiagnosticSeverity.Error,
                        true
                    ),
                    Location.None
                ),
            codeWriters: codeWriter
        );
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(context)
        );

        Assert.Empty(runResult.GeneratedTrees);
        Assert.Single(runResult.Diagnostics);
        Assert.Equal("TEST001", runResult.Diagnostics[0].Id);
        Assert.Contains(
            "Test exception",
            runResult.Diagnostics[0].GetMessage(formatProvider: null)
        );
    }

    [Fact]
    public void GenerateSources_WithExceptionHandler_ContinuesAfterException()
    {
        var throwingWriter = new ThrowingCodeWriter();
        var successWriter = new TestCodeWriter("Success.g.cs", "// Success");
        var dispatcher = new CodeWriterDispatcher(
            exceptionHandler: _ =>
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
        var runResult = SourceProductionContextRunner.Execute(context =>
            dispatcher.GenerateSources(context)
        );

        Assert.Single(runResult.GeneratedTrees);
        Assert.EndsWith("Success.g.cs", runResult.GeneratedTrees[0].FilePath);
        Assert.Single(runResult.Diagnostics);
        Assert.Equal("TEST001", runResult.Diagnostics[0].Id);
    }

    [Fact]
    public void GenerateSources_WhenCancellationRequested_ThrowsOperationCanceledException()
    {
        var codeWriter = new TestCodeWriter("Test.g.cs", "// Test");
        var dispatcher = new CodeWriterDispatcher([codeWriter]);
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        Assert.Throws<OperationCanceledException>(() =>
            SourceProductionContextRunner.Execute(
                context => dispatcher.GenerateSources(context),
                cts.Token
            )
        );
    }

    private sealed class TestCodeWriter : ICodeWriter
    {
        private readonly string _fileName;
        private readonly string _content;

        public TestCodeWriter(string fileName, string content)
        {
            _fileName = fileName;
            _content = content;
        }

        public string GetFileName() => _fileName;

        public void Write(SourceTextWriter writer)
        {
            writer.Write(_content);
        }
    }

    private sealed class ThrowingCodeWriter : ICodeWriter
    {
        public string GetFileName() => "Throw.g.cs";

        public void Write(SourceTextWriter writer)
        {
            throw new InvalidOperationException("Test exception");
        }
    }
}
