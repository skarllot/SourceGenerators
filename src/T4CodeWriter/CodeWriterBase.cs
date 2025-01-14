﻿// <auto-generated />
#nullable enable

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Raiqub.T4Template;

namespace Raiqub.Generators.T4CodeWriter
{
    /// <summary>Represents the base class for code writers.</summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.Generators.T4CodeWriter", "1.0.0.0")]
    public abstract class CodeWriterBase : T4TemplateBase
    {
        /// <summary>
        /// Initializes a new instance of the CodeWriterBase class with a StringBuilder.
        /// </summary>
        /// <param name="builder">The StringBuilder object used for building the template.</param>
        /// <param name="charsPerIndentation">The number of characters per indentation level.</param>
        protected CodeWriterBase(StringBuilder builder, int charsPerIndentation = CharsPerIndentation)
            : base(builder, charsPerIndentation)
        {
        }

        /// <summary>Retrieves the name of the file for the code of current code writer state.</summary>
        /// <returns>The name of the file.</returns>
        public abstract string GetFileName();

        /// <summary>Create the source text from current code writer state.</summary>
        /// <returns>The transformed text.</returns>
        public abstract override string TransformText();

        /// <summary>Adds a source to the compilation using the provided context.</summary>
        /// <param name="context">The source production context.</param>
        /// <remarks>
        /// This method takes a source production context and adds the source with the filename and transformed text
        /// to the compilation using the <see cref="SourceProductionContext.AddSource(string, SourceText)"/> method.
        /// </remarks>
        public void GenerateCompilationSource(SourceProductionContext context)
        {
            ClearAllText();
            context.AddSource(GetFileName(), SourceText.From(TransformText(), Encoding.UTF8));
        }
    }
}
