#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Raiqub.Generators.InterpolationCodeWriter.CSharp;

/// <summary>Represents a dispatcher for code writers that generate compilation source.</summary>
/// <typeparam name="T">The type associated with the code writer.</typeparam>
#if !IS_COMPILED
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.CodeDom.Compiler.GeneratedCodeAttribute(
    "Raiqub.Generators.InterpolationCodeWriter",
    GeneratorInfo.Version
)]
#endif
public class CodeWriterDispatcher<T>
{
    private readonly Func<Exception, T, Diagnostic>? _exceptionHandler;
    private readonly ICodeWriter<T>[] _codeWriters;

    /// <summary>Initializes a new instance of the <see cref="CodeWriterDispatcher{T}"/> class.</summary>
    /// <param name="codeWriters">An array of code writers.</param>
    public CodeWriterDispatcher(ICodeWriter<T>[] codeWriters)
    {
        _codeWriters = codeWriters;
    }

    /// <summary>Initializes a new instance of the <see cref="CodeWriterDispatcher{T}"/> class.</summary>
    /// <param name="exceptionHandler">An optional exception handler.</param>
    /// <param name="codeWriters">An array of code writers.</param>
    public CodeWriterDispatcher(
        Func<Exception, T, Diagnostic>? exceptionHandler,
        params ICodeWriter<T>[] codeWriters
    )
    {
        _exceptionHandler = exceptionHandler;
        _codeWriters = codeWriters;
    }

    /// <summary>Generates compilation sources for the specified models.</summary>
    /// <param name="models">The collection of models.</param>
    /// <param name="context">The incremental source generator context.</param>
    public void GenerateSources(IEnumerable<T> models, SourceProductionContext context)
    {
        GenerateSources<IEnumerable<T>>(models, context);
    }

    /// <summary>Generates compilation sources for the specified models.</summary>
    /// <param name="models">The collection of models.</param>
    /// <param name="context">The incremental source generator context.</param>
    public void GenerateSources(ImmutableArray<T> models, SourceProductionContext context)
    {
        GenerateSources<ImmutableArray<T>>(models, context);
    }

    /// <summary>Generates compilation sources for the specified models.</summary>
    /// <param name="models">The collection of models.</param>
    /// <param name="context">The incremental source generator context.</param>
    private void GenerateSources<TEnumerable>(TEnumerable models, SourceProductionContext context)
        where TEnumerable : IEnumerable<T>
    {
        var sourceTextWriter = new SourceTextWriter();

        foreach (var model in models)
        {
            foreach (var codeWriter in _codeWriters)
            {
                context.CancellationToken.ThrowIfCancellationRequested();
                try
                {
                    codeWriter.GenerateCompilationSource(context, sourceTextWriter, model);
                }
                catch (Exception e) when (_exceptionHandler is not null)
                {
                    context.ReportDiagnostic(_exceptionHandler(e, model));
                }
            }
        }
    }
}
