#nullable enable

using System;
using System.Globalization;
using System.Text;

namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>Provides a text writer for generating source code with automatic indentation support.</summary>
#if !IS_COMPILED
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.Generators.InterpolationCodeWriter", GeneratorInfo.Version)]
#endif
public sealed partial class SourceTextWriter
{
    private const int DefaultStringBuilderCapacity = 1024;
    private readonly IFormatProvider _formatProvider;
    private readonly StringBuilder _builder;
    private readonly string _newLine;
    private readonly int _charsPerIndentation;

    /// <summary>Initializes a new instance of the <see cref="SourceTextWriter"/> class.</summary>
    /// <param name="builder">The <see cref="StringBuilder"/> used to accumulate the generated text.</param>
    /// <param name="newLine">The string to use for line termination. Defaults to "\n".</param>
    /// <param name="charsPerIndentation">The number of characters to use per indentation level. Defaults to <see cref="DefaultCharsPerIndentation"/>.</param>
    /// <param name="formatProvider">An object that supplies culture-specific formatting information. Defaults to <see cref="CultureInfo.InvariantCulture"/>.</param>
    public SourceTextWriter(
        StringBuilder builder,
        string newLine = "\n",
        int charsPerIndentation = DefaultCharsPerIndentation,
        IFormatProvider? formatProvider = null
    )
    {
        Throws.IfNull(builder, nameof(builder));
        Throws.IfNull(newLine, nameof(newLine));
        Throws.ArgIf(newLine.Length is not (1 or 2), "New line string must be 1 or 2 characters.", nameof(newLine));
        Throws.OutOfRangeIfNegative(charsPerIndentation, nameof(charsPerIndentation));

        _builder = builder;
        _newLine = newLine;
        _charsPerIndentation = charsPerIndentation;
        _formatProvider = formatProvider ?? CultureInfo.InvariantCulture;
    }

    /// <summary>Initializes a new instance of the <see cref="SourceTextWriter"/> class with a new <see cref="StringBuilder"/>.</summary>
    /// <param name="newLine">The string to use for line termination. Defaults to "\n".</param>
    /// <param name="charsPerIndentation">The number of characters to use per indentation level. Defaults to <see cref="DefaultCharsPerIndentation"/>.</param>
    /// <param name="formatProvider">An object that supplies culture-specific formatting information. Defaults to <see cref="CultureInfo.InvariantCulture"/>.</param>
    public SourceTextWriter(
        string newLine = "\n",
        int charsPerIndentation = DefaultCharsPerIndentation,
        IFormatProvider? formatProvider = null
    )
        : this(new StringBuilder(DefaultStringBuilderCapacity), newLine, charsPerIndentation, formatProvider) { }

    /// <summary>Gets the format provider used for formatting.</summary>
    public IFormatProvider FormatProvider => _formatProvider;

    /// <summary>Resets the writer to its initial state, clearing all written text and indentation.</summary>
    public void Reset()
    {
        _builder.Clear();
        ClearIndent();
    }

    /// <summary>Remove the specified number of characters from written text.</summary>
    /// <param name="numberOfChars">The number of characters to remove from the end of the string.</param>
    /// <exception cref="ArgumentOutOfRangeException">The number of characters to remove is negative or exceeds the length of the written text.</exception>
    public void Rewind(int numberOfChars)
    {
        Throws.OutOfRangeIfNegative(numberOfChars, nameof(numberOfChars));
        Throws.OutOfRangeIfGreaterThan(numberOfChars, _builder.Length, nameof(numberOfChars));

        _builder.Length -= numberOfChars;
    }

    /// <summary>Converts the written text to a string and resets the writer state.</summary>
    /// <returns>A string containing all written text.</returns>
    public string ToStringAndReset()
    {
        var text = _builder.ToString();
        Reset();
        return text;
    }

    /// <summary>Converts the written text to a string.</summary>
    /// <returns>A string containing all written text.</returns>
    public override string ToString() => _builder.ToString();
}
