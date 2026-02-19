namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Fact]
    public void WriteStringAppendsText()
    {
        var writer = new SourceTextWriter();
        writer.Write("hello");

        Assert.Equal("hello", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteNullStringDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write((string?)null);

        Assert.Equal("", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteEmptyStringDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write("");

        Assert.Equal("", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteMultipleStringsAppendsConcatenated()
    {
        var writer = new SourceTextWriter();
        writer.Write("hello");
        writer.Write(" ");
        writer.Write("world");

        Assert.Equal("hello world", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteTrimsLeadingNewLinesWhenBuilderIsEmpty()
    {
        var writer = new SourceTextWriter();
        writer.Write("\n\nline1");

        Assert.Equal("line1", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteTrimsLeadingCRLFWhenBuilderIsEmpty()
    {
        var writer = new SourceTextWriter();
        writer.Write("\r\n\r\nline1");

        Assert.Equal("line1", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteDoesNotTrimLeadingNewLinesWhenBuilderHasContent()
    {
        var writer = new SourceTextWriter();
        writer.Write("existing");
        writer.Write("\n\nline1");

        Assert.Equal("existing\n\nline1", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteMultiLineTextWithIndentation()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write("line1\nline2\nline3");

        Assert.Equal("header\n    line1\n    line2\n    line3", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteMultiLineTextPreservesConfiguredNewLine()
    {
        var writer = new SourceTextWriter(newLine: "\r\n");
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write("line1\nline2");

        Assert.Equal("header\r\n    line1\r\n    line2", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteBoolAppendsStringRepresentation()
    {
        var writer = new SourceTextWriter();
        writer.Write(true);

        Assert.Equal("True", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteIntAppendsNumber()
    {
        var writer = new SourceTextWriter();
        writer.Write(42);

        Assert.Equal("42", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteCharAppendsCharacter()
    {
        var writer = new SourceTextWriter();
        writer.Write('X');

        Assert.Equal("X", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteRepresentativeNumericTypes()
    {
        var writer = new SourceTextWriter();
        writer.Write(123L);
        writer.Write(",");
        writer.Write(3.14);
        writer.Write(",");
        writer.Write(9.99m);

        Assert.Equal("123,3.14,9.99", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteNullableIntWithValueWritesNumber()
    {
        var writer = new SourceTextWriter();
        writer.Write((int?)42);

        Assert.Equal("42", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteNullableIntWithNullDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write((int?)null);

        Assert.Equal("", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteNullableBoolWithNullDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write((bool?)null);

        Assert.Equal("", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteObjectCallsToString()
    {
        var writer = new SourceTextWriter();
        writer.Write((object)42);

        Assert.Equal("42", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteNullObjectDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write((object?)null);

        Assert.Equal("", writer.ToStringAndReset());
    }

    [Fact]
    public void WritePrimitiveWithIndentationAfterNewLine()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write(42);

        Assert.Equal("header\n    42", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedStringWithStringValue()
    {
        var writer = new SourceTextWriter();
        var name = "World";
        writer.Write($"Hello, {name}!");

        Assert.Equal("Hello, World!", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteTextEndingWithNewlineDoesNotProduceTrailingIndentation()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write("line1\nline2\n");

        Assert.Equal("header\n    line1\n    line2\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteTextEndingWithCRLFDoesNotProduceTrailingIndentation()
    {
        var writer = new SourceTextWriter(newLine: "\r\n");
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write("line1\r\nline2\r\n");

        Assert.Equal("header\r\n    line1\r\n    line2\r\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteMultiLineTextWithEmptyLinesDoesNotIndentEmptyLines()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write("line1\n\nline2");

        Assert.Equal("header\n    line1\n\n    line2", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteSingleLineEndingWithNewlineDoesNotProduceTrailingIndentation()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write("content\n");

        Assert.Equal("header\n    content\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteTextEndingWithNewlineNoIndentationIsUnchanged()
    {
        var writer = new SourceTextWriter();
        writer.Write("line1\nline2\n");

        Assert.Equal("line1\nline2\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteMultipleTrailingNewlinesWithIndentation()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write("content\n\n");

        Assert.Equal("header\n    content\n\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteStringWithLFNormalizesToConfiguredCRLFWhenIndentationIsZero()
    {
        var writer = new SourceTextWriter(newLine: "\r\n");
        writer.Write("line1\nline2");

        Assert.Equal("line1\r\nline2", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteSpanWithCRLFNormalizesToConfiguredLFWhenIndentationIsZero()
    {
        var writer = new SourceTextWriter(newLine: "\n");
        writer.Write("line1\r\nline2".AsSpan());

        Assert.Equal("line1\nline2", writer.ToStringAndReset());
    }
}
