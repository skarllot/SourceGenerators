using System;
using System.Text;

namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>
/// Provides methods for <see cref="StringBuilder"/> to work with <see cref="ReadOnlySpan{T}"/>.
/// </summary>
#if !IS_COMPILED
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.Generators.InterpolationCodeWriter", GeneratorInfo.Version)]
#endif
internal static class StringBuilderMemory
{
    /// <summary>
    /// Appends the string representation of a specified read-only character span to this instance.
    /// </summary>
    public static unsafe void Append(StringBuilder target, ReadOnlySpan<char> value)
    {
        if (value.Length <= 0)
        {
            return;
        }

        fixed (char* valueChars = value)
        {
            target.Append(valueChars, value.Length);
        }
    }
}
