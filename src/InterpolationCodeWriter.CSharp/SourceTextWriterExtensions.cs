using System.Text;
using Microsoft.CodeAnalysis.Text;

namespace Raiqub.Generators.InterpolationCodeWriter.CSharp;

/// <summary>Provides extension methods for <see cref="SourceTextWriter"/>.</summary>
#if !IS_COMPILED
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.CodeDom.Compiler.GeneratedCodeAttribute(
    "Raiqub.Generators.InterpolationCodeWriter",
    GeneratorInfo.Version
)]
#endif
public static class SourceTextWriterExtensions
{
    /// <summary>Converts the written text to a <see cref="SourceText"/> instance and clears the writer state.</summary>
    /// <param name="writer">The <see cref="SourceTextWriter"/> instance to convert.</param>
    /// <returns>A <see cref="SourceText"/> containing all written text.</returns>
    public static SourceText ToSourceText(this SourceTextWriter writer)
    {
        return SourceText.From(writer.ToString(), Encoding.UTF8);
    }
}
