#if NETSTANDARD2_0
#nullable enable
using System;

namespace Raiqub.Generators.InterpolationCodeWriter;

internal static class ReadOnlySpanExtensions
{
    public static SpanLineEnumerator EnumerateLines(this ReadOnlySpan<char> span)
    {
        return new SpanLineEnumerator(span);
    }
}
#endif
