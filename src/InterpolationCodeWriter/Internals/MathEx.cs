using System;
using System.Runtime.CompilerServices;

namespace Raiqub.Generators.InterpolationCodeWriter.Internals;

internal static class MathEx
{
    /// <summary>
    /// Clamps a value to be within the specified minimum and maximum range.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum allowable value.</param>
    /// <param name="max">The maximum allowable value.</param>
    /// <returns>The clamped value within the range [<paramref name="min"/>, <paramref name="max"/>].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Clamp(uint value, uint min, uint max)
    {
#if NETSTANDARD2_0
        Throws.MinMaxExceptionIf(min, max);

        if (value < min)
            return min;
        if (value > max)
            return max;
        return value;
#else
        return Math.Clamp(value, min, max);
#endif
    }
}
