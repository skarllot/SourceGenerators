#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Raiqub.Generators.InterpolationCodeWriter;

public sealed partial class SourceTextWriter
{
    /// <summary>Provides a handler for writing interpolated strings to a <see cref="SourceTextWriter"/>.</summary>
    [InterpolatedStringHandler]
    public readonly struct WriteInterpolatedStringHandler
    {
        private readonly SourceTextWriter _writer;

        /// <summary>Initializes a new instance of the <see cref="WriteInterpolatedStringHandler"/> struct.</summary>
        /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
        /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
        /// <param name="writer">The <see cref="SourceTextWriter"/> to write to.</param>
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        public WriteInterpolatedStringHandler(int literalLength, int formattedCount, SourceTextWriter writer)
        {
            _writer = writer;
        }

        /// <summary>Appends the literal part of the interpolated string.</summary>
        /// <param name="s">The literal string to append.</param>
        public void AppendLiteral(string s)
        {
            _writer.Write(s);
        }

        /// <summary>Appends a formatted expression to the interpolated string.</summary>
        /// <typeparam name="T">The type of the value to format.</typeparam>
        /// <param name="value">The value to format and append.</param>
        public void AppendFormatted<T>(T value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted boolean value to the interpolated string.</summary>
        /// <param name="value">The boolean value to append.</param>
        public void AppendFormatted(bool value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted character to the interpolated string.</summary>
        /// <param name="value">The character to append.</param>
        public void AppendFormatted(char value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(byte value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(sbyte value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(short value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(ushort value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(int value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(uint value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(long value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(ulong value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(float value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(double value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(decimal value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted boolean value to the interpolated string.</summary>
        /// <param name="value">The boolean value to append.</param>
        public void AppendFormatted(bool? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted character to the interpolated string.</summary>
        /// <param name="value">The character to append.</param>
        public void AppendFormatted(char? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(byte? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(sbyte? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(short? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(ushort? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(int? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(uint? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(long? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(ulong? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(float? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(double? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted number value to the interpolated string.</summary>
        /// <param name="value">The number value to append.</param>
        public void AppendFormatted(decimal? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a formatted string to the interpolated string.</summary>
        /// <param name="value">The string value to append.</param>
        public void AppendFormatted(string? value)
        {
            _writer.Write(value);
        }

        /// <summary>Appends a string representation of an object value to the interpolated string.</summary>
        /// <param name="value">The object value to append.</param>
        public void AppendFormatted(object? value)
        {
            _writer.Write(value);
        }
    }
}
