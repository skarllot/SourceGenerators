#nullable enable

using System;
using Microsoft.CodeAnalysis;

namespace Raiqub.Generators.InterpolationCodeWriter.CSharp;

/// <summary>Represents a dispatcher for code writers that generate compilation source.</summary>
#if !IS_COMPILED
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.Generators.InterpolationCodeWriter", GeneratorInfo.Version)]
#endif
public sealed class CodeWriterDispatcher
{
    private readonly Func<Exception, Diagnostic>? _exceptionHandler;
    private readonly ICodeWriter[] _codeWriters;

    /// <summary>Initializes a new instance of the <see cref="CodeWriterDispatcher"/> class.</summary>
    /// <param name="codeWriters">An array of code writers.</param>
    public CodeWriterDispatcher(ICodeWriter[] codeWriters)
    {
        _codeWriters = codeWriters ?? throw new ArgumentNullException(nameof(codeWriters));
    }

    /// <summary>Initializes a new instance of the <see cref="CodeWriterDispatcher"/> class.</summary>
    /// <param name="exceptionHandler">An optional exception handler.</param>
    /// <param name="codeWriters">An array of code writers.</param>
    public CodeWriterDispatcher(Func<Exception, Diagnostic>? exceptionHandler, params ICodeWriter[] codeWriters)
    {
        _exceptionHandler = exceptionHandler;
        _codeWriters = codeWriters ?? throw new ArgumentNullException(nameof(codeWriters));
    }

    /// <summary>Generates compilation sources.</summary>
    /// <param name="context">The incremental source generator context.</param>
    public void GenerateSources(SourceProductionContext context)
    {
        var sourceTextWriter = new SourceTextWriter();

        foreach (var codeWriter in _codeWriters)
        {
            context.CancellationToken.ThrowIfCancellationRequested();
            try
            {
                codeWriter.GenerateCompilationSource(context, sourceTextWriter);
            }
            catch (Exception e) when (_exceptionHandler is not null)
            {
                context.ReportDiagnostic(_exceptionHandler(e));
            }
        }
    }
}
