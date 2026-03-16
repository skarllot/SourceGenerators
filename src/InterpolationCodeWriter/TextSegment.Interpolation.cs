#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Raiqub.Generators.InterpolationCodeWriter;

[InterpolatedStringHandler]
partial struct TextSegment
{
    /// <summary>Initializes a new <see cref="TextSegment"/> interpolated string handler using <see cref="System.Globalization.CultureInfo.InvariantCulture"/>.</summary>
    /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
    /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
    public TextSegment(int literalLength, int formattedCount)
        : this(literalLength, formattedCount, provider: null) { }

    /// <summary>Initializes a new <see cref="TextSegment"/> interpolated string handler using the specified format provider.</summary>
    /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
    /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
    /// <param name="provider">
    /// The format provider used to format interpolated values, or <see langword="null"/> to use
    /// <see cref="System.Globalization.CultureInfo.InvariantCulture"/>.
    /// </param>
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public TextSegment(int literalLength, int formattedCount, IFormatProvider? provider)
    {
        _provider = provider ?? CultureInfo.InvariantCulture;
        _length = 0;
        _appendLine = false;

        var capacity = formattedCount * 2 + 1;
        _parts = new Item[capacity];
    }

    /// <summary>Appends a literal string part to the segment.</summary>
    /// <param name="s">The literal string to append.</param>
    public void AppendLiteral(string s)
    {
        _parts[_length++] = s;
    }

    #region Generic formatting

    /// <summary>Appends the default string representation of a value to the segment.</summary>
    /// <typeparam name="T">The type of the value to append.</typeparam>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted<T>(T value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a value to the segment.</summary>
    /// <typeparam name="T">The type of the value to append.</typeparam>
    /// <param name="value">The value to append.</param>
    /// <param name="format">
    /// The format string to pass to <see cref="IFormattable.ToString(string?, IFormatProvider?)"/> when
    /// <paramref name="value"/> implements <see cref="IFormattable"/>; otherwise ignored.
    /// </param>
    public void AppendFormatted<T>(T value, string? format)
    {
        var str = value is IFormattable formattable ? formattable.ToString(format, _provider) : value?.ToString();
        _parts[_length++] = str;
    }

    /// <summary>Appends the string representation of a <see cref="bool"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(bool value)
    {
        _parts[_length++] = value.ToString(_provider);
    }

    #endregion

    #region Primitive formatting

    /// <summary>Appends the string representation of a <see cref="char"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(char value)
    {
        _parts[_length++] = value.ToString(_provider);
    }

    /// <summary>Appends the string representation of a <see cref="byte"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(byte value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="byte"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(byte value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of an <see cref="sbyte"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(sbyte value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of an <see cref="sbyte"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(sbyte value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="short"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(short value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="short"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(short value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="ushort"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(ushort value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="ushort"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(ushort value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of an <see cref="int"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(int value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of an <see cref="int"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(int value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="uint"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(uint value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="uint"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(uint value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="long"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(long value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="long"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(long value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="ulong"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(ulong value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="ulong"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(ulong value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="float"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(float value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="float"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(float value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="double"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(double value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="double"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(double value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="decimal"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(decimal value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="decimal"/> value to the segment.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(decimal value, string? format)
    {
        _parts[_length++] = value.ToString(format, _provider);
    }

    #endregion

    #region Nullable primitive formatting

    /// <summary>Appends the string representation of a <see cref="bool"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(bool? value)
    {
        _parts[_length++] = value?.ToString(_provider);
    }

    /// <summary>Appends the string representation of a <see cref="char"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(char? value)
    {
        _parts[_length++] = value?.ToString(_provider);
    }

    /// <summary>Appends the string representation of a <see cref="byte"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(byte? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="byte"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(byte? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of an <see cref="sbyte"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(sbyte? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of an <see cref="sbyte"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(sbyte? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="short"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(short? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="short"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(short? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="ushort"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(ushort? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="ushort"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(ushort? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of an <see cref="int"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(int? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of an <see cref="int"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(int? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="uint"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(uint? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="uint"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(uint? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="long"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(long? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="long"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(long? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="ulong"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(ulong? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="ulong"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(ulong? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="float"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(float? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="float"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(float? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="double"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(double? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="double"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(double? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    /// <summary>Appends the string representation of a <see cref="decimal"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted(decimal? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of a <see cref="decimal"/> value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">A standard or custom numeric format string.</param>
    public void AppendFormatted(decimal? value, string? format)
    {
        _parts[_length++] = value?.ToString(format, _provider);
    }

    #endregion

    #region String formatting

    /// <summary>Appends a string value to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The string to append.</param>
    public void AppendFormatted(string? value)
    {
        _parts[_length++] = value;
    }

    #endregion

    /// <summary>Appends the string representation of an object to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The object to append.</param>
    public void AppendFormatted(object? value)
    {
        AppendFormatted(value, format: null);
    }

    /// <summary>Appends the formatted string representation of an object to the segment, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The object to append.</param>
    /// <param name="format">
    /// The format string to pass to <see cref="IFormattable.ToString(string?, IFormatProvider?)"/> when
    /// <paramref name="value"/> implements <see cref="IFormattable"/>; otherwise ignored.
    /// </param>
    public void AppendFormatted(object? value, string? format)
    {
        var str = value is IFormattable formattable ? formattable.ToString(format, _provider) : value?.ToString();
        _parts[_length++] = str;
    }

    /// <summary>Appends a <see cref="TextSegment"/> as a nested part of this segment.</summary>
    /// <param name="value">The segment to append.</param>
    public void AppendFormatted(in TextSegment value)
    {
        _parts[_length++] = value;
    }

    /// <summary>Appends a <see cref="TextSegment"/> as a nested part of this sequence, or nothing if the value is <see langword="null"/>.</summary>
    /// <param name="value">The segment to append.</param>
    public void AppendFormatted(in TextSegment? value)
    {
        _parts[_length++] = value;
    }
}
