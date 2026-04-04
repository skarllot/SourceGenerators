#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Raiqub.Generators.InterpolationCodeWriter.Internals;

namespace Raiqub.Generators.InterpolationCodeWriter;

partial struct TextSegment
{
    /// <summary>Gets the string part at the specified index.</summary>
    /// <param name="index">The zero-based index of the part to get.</param>
    /// <returns>The string part at <paramref name="index"/>, or <see langword="null"/> if that part was a null value.</returns>
    /// <exception cref="IndexOutOfRangeException"><paramref name="index"/> is less than zero or greater than or equal to the number of parts.</exception>
    public string? this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get
        {
            Throws.IndexOutOfRangeIfInvalid(index, _length, nameof(index));
            return GetItemAt(index);
        }
        private set
        {
            Throws.IndexOutOfRangeIfInvalid(index, _length, nameof(index));
            switch (index)
            {
                case 0:
                    _part0 = value;
                    break;
                case 1:
                    _part1 = value;
                    break;
                case 2:
                    _part2 = value;
                    break;
                case 3:
                    _part3 = value;
                    break;
                case 4:
                    _part4 = value;
                    break;
                case 5:
                    _part5 = value;
                    break;
                case 6:
                    _part6 = value;
                    break;
                case 7:
                    _part7 = value;
                    break;
                default:
                    _additionalParts![index - MinimumCapacity] = value;
                    break;
            }
        }
    }

    /// <summary>Gets the number of string parts in this segment.</summary>
    public readonly int Count => _length;

    private readonly int Capacity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _additionalParts?.Length + MinimumCapacity ?? MinimumCapacity;
    }

    /// <summary>Returns an enumerator that iterates through the string parts of this segment.</summary>
    /// <returns>An <see cref="Enumerator"/> for this <see cref="TextSegment"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Enumerator GetEnumerator() => new(this);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly string? GetItemAt(int index) =>
        index switch
        {
            0 => _part0,
            1 => _part1,
            2 => _part2,
            3 => _part3,
            4 => _part4,
            5 => _part5,
            6 => _part6,
            7 => _part7,
            _ => _additionalParts![index - MinimumCapacity],
        };

    /// <summary>Enumerates the string parts of a <see cref="TextSegment"/>.</summary>
    [StructLayout(LayoutKind.Auto)]
    public ref struct Enumerator
    {
        private readonly TextSegment _segment;
        private int _position;

        /// <summary>Initializes a new instance of <see cref="Enumerator"/> for the specified segment.</summary>
        /// <param name="segment">The <see cref="TextSegment"/> to enumerate.</param>
        public Enumerator(TextSegment segment)
        {
            _segment = segment;
            _position = -1;
        }

        /// <summary>Advances the enumerator to the next part.</summary>
        /// <returns><see langword="true"/> if the enumerator was successfully advanced; <see langword="false"/> if the enumerator has passed the end of the segment.</returns>
        public bool MoveNext()
        {
            if (_position >= _segment._length - 1)
            {
                return false;
            }

            _position++;
            return true;
        }

        /// <summary>Gets the string part at the current position of the enumerator.</summary>
        /// <value>The string part at the current position, or <see langword="null"/> if that part was a null value.</value>
        public readonly string? Current => _segment.GetItemAt(_position);
    }
}
