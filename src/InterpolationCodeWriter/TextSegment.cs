#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Raiqub.Generators.InterpolationCodeWriter.Internals;

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
    private const int MinimumCapacity = 8;

    private readonly IFormatProvider _provider;
    private string? _part0;
    private string? _part1;
    private string? _part2;
    private string? _part3;
    private string? _part4;
    private string? _part5;
    private string? _part6;
    private string? _part7;
    private string?[]? _additionalParts;
    private bool _appendLine;
    private int _length;

    private TextSegment(string? value)
    {
        _provider = CultureInfo.InvariantCulture;
        _part0 = value;
        _part1 = null;
        _part2 = null;
        _part3 = null;
        _part4 = null;
        _part5 = null;
        _part6 = null;
        _part7 = null;
        _additionalParts = null;
        _appendLine = false;
        _length = 1;
    }

    /// <summary>
    /// Defines an implicit conversion from a <see cref="string"/> to a <see cref="TextSegment"/> object.
    /// </summary>
    /// <param name="value">The string value to be converted to a <see cref="TextSegment"/>.</param>
    /// <returns>A new instance of <see cref="TextSegment"/> that encapsulates the given string value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TextSegment(string? value) => new(value);

    /// <summary>Gets a value indicating whether a line terminator should be appended after writing all parts.</summary>
    public readonly bool AppendLine => _appendLine;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string DebuggerDisplay
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var item in this)
            {
                sb.Append(item);
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
    /// <returns>
    /// A read-only list containing all string parts.
    /// Entries are <see langword="null"/> when the corresponding interpolated value was <see langword="null"/> at the time the segment was created.
    /// </returns>
    public readonly IReadOnlyList<string?> ToList()
    {
        var list = new string?[_length];
        var inlineCount = Math.Min(_length, MinimumCapacity);
        for (var i = 0; i < inlineCount; i++)
        {
            list[i] = GetItemAt(i);
        }

        if (_additionalParts?.Length > 0)
        {
            Array.Copy(_additionalParts, 0, list, MinimumCapacity, _length - MinimumCapacity);
        }

        return list;
    }

    /// <summary>
    /// Writes all parts of this segment to the specified <see cref="SourceTextWriter"/>.
    /// </summary>
    /// <param name="writer">The writer to which the parts are written.</param>
    public readonly void WriteTo(SourceTextWriter writer)
    {
        WriteTo(writer, _appendLine);
    }

    /// <summary>
    /// Writes all parts of this segment to the specified <see cref="SourceTextWriter"/>,
    /// overriding whether a line terminator is appended.
    /// </summary>
    /// <param name="writer">The writer to which the parts are written.</param>
    /// <param name="appendLine">
    /// <see langword="true"/> to append a line terminator after writing all parts;
    /// <see langword="false"/> otherwise. Overrides <see cref="AppendLine"/>.
    /// </param>
    public readonly void WriteTo(SourceTextWriter writer, bool appendLine)
    {
        foreach (var item in this)
        {
            writer.Write(item);
        }

        if (appendLine)
        {
            writer.WriteLine();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureCapacityForAdditionalItems(int additionalItems)
    {
        if (Capacity - _length < additionalItems)
        {
            Grow(additionalItems);
        }
    }

    private void Grow(int additionalItems)
    {
        var requiredMinCapacity = (uint)_length + (uint)additionalItems - MinimumCapacity;
        var newCapacity = Math.Max(
            requiredMinCapacity,
            Math.Min((uint)(_additionalParts?.Length ?? 0) * 2, int.MaxValue)
        );
        var arraySize = (int)MathEx.Clamp(newCapacity, 4, int.MaxValue);

        Array.Resize(ref _additionalParts, arraySize);
    }
}
