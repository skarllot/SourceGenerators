using System.Text;

namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public class StringBuilderMemoryTest
{
    [Fact]
    public void AppendAddsTextToStringBuilder()
    {
        var sb = new StringBuilder();
        StringBuilderMemory.Append(sb, "hello".AsSpan());

        Assert.Equal("hello", sb.ToString());
    }

    [Fact]
    public void AppendEmptySpanDoesNothing()
    {
        var sb = new StringBuilder("existing");
        StringBuilderMemory.Append(sb, ReadOnlySpan<char>.Empty);

        Assert.Equal("existing", sb.ToString());
    }

    [Fact]
    public void AppendAppendsToExistingContent()
    {
        var sb = new StringBuilder("hello");
        StringBuilderMemory.Append(sb, " world".AsSpan());

        Assert.Equal("hello world", sb.ToString());
    }

    [Fact]
    public void AppendSingleCharacter()
    {
        var sb = new StringBuilder();
        StringBuilderMemory.Append(sb, "X".AsSpan());

        Assert.Equal("X", sb.ToString());
    }

    [Fact]
    public void AppendMultipleTimesAccumulatesText()
    {
        var sb = new StringBuilder();
        StringBuilderMemory.Append(sb, "abc".AsSpan());
        StringBuilderMemory.Append(sb, "def".AsSpan());
        StringBuilderMemory.Append(sb, "ghi".AsSpan());

        Assert.Equal("abcdefghi", sb.ToString());
    }

    [Fact]
    public void AppendPreservesSpecialCharacters()
    {
        var sb = new StringBuilder();
        StringBuilderMemory.Append(sb, "line1\nline2\r\n".AsSpan());

        Assert.Equal("line1\nline2\r\n", sb.ToString());
    }
}
