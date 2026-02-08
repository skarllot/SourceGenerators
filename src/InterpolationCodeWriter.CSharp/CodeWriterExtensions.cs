using Microsoft.CodeAnalysis;

namespace Raiqub.Generators.InterpolationCodeWriter.CSharp;

/// <summary>
/// Provides extension methods for <see cref="ICodeWriter"/> and <see cref="ICodeWriter{T}"/> to simplify
/// source generation in Roslyn source generators.
/// </summary>
#if !IS_COMPILED
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.Generators.InterpolationCodeWriter", GeneratorInfo.Version)]
#endif
public static class CodeWriterExtensions
{
    /// <summary>
    /// Generates source code and adds it to the compilation context.
    /// </summary>
    /// <param name="writer">The code writer that generates the source code.</param>
    /// <param name="context">The source production context to add the generated source to.</param>
    /// <param name="textWriter">The text writer used to generate the source code.</param>
    public static void GenerateCompilationSource(
        this ICodeWriter writer,
        SourceProductionContext context,
        SourceTextWriter textWriter
    )
    {
        try
        {
            writer.Write(textWriter);
        }
        catch
        {
            textWriter.ClearAllText();
            throw;
        }

        context.AddSource(writer.GetFileName(), textWriter.ToSourceText());
    }

    /// <summary>
    /// Generates source code based on a model and adds it to the compilation context.
    /// </summary>
    /// <typeparam name="T">The type of the model used for code generation.</typeparam>
    /// <param name="writer">The code writer that generates the source code.</param>
    /// <param name="context">The source production context to add the generated source to.</param>
    /// <param name="textWriter">The text writer used to generate the source code.</param>
    /// <param name="model">The model containing the data for code generation.</param>
    /// <remarks>
    /// This method checks if code should be generated for the model using <see cref="ICodeWriter{T}.CanGenerateFor"/>
    /// before performing generation. If the check returns <see langword="false"/>, no source is added.
    /// </remarks>
    public static void GenerateCompilationSource<T>(
        this ICodeWriter<T> writer,
        SourceProductionContext context,
        SourceTextWriter textWriter,
        T model
    )
    {
        if (!writer.CanGenerateFor(model))
            return;

        try
        {
            writer.Write(textWriter, model);
        }
        catch
        {
            textWriter.ClearAllText();
            throw;
        }

        context.AddSource(writer.GetFileName(model), textWriter.ToSourceText());
    }
}
