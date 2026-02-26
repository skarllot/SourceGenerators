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
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(sbyte number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(short number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(ushort number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(int number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(uint number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(long number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(ulong number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(float number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(double number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(decimal number)
    {
        Write(number);
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
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(sbyte? number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(short? number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(ushort? number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(int? number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(uint? number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(long? number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(ulong? number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(float? number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(double? number)
    {
        Write(number);
        _builder.Append(_newLine);
    }

    /// <summary>Write number directly into the generated output and appends a new line.</summary>
    /// <param name="number">The number to be written.</param>
    public void WriteLine(decimal? number)
    {
        Write(number);
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
        Write(value);
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
