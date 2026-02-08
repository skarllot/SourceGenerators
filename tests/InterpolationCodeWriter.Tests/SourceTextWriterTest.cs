using System.Text;

namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Fact]
    public void DefaultConstructorCreatesEmptyWriter()
    {
        var writer = new SourceTextWriter();

        Assert.Equal("", writer.ToString());
    }

    [Fact]
    public void ConstructorWithCustomNewLine()
    {
        var writer = new SourceTextWriter(newLine: "\r\n");
        writer.WriteLine("hello");

        Assert.Equal("hello\r\n", writer.ToString());
    }

    [Fact]
    public void ConstructorWithCustomStringBuilder()
    {
        var sb = new StringBuilder("existing");
        var writer = new SourceTextWriter(sb);
        writer.Write(" content");

        Assert.Equal("existing content", writer.ToString());
    }

    [Fact]
    public void ToStringReturnsContentAndClearsState()
    {
        var writer = new SourceTextWriter();
        writer.Write("hello");

        var first = writer.ToString();
        var second = writer.ToString();

        Assert.Equal("hello", first);
        Assert.Equal("", second);
    }

    [Fact]
    public void ClearAllTextRemovesContentAndResetsIndentation()
    {
        var writer = new SourceTextWriter();
        writer.Write("some content");
        writer.PushIndent();
        writer.ClearAllText();
        writer.WriteLine("line1");
        writer.Write("line2");

        Assert.Equal("line1\nline2", writer.ToString());
    }

    [Fact]
    public void RewindRemovesLastNCharacters()
    {
        var writer = new SourceTextWriter();
        writer.Write("Hello, World!");
        writer.Rewind(8);

        Assert.Equal("Hello", writer.ToString());
    }

    [Fact]
    public void RewindToZeroResultsInEmptyContent()
    {
        var writer = new SourceTextWriter();
        writer.Write("abc");
        writer.Rewind(3);

        Assert.Equal("", writer.ToString());
    }
}
