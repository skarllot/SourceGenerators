#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Raiqub.Generators.InterpolationCodeWriter;

public sealed partial class SourceTextWriter
{
    /// <summary>Write the string representation of a specified boolean value directly into the generated output.</summary>
    /// <param name="value">The boolean to be appended to the generated output.</param>
    public void Write(bool value)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(value.ToString(_formatProvider));
    }

    /// <summary>Write character directly into the generated output.</summary>
    /// <param name="value">The character to be appended to the generated output.</param>
    public void Write(char value)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(byte number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(sbyte number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(short number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(ushort number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(int number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(uint number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(long number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(ulong number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(float number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(double number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(decimal number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        _builder.Append(number.ToString(_formatProvider));
    }

    /// <summary>Write the string representation of a specified boolean value directly into the generated output.</summary>
    /// <param name="value">The boolean to be appended to the generated output.</param>
    public void Write(bool? value)
    {
        if (value is null)
        {
            return;
        }

        Write(value.Value);
    }

    /// <summary>Write character directly into the generated output.</summary>
    /// <param name="value">The character to be appended to the generated output.</param>
    public void Write(char? value)
    {
        if (value is null)
        {
            return;
        }

        Write(value.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(byte? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(sbyte? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(short? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(ushort? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(int? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(uint? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(long? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(ulong? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(float? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(double? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(decimal? number)
    {
        if (number is null)
        {
            return;
        }

        Write(number.Value);
    }

    /// <summary>Write the string representation of a specified object directly into the generated output.</summary>
    /// <param name="value">The object to be appended to the generated output.</param>
    public void Write(object? value)
    {
        switch (value)
        {
            case null:
                return;
            case IFormattable formattable:
                Write(formattable.ToString(null, _formatProvider));
                break;
            default:
                Write(value.ToString());
                break;
        }
    }

    /// <summary>Write the string representation of a specified value directly into the generated output.</summary>
    /// <typeparam name="T">The type of the value to write.</typeparam>
    /// <param name="value">The value to be appended to the generated output.</param>
    public void Write<T>(T value)
    {
        if (value is IFormattable formattable)
            Write(formattable.ToString(null, _formatProvider));
        else if (value is not null)
            Write(value.ToString());
    }

    /// <summary>Write text directly into the generated output.</summary>
    /// <param name="textToAppend">The text to be appended to the generated output.</param>
    public void Write(string? textToAppend)
    {
        Write(textToAppend.AsSpan());
    }

    /// <summary>Accepts an <see cref="EmptyResult"/> value and writes nothing to the generated output.</summary>
    /// <param name="value">The empty result value. Its content is ignored.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public void Write([SuppressMessage("ReSharper", "UnusedParameter.Global")] EmptyResult value)
    {
        // Nothing to write
    }

    /// <summary>Write text directly into the generated output.</summary>
    /// <param name="textToAppend">The text to be appended to the generated output.</param>
    public void Write(ReadOnlySpan<char> textToAppend)
    {
        if (textToAppend.IsEmpty)
        {
            return;
        }

        if (_builder.Length == 0)
        {
            textToAppend = textToAppend.TrimStart("\r\n".AsSpan());
        }

        var endsWithNewline = EndsWithNewLine;
        var lineIndex = 0;
#if NETSTANDARD2_0
        foreach (var line in new InternalSpanLineEnumerator(textToAppend))
#else
        foreach (var line in textToAppend.EnumerateLines())
#endif
        {
            if (lineIndex > 0)
                _builder.Append(_newLine);

            if (endsWithNewline && _indentation > 0 && line.Length > 0)
                WriteIndentation();

#if NETSTANDARD2_0
            StringBuilderMemory.Append(_builder, line);
#else
            _builder.Append(line);
#endif
            lineIndex++;
            endsWithNewline = true;
        }
    }

    /// <summary>Writes the specified interpolated string directly into the generated output.</summary>
    /// <param name="handler">The interpolated string to append.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public void Write([InterpolatedStringHandlerArgument("")] ref WriteInterpolatedStringHandler handler)
    {
        // Text is written using interpolated string handler by compiler generated code
    }
}
