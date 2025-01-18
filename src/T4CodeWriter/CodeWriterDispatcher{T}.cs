﻿// <auto-generated />
#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Raiqub.Generators.T4CodeWriter
{
    /// <summary>Represents a dispatcher for code writers that generate compilation source.</summary>
    /// <typeparam name="T">The type associated with the code writer.</typeparam>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.Generators.T4CodeWriter", GeneratorInfo.Version)]
    public sealed class CodeWriterDispatcher<T>
    {
        private const int DefaultStringBuilderCapacity = 1024;
        private readonly Func<Exception, T, Diagnostic>? _exceptionHandler;
        private readonly Func<StringBuilder, CodeWriterBase<T>>[] _codeWriterFactories;

        /// <summary>Initializes a new instance of the <see cref="CodeWriterDispatcher{T}"/> class.</summary>
        /// <param name="codeWriterFactories">An array of code writer factories.</param>
        public CodeWriterDispatcher(Func<StringBuilder, CodeWriterBase<T>>[] codeWriterFactories)
        {
            _codeWriterFactories = codeWriterFactories;
        }

        /// <summary>Initializes a new instance of the <see cref="CodeWriterDispatcher{T}"/> class.</summary>
        /// <param name="exceptionHandler">An optional exception handler.</param>
        /// <param name="codeWriterFactories">An array of code writer factories.</param>
        public CodeWriterDispatcher(
            Func<Exception, T, Diagnostic>? exceptionHandler,
            params Func<StringBuilder, CodeWriterBase<T>>[] codeWriterFactories)
        {
            _exceptionHandler = exceptionHandler;
            _codeWriterFactories = codeWriterFactories;
        }

        /// <summary>Generates compilation sources for the specified models.</summary>
        /// <param name="models">The collection of models.</param>
        /// <param name="context">The incremental source generator context.</param>
        public void GenerateSources(IEnumerable<T> models, SourceProductionContext context)
        {
            var codeWriters = new CodeWriterBase<T>[_codeWriterFactories.Length];
            var sb = new StringBuilder(DefaultStringBuilderCapacity);

            for (int i = 0; i < _codeWriterFactories.Length; i++)
            {
                codeWriters[i] = _codeWriterFactories[i](sb);
            }

            foreach (var model in models)
            {
                foreach (var codeWriter in codeWriters)
                {
                    context.CancellationToken.ThrowIfCancellationRequested();
                    try
                    {
                        codeWriter.GenerateCompilationSource(context, model);
                    }
                    catch (Exception e) when (_exceptionHandler is not null)
                    {
                        context.ReportDiagnostic(_exceptionHandler(e, model));
                    }
                }
            }
        }
    }
}
