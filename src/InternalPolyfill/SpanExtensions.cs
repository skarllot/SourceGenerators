#nullable enable

using System;
using System.Runtime.CompilerServices;

namespace Raiqub.Generators.InterpolationCodeWriter.Internals;

#if !IS_COMPILED
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.Generators.InterpolationCodeWriter", GeneratorInfo.Version)]
#endif
internal static class SpanExtensions
{
    /// <summary>
    /// Returns an enumeration of lines over the provided span.
    /// </summary>
    /// <param name="span">The span to enumerate lines over.</param>
    /// <returns>A <see cref="SpanLineEnumerator"/> for the provided <paramref name="span"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SpanLineEnumerator EnumerateLines(this ReadOnlySpan<char> span)
    {
        return new SpanLineEnumerator(span);
    }
}
