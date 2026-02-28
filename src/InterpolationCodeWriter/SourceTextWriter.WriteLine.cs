#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Raiqub.Generators.InterpolationCodeWriter;

public sealed partial class SourceTextWriter
{
    private bool EndsWithNewLine =>
        _builder.Length >= _newLine.Length
        && _newLine.Length switch
        {
            1 => _builder[^1] == _newLine[0],
            2 => _builder[^2] == _newLine[0] && _builder[^1] == _newLine[1],
            _ => throw new InvalidOperationException($"Invalid new line value: '{_newLine}'."),
        };

    /// <summary>Appends a new line into the generated output.</summary>
    public void WriteLine()
    {
        _builder.Append(_newLine);
    }

    /// <summary>Write boolean directly into the generated output and appends a new line.</summary>
    /// <param name="value">The boolean to be written.</param>
    public void WriteLine(bool value)
    {
        Write(value);
        _builder.Append(_newLine);
    }

    /// <summary>Write character directly into the generated output and appends a new line.</summary>
    /// <param name="value">The character to be written.</param>
    public void WriteLine(char value)
    {
        Write(value);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(byte number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(byte number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(sbyte number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(sbyte number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(short number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(short number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(ushort number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(ushort number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(int number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(int number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(uint number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(uint number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(long number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(long number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(ulong number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(ulong number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(float number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(float number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(double number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(double number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(decimal number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(decimal number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write boolean directly into the generated output and appends a new line.</summary>
    /// <param name="value">The boolean to be written.</param>
    public void WriteLine(bool? value)
    {
        Write(value);
        _builder.Append(_newLine);
    }

    /// <summary>Write character directly into the generated output and appends a new line.</summary>
    /// <param name="value">The character to be written.</param>
    public void WriteLine(char? value)
    {
        Write(value);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(byte? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(byte? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(sbyte? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(sbyte? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(short? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(short? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(ushort? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(ushort? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(int? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(int? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(uint? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(uint? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(long? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(long? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(ulong? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(ulong? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(float? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(float? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(double? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(double? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(decimal? number)
    {
        WriteLine(number, format: null);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(decimal? number, string? format)
    {
        Write(number, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write text directly into the generated output and appends a new line.</summary>
    /// <param name="textToAppend">The text to be written.</param>
    public void WriteLine(string? textToAppend)
    {
        Write(textToAppend);
        _builder.Append(_newLine);
    }

    /// <summary>Write text directly into the generated output and appends a new line.</summary>
    /// <param name="value">The text to be written.</param>
    public void WriteLine(object? value)
    {
        WriteLine(value, format: null);
    }

    /// <summary>Write text directly into the generated output and appends a new line.</summary>
    /// <param name="value">The text to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine(object? value, string? format)
    {
        Write(value, format);
        _builder.Append(_newLine);
    }

    /// <summary>Write the string representation of a specified value directly into the generated output and appends a new line.</summary>
    /// <typeparam name="T">The type of the value to write.</typeparam>
    /// <param name="value">The value to be written.</param>
    public void WriteLine<T>(T value)
    {
        WriteLine(value, format: null);
    }

    /// <summary>Write the string representation of a specified value directly into the generated output and appends a new line.</summary>
    /// <typeparam name="T">The type of the value to write.</typeparam>
    /// <param name="value">The value to be written.</param>
    /// <param name="format">The format string.</param>
    public void WriteLine<T>(T value, string? format)
    {
        Write(value, format);
        _builder.Append(_newLine);
    }

    /// <summary>Accepts an <see cref="EmptyResult"/> value and appends a new line to the generated output.</summary>
    /// <param name="value">The empty result value. Its content is ignored.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteLine([SuppressMessage("ReSharper", "UnusedParameter.Global")] EmptyResult value)
    {
        _builder.Append(_newLine);
    }

    /// <summary>Writes the specified interpolated string directly into the generated output and appends a new line.</summary>
    /// <param name="handler">The interpolated string to append.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public void WriteLine([InterpolatedStringHandlerArgument("")] ref WriteInterpolatedStringHandler handler)
    {
        // Text is written using interpolated string handler by compiler generated code
        _builder.Append(_newLine);
    }
}
