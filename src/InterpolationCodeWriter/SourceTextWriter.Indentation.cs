#nullable enable

using System;

namespace Raiqub.Generators.InterpolationCodeWriter;

public sealed partial class SourceTextWriter
{
    /// <summary>The character used for indentation.</summary>
    private const char IndentationChar = ' ';

    /// <summary>The default number of characters per indentation level.</summary>
    private const int DefaultCharsPerIndentation = 4;

    /// <summary>Increase the indent.</summary>
    /// <param name="levels">The number of levels to increase.</param>
    public void PushIndent(int levels = 1)
    {
        _indentation += levels;
    }

    /// <summary>Remove the last indent that was added with <see cref="PushIndent"/>.</summary>
    /// <param name="levels">The number of levels to decrease.</param>
    public void PopIndent(int levels = 1)
    {
        if (_indentation - levels < 0)
        {
            Throw();

            static void Throw() =>
                throw new InvalidOperationException("Indentation cannot decrease below zero");
        }

        _indentation -= levels;
    }

    /// <summary>Remove any indentation.</summary>
    public void ClearIndent()
    {
        _indentation = 0;
    }

    /// <summary>Writes the current indentation to the output.</summary>
    private void WriteIndentation()
    {
        _builder.Append(IndentationChar, _charsPerIndentation * _indentation);
    }
}
