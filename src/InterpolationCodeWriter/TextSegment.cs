#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>
/// Represents a segment of string parts that can be written to a <see cref="SourceTextWriter"/>.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="TextSegment"/> is itself an
/// <see cref="System.Runtime.CompilerServices.InterpolatedStringHandlerAttribute">interpolated string handler</see>,
/// so instances can be created directly from an interpolated string expression
/// (e.g. <c>TextSegment seg = $"Hello {name}";</c>), or via
/// <see cref="Create(ref TextSegment, bool)"/> and
/// <see cref="Create(IFormatProvider?, ref TextSegment, bool)"/> when an append-line flag
/// or a custom <see cref="IFormatProvider"/> are needed.
/// </para>
/// <para>
/// During construction the compiler calls <c>AppendLiteral</c> and <c>AppendFormatted</c>
/// to accumulate parts, so the instance is mutated while the interpolated string is being
/// evaluated. Once construction is complete the segment is not modified further and can be
/// treated as effectively immutable for reading and writing purposes.
/// </para>
/// </remarks>
[DebuggerDisplay("{DebuggerDisplay}")]
public partial struct TextSegment
{
    private readonly IFormatProvider _provider;
    private readonly Item[] _parts;
    private bool _appendLine;
    private int _length;

    /// <summary>Gets a value indicating whether a line terminator should be appended after writing all parts.</summary>
    public readonly bool AppendLine => _appendLine;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string DebuggerDisplay
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var item in _parts.AsSpan(0, _length))
            {
                sb.Append(item.ToString());
            }

            if (_appendLine)
            {
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    /// <summary>Creates a <see cref="TextSegment"/> from an interpolated string.</summary>
    /// <param name="handler">The interpolated string handler that holds the accumulated parts.</param>
    /// <param name="appendLine">
    /// <see langword="true"/> to append a line terminator after writing all parts;
    /// <see langword="false"/> otherwise. Defaults to <see langword="false"/>.
    /// </param>
    /// <returns>A new <see cref="TextSegment"/> built from the interpolated string.</returns>
    public static ref TextSegment Create(
        [InterpolatedStringHandlerArgument] ref TextSegment handler,
        bool appendLine = false
    )
    {
        handler._appendLine = appendLine;
        return ref handler;
    }

    /// <summary>
    /// Creates a <see cref="TextSegment"/> from an interpolated string using the specified format provider.
    /// </summary>
    /// <param name="provider">
    /// The format provider used to format interpolated values, or <see langword="null"/> to use
    /// <see cref="System.Globalization.CultureInfo.InvariantCulture"/>.
    /// </param>
    /// <param name="handler">The interpolated string handler that holds the accumulated parts.</param>
    /// <param name="appendLine">
    /// <see langword="true"/> to append a line terminator after writing all parts;
    /// <see langword="false"/> otherwise. Defaults to <see langword="false"/>.
    /// </param>
    /// <returns>A new <see cref="TextSegment"/> built from the interpolated string.</returns>
    public static ref TextSegment Create(
        IFormatProvider? provider,
        [InterpolatedStringHandlerArgument("provider")] ref TextSegment handler,
        bool appendLine = false
    )
    {
        handler._appendLine = appendLine;
        return ref handler;
    }

    /// <summary>
    /// Enumerates all string values in this segment into a list.
    /// </summary>
    /// <returns>A read-only list containing all string values, with <see langword="null"/> for empty parts.</returns>
    public readonly IReadOnlyList<string?> ToList()
    {
        var list = new List<string?>();
        FillList(list);
        return list;
    }

    /// <summary>
    /// Writes all parts of this segment to the specified <see cref="SourceTextWriter"/>.
    /// </summary>
    /// <param name="writer">The writer to which the parts are written.</param>
    public readonly void WriteTo(SourceTextWriter writer)
    {
        foreach (var item in _parts.AsSpan(0, _length))
        {
            item.WriteTo(writer);
        }

        if (_appendLine)
        {
            writer.WriteLine();
        }
    }

    private readonly void FillList(List<string?> list)
    {
        foreach (var item in _parts.AsSpan(0, _length))
        {
            item.FillList(list);
        }
    }

    private readonly struct Item
    {
        private readonly string? _text;
        private readonly TextSegment? _textSequence;

        private Item(string? text)
        {
            _text = text;
            _textSequence = null;
        }

        private Item(TextSegment? textSequence)
        {
            _textSequence = textSequence;
            _text = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Item(string? value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Item(in TextSegment? value) => new(value);

        public void WriteTo(SourceTextWriter writer)
        {
            if (_textSequence.HasValue)
            {
                _textSequence.Value.WriteTo(writer);
            }
            else
            {
                writer.Write(_text);
            }
        }

        public void FillList(List<string?> list)
        {
            if (_textSequence.HasValue)
            {
                _textSequence.Value.FillList(list);
            }
            else
            {
                list.Add(_text);
            }
        }

        public override string? ToString() => _textSequence.HasValue ? _textSequence.Value.DebuggerDisplay : _text;
    }
}
