#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;

namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>Represents an empty result that writes nothing to the output.</summary>
/// <remarks>
/// Use this type as the return value of a method called within an interpolated string
/// to indicate that the method handles writing directly to the <see cref="SourceTextWriter"/>
/// and produces no interpolated output itself.
/// </remarks>
public readonly record struct EmptyResult
#if NET8_0_OR_GREATER
    : ISpanFormattable, IUtf8SpanFormattable
#else
    : IFormattable
#endif
{
    /// <summary>Gets the singleton empty result value.</summary>
    [SuppressMessage("ReSharper", "UnassignedReadonlyField")]
    public static readonly EmptyResult Value;

    /// <summary>Returns an empty string.</summary>
    /// <returns>An empty string.</returns>
    public override string ToString() => string.Empty;

    /// <summary>Returns an empty string, ignoring the format and format provider.</summary>
    /// <param name="format">This parameter is ignored.</param>
    /// <param name="formatProvider">This parameter is ignored.</param>
    /// <returns>An empty string.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider) => string.Empty;

#if NET8_0_OR_GREATER
    /// <summary>Writes nothing to the destination span.</summary>
    /// <param name="destination">This parameter is ignored.</param>
    /// <param name="charsWritten">When this method returns, contains zero.</param>
    /// <param name="format">This parameter is ignored.</param>
    /// <param name="provider">This parameter is ignored.</param>
    /// <returns><see langword="true"/> always.</returns>
    public bool TryFormat(
        Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> format,
        IFormatProvider? provider
    )
    {
        charsWritten = 0;
        return true;
    }

    /// <summary>Writes nothing to the destination span.</summary>
    /// <param name="destination">This parameter is ignored.</param>
    /// <param name="bytesWritten">When this method returns, contains zero.</param>
    /// <param name="format">This parameter is ignored.</param>
    /// <param name="provider">This parameter is ignored.</param>
    /// <returns><see langword="true"/> always.</returns>
    [SuppressMessage("Naming", "CA1725:Parameter names should match base declaration")]
    public bool TryFormat(
        Span<byte> destination,
        out int bytesWritten,
        ReadOnlySpan<char> format,
        IFormatProvider? provider
    )
    {
        bytesWritten = 0;
        return true;
    }
#endif
}
