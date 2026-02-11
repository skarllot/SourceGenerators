#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;

namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>
/// Provides helper methods for throwing exceptions.
/// </summary>
#if !IS_COMPILED
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.Generators.InterpolationCodeWriter", GeneratorInfo.Version)]
#endif
internal static class Throws
{
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="paramName">The name of the parameter that caused the exception.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="condition"/> is true.</exception>
    public static void ArgIf(bool condition, string message, string paramName)
    {
        if (condition)
            throw new ArgumentException(message, paramName);
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the specified argument is null.
    /// </summary>
    /// <param name="argument">The argument to check for null.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is null.</exception>
    public static void IfNull([NotNull] object? argument, string paramName)
    {
        if (argument == null)
            throw new ArgumentNullException(paramName);
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="message">The message to include in the exception.</param>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="condition"/> is true.</exception>
    public static void InvalidOperationIf([DoesNotReturnIf(true)] bool condition, string message)
    {
        if (condition)
            throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the specified value is negative.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is negative.</exception>
    public static void OutOfRangeIfNegative(int value, string paramName)
    {
#if NETSTANDARD2_0
        if (value < 0)
            throw new ArgumentOutOfRangeException(paramName, value, $"The value must be non-negative. Actual: {value}");
#else
        ArgumentOutOfRangeException.ThrowIfNegative(value, paramName);
#endif
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the specified value is greater than another value.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="other">The value to compare against.</param>
    /// <param name="paramName">The name of the parameter being checked.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is greater than <paramref name="other"/>.</exception>
    public static void OutOfRangeIfGreaterThan(int value, int other, string paramName)
    {
#if NETSTANDARD2_0
        if (value > other)
            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                $"The value must be less than or equal to {other}. Actual: {value}"
            );
#else
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, other, paramName);
#endif
    }
}
