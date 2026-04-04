using System.Diagnostics.CodeAnalysis;

namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public class TextSegmentCollectionTest
{
    // $"a{1}b{2}c{3}d{4}e" → 9 parts:
    // index 0-7 in inline fields (_part0…_part7), index 8 in _additionalParts[0]
    private static TextSegment NinePartSegment =>
        $"a{1}b{2}c{3}d{4}e";

    #region Indexer

    [Theory]
    [InlineData(0, "a")]   // first inline literal
    [InlineData(1, "1")]   // first formatted value
    [InlineData(6, "d")]   // seventh inline part (literal)
    [InlineData(7, "4")]   // last inline part (formatted value)
    public void IndexerReturnsInlinePart(int index, string expected)
    {
        Assert.Equal(expected, NinePartSegment[index]);
    }

    [Fact]
    public void IndexerReturnsAdditionalPart()
    {
        // index 8 lives in _additionalParts[0], past the 8 inline fields
        Assert.Equal("e", NinePartSegment[8]);
    }

    [Fact]
    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public void IndexerReturnsNullForNullFormattedValue()
    {
        TextSegment seg = $"x{(string?)null}y";
        Assert.Null(seg[1]);
    }

    [Fact]
    public void IndexerThrowsForNegativeIndex()
    {
        TextSegment seg = $"hello";
        Assert.Throws<ArgumentOutOfRangeException>(() => seg[-1]);
    }

    [Fact]
    public void IndexerThrowsForIndexEqualToLength()
    {
        TextSegment seg = $"hello";
        // length == 1 (single literal); index 1 is out of range
        Assert.Throws<ArgumentOutOfRangeException>(() => seg[1]);
    }

    [Fact]
    public void IndexerThrowsForIndexGreaterThanLength()
    {
        TextSegment seg = $"hello";
        Assert.Throws<ArgumentOutOfRangeException>(() => seg[99]);
    }

    #endregion

    #region Enumerator

    [Fact]
    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public void EnumeratorIteratesAllEightInlineParts()
    {
        // 8 parts exactly fill the inline fields; no _additionalParts needed
        TextSegment seg = $"a{1}b{2}c{3}d{4}";
        var result = new List<string?>();
        foreach (var part in seg)
        {
            result.Add(part);
        }

        Assert.Equal(["a", "1", "b", "2", "c", "3", "d", "4"], result);
    }

    [Fact]
    public void EnumeratorIteratesPartsAcrossInlineAndAdditional()
    {
        // 9 parts: 8 inline + 1 in _additionalParts
        var result = new List<string?>();
        foreach (var part in NinePartSegment)
        {
            result.Add(part);
        }

        Assert.Equal(["a", "1", "b", "2", "c", "3", "d", "4", "e"], result);
    }

    [Fact]
    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public void EnumeratorYieldsNullForNullFormattedValues()
    {
        TextSegment seg = $"x{(int?)null}y";
        var result = new List<string?>();
        foreach (var part in seg)
        {
            result.Add(part);
        }

        Assert.Equal(["x", null, "y"], result);
    }

    [Fact]
    public void EnumeratorMoveNextReturnsFalseAfterLastPart()
    {
        TextSegment seg = $"hello";
        var enumerator = seg.GetEnumerator();
        Assert.True(enumerator.MoveNext());   // advances to part 0
        Assert.Equal("hello", enumerator.Current);
        Assert.False(enumerator.MoveNext());  // no more parts
    }

    [Fact]
    public void EnumeratorEachCallIsIndependent()
    {
        // Two independent enumerations of the same segment should yield equal results
        var first = new List<string?>();
        var second = new List<string?>();
        foreach (var part in NinePartSegment) first.Add(part);
        foreach (var part in NinePartSegment) second.Add(part);

        Assert.Equal(first, second);
    }

    #endregion
}
