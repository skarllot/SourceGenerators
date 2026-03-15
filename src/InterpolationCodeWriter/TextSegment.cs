#nullable enable

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>
/// Represents an immutable segment of string parts that can be written to a <see cref="SourceTextWriter"/>.
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
/// For large segments (256 or more parts), the backing array is rented from <see cref="ArrayPool{T}.Shared"/>
/// and is automatically returned when the segment is consumed via <see cref="WriteToAndClear"/>.
/// </para>
/// </remarks>
[DebuggerDisplay("{DebuggerDisplay}")]
public partial struct TextSegment
{
    private const int ArrayPoolThreshold = 256;

    private readonly IFormatProvider _provider;
    private readonly Item[] _parts;
    private readonly bool _isRented;
    private readonly bool _appendLine;
    private int _length;

    private TextSegment(IFormatProvider? provider, Item[] parts, int length, bool isRented, bool appendLine)
    {
        _provider = provider ?? CultureInfo.InvariantCulture;
        _parts = parts;
        _length = length;
        _isRented = isRented;
        _appendLine = appendLine;
    }

    /// <summary>Gets a value indicating whether a line terminator should be appended after writing all parts.</summary>
    public bool AppendLine => _appendLine;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
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
    public static TextSegment Create(
        [InterpolatedStringHandlerArgument] ref TextSegment handler,
        bool appendLine = false
    )
    {
        return handler.WithAppendLine(appendLine);
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
    public static TextSegment Create(
        IFormatProvider? provider,
        [InterpolatedStringHandlerArgument("provider")] ref TextSegment handler,
        bool appendLine = false
    )
    {
        return handler.WithAppendLine(appendLine);
    }

    /// <summary>
    /// Enumerates all string values in this segment into a list and returns any rented backing array to
    /// <see cref="ArrayPool{T}.Shared"/>.
    /// </summary>
    /// <returns>A read-only list containing all string values, with <see langword="null"/> for empty parts.</returns>
    public readonly IReadOnlyList<string?> ToListAndClear()
    {
        var list = new List<string?>();
        FillListAndClear(list);
        return list;
    }

    /// <summary>Returns a <see cref="TextSegment"/> identical to this one but with <see cref="AppendLine"/> set to <paramref name="value"/>.</summary>
    /// <param name="value"><see langword="true"/> to append a line terminator after writing; <see langword="false"/> otherwise.</param>
    /// <returns>This instance if <see cref="AppendLine"/> already equals <paramref name="value"/>; otherwise a new instance with the updated flag.</returns>
    public readonly TextSegment WithAppendLine(bool value)
    {
        return _appendLine != value ? new TextSegment(_provider, _parts, _length, _isRented, value) : this;
    }

    /// <summary>
    /// Writes all parts of this segment to the specified <see cref="SourceTextWriter"/> and returns any
    /// rented backing array to <see cref="ArrayPool{T}.Shared"/>.
    /// </summary>
    /// <param name="writer">The writer to which the parts are written.</param>
    /// <remarks>
    /// If <see cref="AppendLine"/> is <see langword="true"/>, a line terminator is written after all parts.
    /// The rented backing array, if any, is returned in a <see langword="finally"/> block so it is always
    /// released even if writing throws.
    /// </remarks>
    public readonly void WriteToAndClear(SourceTextWriter writer)
    {
        try
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
        finally
        {
            Clear();
        }
    }

    private readonly void Clear()
    {
        if (_isRented)
        {
            ArrayPool<Item>.Shared.Return(_parts);
        }
    }

    private readonly void FillListAndClear(List<string?> list)
    {
        try
        {
            foreach (var item in _parts.AsSpan(0, _length))
            {
                item.FillList(list);
            }
        }
        finally
        {
            Clear();
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
                _textSequence.Value.WriteToAndClear(writer);
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
                _textSequence.Value.FillListAndClear(list);
            }
            else
            {
                list.Add(_text);
            }
        }

        public override string? ToString() => _textSequence.HasValue ? _textSequence.Value.DebuggerDisplay : _text;
    }
}
