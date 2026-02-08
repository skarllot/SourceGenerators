#nullable enable

using System;
using System.Globalization;
using System.Text;

namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>Provides a text writer for generating source code with automatic indentation support.</summary>
#if !IS_COMPILED
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.CodeDom.Compiler.GeneratedCodeAttribute(
    "Raiqub.Generators.InterpolationCodeWriter",
    GeneratorInfo.Version
)]
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
        int charsPerIndentation = SourceTextWriter.DefaultCharsPerIndentation,
        IFormatProvider? formatProvider = null
    )
    {
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
        : this(
            new StringBuilder(DefaultStringBuilderCapacity),
            newLine,
            charsPerIndentation,
            formatProvider
        ) { }

    /// <summary>Remove all characters from the writer state.</summary>
    public void ClearAllText()
    {
        _builder.Clear();
        ClearIndent();
    }

    /// <summary>Remove the specified number of characters from written text.</summary>
    /// <param name="numberOfChars">The number of characters to remove from the end of the string.</param>
    public void Rewind(int numberOfChars)
    {
        _builder.Length -= numberOfChars;
    }

    /// <summary>Converts the written text to a string and clears the writer state.</summary>
    /// <returns>A string containing all written text.</returns>
    public override string ToString()
    {
        var text = _builder.ToString();
        ClearAllText();
        return text;
    }
}
