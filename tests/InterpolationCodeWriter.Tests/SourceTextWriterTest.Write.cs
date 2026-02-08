namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Fact]
    public void WriteStringAppendsText()
    {
        var writer = new SourceTextWriter();
        writer.Write("hello");

        Assert.Equal("hello", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteNullStringDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write((string?)null);

        Assert.Equal("", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteEmptyStringDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write("");

        Assert.Equal("", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteMultipleStringsAppendsConcatenated()
    {
        var writer = new SourceTextWriter();
        writer.Write("hello");
        writer.Write(" ");
        writer.Write("world");

        Assert.Equal("hello world", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteTrimsLeadingNewLinesWhenBuilderIsEmpty()
    {
        var writer = new SourceTextWriter();
        writer.Write("\n\nline1");

        Assert.Equal("line1", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteTrimsLeadingCRLFWhenBuilderIsEmpty()
    {
        var writer = new SourceTextWriter();
        writer.Write("\r\n\r\nline1");

        Assert.Equal("line1", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteDoesNotTrimLeadingNewLinesWhenBuilderHasContent()
    {
        var writer = new SourceTextWriter();
        writer.Write("existing");
        writer.Write("\n\nline1");

        Assert.Equal("existing\n\nline1", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteMultiLineTextWithIndentation()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write("line1\nline2\nline3");

        Assert.Equal("header\n    line1\n    line2\n    line3", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteMultiLineTextPreservesConfiguredNewLine()
    {
        var writer = new SourceTextWriter(newLine: "\r\n");
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write("line1\nline2");

        Assert.Equal("header\r\n    line1\r\n    line2", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteBoolAppendsStringRepresentation()
    {
        var writer = new SourceTextWriter();
        writer.Write(true);

        Assert.Equal("True", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteIntAppendsNumber()
    {
        var writer = new SourceTextWriter();
        writer.Write(42);

        Assert.Equal("42", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteCharAppendsCharacter()
    {
        var writer = new SourceTextWriter();
        writer.Write('X');

        Assert.Equal("X", writer.ToStringAndClear());
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

        Assert.Equal("123,3.14,9.99", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteNullableIntWithValueWritesNumber()
    {
        var writer = new SourceTextWriter();
        writer.Write((int?)42);

        Assert.Equal("42", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteNullableIntWithNullDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write((int?)null);

        Assert.Equal("", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteNullableBoolWithNullDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write((bool?)null);

        Assert.Equal("", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteObjectCallsToString()
    {
        var writer = new SourceTextWriter();
        writer.Write((object)42);

        Assert.Equal("42", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteNullObjectDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write((object?)null);

        Assert.Equal("", writer.ToStringAndClear());
    }

    [Fact]
    public void WritePrimitiveWithIndentationAfterNewLine()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("header");
        writer.Write(42);

        Assert.Equal("header\n    42", writer.ToStringAndClear());
    }

    [Fact]
    public void WriteInterpolatedStringWithStringValue()
    {
        var writer = new SourceTextWriter();
        var name = "World";
        writer.Write($"Hello, {name}!");

        Assert.Equal("Hello, World!", writer.ToStringAndClear());
    }
}
