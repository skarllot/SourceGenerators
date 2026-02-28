namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Theory]
    [MemberData(nameof(WritePrimitiveCases))]
    [MemberData(nameof(WriteNullableValueCases))]
    public void WriteValueAppendsStringRepresentation(Action<SourceTextWriter> write, string expected)
    {
        var writer = new SourceTextWriter();
        write(writer);
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    public static TheoryData<Action<SourceTextWriter>, string> WritePrimitiveCases =>
        new()
        {
            { w => w.Write(true), "True" },
            { w => w.Write(false), "False" },
            { w => w.Write('X'), "X" },
            { w => w.Write((byte)42), "42" },
            { w => w.Write((sbyte)42), "42" },
            { w => w.Write((short)42), "42" },
            { w => w.Write((ushort)42), "42" },
            { w => w.Write(42), "42" },
            { w => w.Write(42u), "42" },
            { w => w.Write(42L), "42" },
            { w => w.Write(42ul), "42" },
            { w => w.Write(1.5f), "1.5" },
            { w => w.Write(3.14), "3.14" },
            { w => w.Write(9.99m), "9.99" },
            { w => w.Write("hello"), "hello" },
            { w => w.Write((object)42), "42" },
        };

    public static TheoryData<Action<SourceTextWriter>, string> WriteNullableValueCases =>
        new()
        {
            { w => w.Write((bool?)true), "True" },
            { w => w.Write((char?)'A'), "A" },
            { w => w.Write((byte?)42), "42" },
            { w => w.Write((sbyte?)42), "42" },
            { w => w.Write((short?)42), "42" },
            { w => w.Write((ushort?)42), "42" },
            { w => w.Write((int?)42), "42" },
            { w => w.Write((uint?)42u), "42" },
            { w => w.Write((long?)42L), "42" },
            { w => w.Write((ulong?)42ul), "42" },
            { w => w.Write((float?)1.5f), "1.5" },
            { w => w.Write((double?)3.14), "3.14" },
            { w => w.Write((decimal?)9.99m), "9.99" },
        };

    [Theory]
    [MemberData(nameof(WriteNullOrEmptyCases))]
    public void WriteNullOrEmptyDoesNothing(Action<SourceTextWriter> write)
    {
        var writer = new SourceTextWriter();
        write(writer);
        Assert.Equal("", writer.ToStringAndReset());
    }

    public static TheoryData<Action<SourceTextWriter>> WriteNullOrEmptyCases =>
        new()
        {
            w => w.Write((string?)null),
            w => w.Write(""),
            w => w.Write((bool?)null),
            w => w.Write((char?)null),
            w => w.Write((byte?)null),
            w => w.Write((sbyte?)null),
            w => w.Write((short?)null),
            w => w.Write((ushort?)null),
            w => w.Write((int?)null),
            w => w.Write((uint?)null),
            w => w.Write((long?)null),
            w => w.Write((ulong?)null),
            w => w.Write((float?)null),
            w => w.Write((double?)null),
            w => w.Write((decimal?)null),
            w => w.Write((object?)null),
            w => w.Write((object?)null, "X"),
            w => w.Write((byte?)null, "X"),
            w => w.Write((sbyte?)null, "X"),
            w => w.Write((short?)null, "X"),
            w => w.Write((ushort?)null, "X"),
            w => w.Write((int?)null, "X"),
            w => w.Write((uint?)null, "X"),
            w => w.Write((long?)null, "X"),
            w => w.Write((ulong?)null, "X"),
            w => w.Write((float?)null, "F2"),
            w => w.Write((double?)null, "F2"),
            w => w.Write((decimal?)null, "F2"),
        };

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
    public void WriteTrimsLeadingCrLfWhenBuilderIsEmpty()
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
    public void WriteTextEndingWithCrLfDoesNotProduceTrailingIndentation()
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
    public void WriteStringWithLfNormalizesToConfiguredCrLfWhenIndentationIsZero()
    {
        var writer = new SourceTextWriter(newLine: "\r\n");
        writer.Write("line1\nline2");

        Assert.Equal("line1\r\nline2", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteSpanWithCrLfNormalizesToConfiguredLfWhenIndentationIsZero()
    {
        var writer = new SourceTextWriter(newLine: "\n");
        writer.Write("line1\r\nline2".AsSpan());

        Assert.Equal("line1\nline2", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteEmptyResultDoesNothing()
    {
        var writer = new SourceTextWriter();
        writer.Write(EmptyResult.Value);

        Assert.Equal("", writer.ToStringAndReset());
    }
}
