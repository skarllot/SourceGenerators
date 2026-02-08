namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Fact]
    public void WriteLineAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine();

        Assert.Equal("start\n", writer.ToString());
    }

    [Fact]
    public void WriteLineStringAppendsTextAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine("hello");

        Assert.Equal("hello\n", writer.ToString());
    }

    [Fact]
    public void WriteLineNullStringAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((string?)null);

        Assert.Equal("start\n", writer.ToString());
    }

    [Fact]
    public void WriteLineWithCRLFNewLine()
    {
        var writer = new SourceTextWriter(newLine: "\r\n");
        writer.WriteLine("hello");

        Assert.Equal("hello\r\n", writer.ToString());
    }

    [Fact]
    public void WriteLineIntAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((int?)42);

        Assert.Equal("42\n", writer.ToString());
    }

    [Fact]
    public void WriteLineNullIntAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((int?)null);

        Assert.Equal("start\n", writer.ToString());
    }

    [Fact]
    public void WriteLineBoolAppendsValueAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((bool?)true);

        Assert.Equal("True\n", writer.ToString());
    }

    [Fact]
    public void WriteLineObjectAppendsToStringAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((object)"test");

        Assert.Equal("test\n", writer.ToString());
    }

    [Fact]
    public void WriteLineNullObjectAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((object?)null);

        Assert.Equal("start\n", writer.ToString());
    }

    [Fact]
    public void WriteLineInterpolatedStringAppendsTextAndNewLine()
    {
        var writer = new SourceTextWriter();
        var x = 5;
        writer.WriteLine($"val={x}");

        Assert.Equal("val=5\n", writer.ToString());
    }

    [Fact]
    public void ConsecutiveWriteLineProducesMultipleLines()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine("a");
        writer.WriteLine("b");

        Assert.Equal("a\nb\n", writer.ToString());
    }

    [Fact]
    public void WriteLineFollowedByWriteAppliesIndentation()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("first");
        writer.Write("second");

        Assert.Equal("first\n    second", writer.ToString());
    }

    [Fact]
    public void MultipleWriteLineThenWriteVerifiesIndentation()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.Write("line1");
        writer.WriteLine();
        writer.Write("line2");
        writer.WriteLine();
        writer.Write("line3");

        Assert.Equal("line1\n    line2\n    line3", writer.ToString());
    }
}
