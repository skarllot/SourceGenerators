#nullable enable

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Raiqub.Generators.InterpolationCodeWriter;

public sealed partial class SourceTextWriter
{
    private int _indentation;

    /// <summary>Write the string representation of a specified boolean value directly into the generated output.</summary>
    /// <param name="value">The boolean to be appended to the generated output.</param>
    public void Write(bool value)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{value}");
    }

    /// <summary>Write character directly into the generated output.</summary>
    /// <param name="value">The character to be appended to the generated output.</param>
    public void Write(char value)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{value}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(byte number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(sbyte number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(short number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(ushort number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(int number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(uint number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(long number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(ulong number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(float number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(double number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
    }

    /// <summary>Write number directly into the generated output.</summary>
    /// <param name="number">The number to be appended to the generated output.</param>
    public void Write(decimal number)
    {
        if (_indentation > 0 && EndsWithNewLine)
            WriteIndentation();

        WriteFormatted(_builder, _formatProvider, $"{number}");
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
        if (value is null)
        {
            return;
        }

        Write(value.ToString());
    }

    /// <summary>Write text directly into the generated output.</summary>
    /// <param name="textToAppend">The text to be appended to the generated output.</param>
    public void Write(string? textToAppend)
    {
        Write(textToAppend.AsSpan());
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

        if (_indentation == 0)
        {
            _builder.Append(textToAppend);
            return;
        }

        var endsWithNewline = EndsWithNewLine;
        var lineIndex = 0;
        foreach (var line in textToAppend.EnumerateLines())
        {
            if (lineIndex > 0)
                _builder.Append(_newLine);

            if (endsWithNewline)
                WriteIndentation();

            _builder.Append(line);
            lineIndex++;
            endsWithNewline = true;
        }
    }

    /// <summary>Writes the specified interpolated string directly into the generated output.</summary>
    /// <param name="handler">The interpolated string to append.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(
        [InterpolatedStringHandlerArgument("")] ref WriteInterpolatedStringHandler handler
    )
    {
        // Text is written using interpolated string handler by compiler generated code
        Debug.Assert(_builder != null);
    }

#if NETSTANDARD2_0
    private static void WriteFormatted(
        StringBuilder builder,
        IFormatProvider? provider,
        [InterpolatedStringHandlerArgument("builder", "provider")]
            ref AppendInterpolatedStringHandler handler
    )
    {
        // Text is written using interpolated string handler by compiler generated code
        Debug.Assert(builder != null);
        Debug.Assert(provider != null || provider == null);
        Debug.Assert(handler.GetType() != null);
    }
#else
    private static void WriteFormatted(
        StringBuilder builder,
        IFormatProvider? provider,
        [InterpolatedStringHandlerArgument("builder", "provider")]
            ref StringBuilder.AppendInterpolatedStringHandler handler
    )
    {
        // Text is written using interpolated string handler by compiler generated code
        Debug.Assert(handler.GetType() != null);
    }
#endif
}
