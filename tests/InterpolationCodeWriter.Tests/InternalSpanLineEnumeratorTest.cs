namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public class InternalSpanLineEnumeratorTest
{
    [Fact]
    public void EnumeratesSingleLine()
    {
        var lines = CollectLines("hello");

        Assert.Equal(["hello"], lines);
    }

    [Fact]
    public void EnumeratesLinesWithLineFeed()
    {
        var lines = CollectLines("a\nb\nc");

        Assert.Equal(["a", "b", "c"], lines);
    }

    [Fact]
    public void EnumeratesLinesWithCarriageReturnLineFeed()
    {
        var lines = CollectLines("a\r\nb\r\nc");

        Assert.Equal(["a", "b", "c"], lines);
    }

    [Fact]
    public void EnumeratesLinesWithCarriageReturnOnly()
    {
        var lines = CollectLines("a\rb\rc");

        Assert.Equal(["a", "b", "c"], lines);
    }

    [Fact]
    public void EnumeratesLinesWithMixedLineEndings()
    {
        var lines = CollectLines("a\nb\r\nc\rd");

        Assert.Equal(["a", "b", "c", "d"], lines);
    }

    [Fact]
    public void EmptyStringYieldsSingleEmptyLine()
    {
        var lines = CollectLines("");

        Assert.Equal([""], lines);
    }

    [Fact]
    public void TrailingLineFeedYieldsEmptyLastLine()
    {
        var lines = CollectLines("a\n");

        Assert.Equal(["a", ""], lines);
    }

    [Fact]
    public void TrailingCrLfYieldsEmptyLastLine()
    {
        var lines = CollectLines("a\r\n");

        Assert.Equal(["a", ""], lines);
    }

    [Fact]
    public void ConsecutiveLineFeedsYieldEmptyLines()
    {
        var lines = CollectLines("a\n\n\nb");

        Assert.Equal(["a", "", "", "b"], lines);
    }

    [Fact]
    public void LeadingLineFeedYieldsEmptyFirstLine()
    {
        var lines = CollectLines("\na");

        Assert.Equal(["", "a"], lines);
    }

    [Fact]
    public void MoveNextReturnsFalseAfterExhaustion()
    {
        var enumerator = new InternalSpanLineEnumerator("ab".AsSpan());

        Assert.True(enumerator.MoveNext());
        Assert.False(enumerator.MoveNext());
        Assert.False(enumerator.MoveNext());
    }

    private static string[] CollectLines(string input)
    {
        var result = new List<string>();
        foreach (var line in new InternalSpanLineEnumerator(input.AsSpan()))
        {
            result.Add(line.ToString());
        }

        return result.ToArray();
    }
}
