#if NETSTANDARD2_0
#nullable enable
using System;

namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>
/// Provides extension methods for <see cref="ReadOnlySpan{T}"/> of <see cref="char"/>.
/// </summary>
#if !IS_COMPILED
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.Generators.InterpolationCodeWriter", GeneratorInfo.Version)]
#endif
internal static class ReadOnlySpanExtensions
{
    /// <summary>
    /// Returns an enumerator that iterates through the lines in the span.
    /// </summary>
    /// <param name="span">The span to enumerate.</param>
    /// <returns>An enumerator that can be used to iterate through the lines.</returns>
    public static SpanLineEnumerator EnumerateLines(this ReadOnlySpan<char> span)
    {
        return new SpanLineEnumerator(span);
    }
}
#endif
