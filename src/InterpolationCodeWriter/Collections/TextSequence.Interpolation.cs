#nullable enable

using System;
using System.Buffers;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Raiqub.Generators.InterpolationCodeWriter.Collections;

public partial struct TextSequence
{
    /// <summary>
    /// Interpolated string handler that accumulates string parts for constructing a <see cref="TextSequence"/>.
    /// </summary>
    /// <remarks>
    /// This type is used implicitly by the compiler when an interpolated string is passed to
    /// <see cref="TextSequence.Create(ref CreateInterpolatedStringHandler, bool)"/> or
    /// <see cref="TextSequence.Create(IFormatProvider?, ref CreateInterpolatedStringHandler, bool)"/>.
    /// </remarks>
    [InterpolatedStringHandler]
    public struct CreateInterpolatedStringHandler
    {
        private readonly IFormatProvider? _provider;
        private readonly Item[] _parts;
        private readonly bool _isRented;
        private int _length;

        /// <summary>
        /// Initializes a new instance of <see cref="CreateInterpolatedStringHandler"/> using
        /// <see cref="CultureInfo.InvariantCulture"/> as the format provider.
        /// </summary>
        /// <param name="literalLength">The total length of literal character spans in the interpolated string. Unused.</param>
        /// <param name="formattedCount">The number of interpolated holes in the interpolated string.</param>
        public CreateInterpolatedStringHandler(int literalLength, int formattedCount)
            : this(literalLength, formattedCount, provider: null) { }

        /// <summary>Initializes a new instance of <see cref="CreateInterpolatedStringHandler"/>.</summary>
        /// <param name="literalLength">The total length of literal character spans in the interpolated string. Unused.</param>
        /// <param name="formattedCount">The number of interpolated holes in the interpolated string.</param>
        /// <param name="provider">
        /// The format provider used to format interpolated values, or <see langword="null"/> to use
        /// <see cref="CultureInfo.InvariantCulture"/>.
        /// </param>
        public CreateInterpolatedStringHandler(int literalLength, int formattedCount, IFormatProvider? provider)
        {
            _provider = provider ?? CultureInfo.InvariantCulture;
            var capacity = formattedCount * 2 + 1;
            _length = 0;
            switch (capacity)
            {
                case < ArrayPoolThreshold:
                    _parts = new Item[capacity];
                    _isRented = false;
                    break;
                default:
                    _parts = ArrayPool<Item>.Shared.Rent(capacity);
                    _isRented = true;
                    break;
            }
        }

        /// <summary>Appends a literal string part to the sequence.</summary>
        /// <param name="s">The literal string to append.</param>
        public void AppendLiteral(string s)
        {
            _parts[_length++] = s;
        }

        /// <summary>Appends the default string representation of a value to the sequence.</summary>
        /// <typeparam name="T">The type of the value to append.</typeparam>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted<T>(T value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a value to the sequence.</summary>
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

        /// <summary>Appends the string representation of a <see cref="bool"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(bool value)
        {
            _parts[_length++] = value.ToString(_provider);
        }

        /// <summary>Appends the string representation of a <see cref="char"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(char value)
        {
            _parts[_length++] = value.ToString(_provider);
        }

        /// <summary>Appends the string representation of a <see cref="byte"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(byte value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="byte"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(byte value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of an <see cref="sbyte"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(sbyte value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of an <see cref="sbyte"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(sbyte value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="short"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(short value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="short"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(short value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="ushort"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(ushort value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="ushort"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(ushort value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of an <see cref="int"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(int value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of an <see cref="int"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(int value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="uint"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(uint value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="uint"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(uint value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="long"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(long value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="long"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(long value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="ulong"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(ulong value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="ulong"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(ulong value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="float"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(float value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="float"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(float value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="double"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(double value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="double"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(double value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="decimal"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(decimal value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="decimal"/> value to the sequence.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(decimal value, string? format)
        {
            _parts[_length++] = value.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="bool"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(bool? value)
        {
            _parts[_length++] = value?.ToString(_provider);
        }

        /// <summary>Appends the string representation of a <see cref="char"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(char? value)
        {
            _parts[_length++] = value?.ToString(_provider);
        }

        /// <summary>Appends the string representation of a <see cref="byte"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(byte? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="byte"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(byte? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of an <see cref="sbyte"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(sbyte? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of an <see cref="sbyte"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(sbyte? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="short"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(short? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="short"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(short? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="ushort"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(ushort? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="ushort"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(ushort? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of an <see cref="int"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(int? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of an <see cref="int"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(int? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="uint"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(uint? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="uint"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(uint? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="long"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(long? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="long"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(long? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="ulong"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(ulong? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="ulong"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(ulong? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="float"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(float? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="float"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(float? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="double"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(double? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="double"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(double? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends the string representation of a <see cref="decimal"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        public void AppendFormatted(decimal? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of a <see cref="decimal"/> value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The value to append.</param>
        /// <param name="format">A standard or custom numeric format string.</param>
        public void AppendFormatted(decimal? value, string? format)
        {
            _parts[_length++] = value?.ToString(format, _provider);
        }

        /// <summary>Appends a string value to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The string to append.</param>
        public void AppendFormatted(string? value)
        {
            _parts[_length++] = value;
        }

        /// <summary>Appends the string representation of an object to the sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The object to append.</param>
        public void AppendFormatted(object? value)
        {
            AppendFormatted(value, format: null);
        }

        /// <summary>Appends the formatted string representation of an object to the sequence, or nothing if the value is <see langword="null"/>.</summary>
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

        /// <summary>Appends a <see cref="TextSequence"/> as a nested part of this sequence.</summary>
        /// <param name="value">The sequence to append.</param>
        public void AppendFormatted(in TextSequence value)
        {
            _parts[_length++] = value;
        }

        /// <summary>Appends a <see cref="TextSequence"/> as a nested part of this sequence, or nothing if the value is <see langword="null"/>.</summary>
        /// <param name="value">The sequence to append.</param>
        public void AppendFormatted(in TextSequence? value)
        {
            _parts[_length++] = value;
        }

        /// <summary>Builds a <see cref="TextSequence"/> from the accumulated parts.</summary>
        /// <param name="appendLine">
        /// <see langword="true"/> to append a line terminator after writing all parts;
        /// <see langword="false"/> otherwise.
        /// </param>
        /// <returns>A new <see cref="TextSequence"/> containing the accumulated parts.</returns>
        public readonly TextSequence Build(bool appendLine)
        {
            return new TextSequence(_parts, _length, _isRented, appendLine);
        }
    }
}
