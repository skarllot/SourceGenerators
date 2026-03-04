#nullable enable

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Raiqub.Generators.InterpolationCodeWriter.Collections;

/// <summary>
/// Represents an immutable sequence of string parts that can be written to a <see cref="SourceTextWriter"/>.
/// </summary>
/// <remarks>
/// <para>
/// Instances are created via interpolated string syntax using <see cref="CreateInterpolatedStringHandler"/>,
/// or from a span of strings via <see cref="Create(ReadOnlySpan{string?},bool)"/>.
/// </para>
/// <para>
/// For large sequences (256 or more parts), the backing array is rented from <see cref="ArrayPool{T}.Shared"/>
/// and is automatically returned when the sequence is consumed via <see cref="WriteToAndClear"/>.
/// </para>
/// </remarks>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly partial struct TextSequence
{
    private const int ArrayPoolThreshold = 256;

    private readonly Item[] _parts;
    private readonly int _length;
    private readonly bool _isRented;
    private readonly bool _appendLine;

    private TextSequence(ReadOnlySpan<string?> parts, bool appendLine)
    {
        _length = parts.Length;
        _appendLine = appendLine;

        switch (parts.Length)
        {
            case < ArrayPoolThreshold:
                _parts = new Item[parts.Length];
                FillItems(_parts, parts);
                _isRented = false;
                break;
            default:
                _parts = ArrayPool<Item>.Shared.Rent(parts.Length);
                FillItems(_parts, parts);
                _isRented = true;
                break;
        }
    }

    private TextSequence(Item[] parts, int length, bool isRented, bool appendLine)
    {
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

    /// <summary>Creates a <see cref="TextSequence"/> from a span of string parts.</summary>
    /// <param name="parts">The string parts that form the sequence.</param>
    /// <param name="appendLine">
    /// <see langword="true"/> to append a line terminator after writing all parts;
    /// <see langword="false"/> otherwise. Defaults to <see langword="false"/>.
    /// </param>
    /// <returns>A new <see cref="TextSequence"/> containing the specified parts.</returns>
    public static TextSequence Create(ReadOnlySpan<string?> parts, bool appendLine = false)
    {
        return new TextSequence(parts, appendLine);
    }

    /// <summary>Creates a <see cref="TextSequence"/> from an interpolated string.</summary>
    /// <param name="handler">The interpolated string handler that holds the accumulated parts.</param>
    /// <param name="appendLine">
    /// <see langword="true"/> to append a line terminator after writing all parts;
    /// <see langword="false"/> otherwise. Defaults to <see langword="false"/>.
    /// </param>
    /// <returns>A new <see cref="TextSequence"/> built from the interpolated string.</returns>
    public static TextSequence Create(
        [InterpolatedStringHandlerArgument] ref CreateInterpolatedStringHandler handler,
        bool appendLine = false
    )
    {
        return handler.Build(appendLine);
    }

    /// <summary>
    /// Creates a <see cref="TextSequence"/> from an interpolated string using the specified format provider.
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
    /// <returns>A new <see cref="TextSequence"/> built from the interpolated string.</returns>
    public static TextSequence Create(
        IFormatProvider? provider,
        [InterpolatedStringHandlerArgument("provider")]
        ref CreateInterpolatedStringHandler handler,
        bool appendLine = false
    )
    {
        return handler.Build(appendLine);
    }

    private static void FillItems(Span<Item> destination, ReadOnlySpan<string?> source)
    {
        for (var i = 0; i < destination.Length; i++)
        {
            destination[i] = source[i];
        }
    }

    private void Clear()
    {
        if (_isRented)
        {
            ArrayPool<Item>.Shared.Return(_parts);
        }
    }

    private void FillListAndClear(List<string?> list)
    {
        try
        {
            foreach (var part in _parts)
            {
                part.FillList(list);
            }
        }
        finally
        {
            Clear();
        }
    }

    /// <summary>
    /// Enumerates all string values in this sequence into a list and returns any rented backing array to
    /// <see cref="ArrayPool{T}.Shared"/>.
    /// </summary>
    /// <returns>A read-only list containing all string values, with <see langword="null"/> for empty parts.</returns>
    public IReadOnlyList<string?> ToListAndClear()
    {
        var list = new List<string?>();
        FillListAndClear(list);
        return list;
    }

    /// <summary>
    /// Writes all parts of this sequence to the specified <see cref="SourceTextWriter"/> and returns any
    /// rented backing array to <see cref="ArrayPool{T}.Shared"/>.
    /// </summary>
    /// <param name="writer">The writer to which the parts are written.</param>
    /// <remarks>
    /// If <see cref="AppendLine"/> is <see langword="true"/>, a line terminator is written after all parts.
    /// The rented backing array, if any, is returned in a <see langword="finally"/> block so it is always
    /// released even if writing throws.
    /// </remarks>
    public void WriteToAndClear(SourceTextWriter writer)
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

    private readonly struct Item
    {
        private readonly string? _text;
        private readonly TextSequence? _textSequence;

        private Item(string? text) => _text = text;

        private Item(TextSequence? textSequence) => _textSequence = textSequence;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Item(string? value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Item(in TextSequence? value) => new(value);

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
