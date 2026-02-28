namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Theory]
    [MemberData(nameof(WriteLinePrimitiveCases))]
    [MemberData(nameof(WriteLineNullableValueCases))]
    public void WriteLineValueAppendsStringRepresentation(Action<SourceTextWriter> writeLine, string expected)
    {
        var writer = new SourceTextWriter();
        writeLine(writer);
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    public static TheoryData<Action<SourceTextWriter>, string> WriteLinePrimitiveCases =>
        new()
        {
            { w => w.WriteLine(true), "True\n" },
            { w => w.WriteLine(false), "False\n" },
            { w => w.WriteLine('X'), "X\n" },
            { w => w.WriteLine((byte)255), "255\n" },
            { w => w.WriteLine((sbyte)-1), "-1\n" },
            { w => w.WriteLine((short)1000), "1000\n" },
            { w => w.WriteLine((ushort)2000), "2000\n" },
            { w => w.WriteLine(42), "42\n" },
            { w => w.WriteLine(100u), "100\n" },
            { w => w.WriteLine(123L), "123\n" },
            { w => w.WriteLine(456ul), "456\n" },
            { w => w.WriteLine(1.5f), "1.5\n" },
            { w => w.WriteLine(3.14), "3.14\n" },
            { w => w.WriteLine(9.99m), "9.99\n" },
            { w => w.WriteLine("hello"), "hello\n" },
            { w => w.WriteLine((object)"test"), "test\n" },
        };

    public static TheoryData<Action<SourceTextWriter>, string> WriteLineNullableValueCases =>
        new()
        {
            { w => w.WriteLine((bool?)true), "True\n" },
            { w => w.WriteLine((char?)'X'), "X\n" },
            { w => w.WriteLine((byte?)255), "255\n" },
            { w => w.WriteLine((sbyte?)-1), "-1\n" },
            { w => w.WriteLine((short?)1000), "1000\n" },
            { w => w.WriteLine((ushort?)2000), "2000\n" },
            { w => w.WriteLine((int?)42), "42\n" },
            { w => w.WriteLine((uint?)100u), "100\n" },
            { w => w.WriteLine((long?)123L), "123\n" },
            { w => w.WriteLine((ulong?)456ul), "456\n" },
            { w => w.WriteLine((float?)1.5f), "1.5\n" },
            { w => w.WriteLine((double?)3.14), "3.14\n" },
            { w => w.WriteLine((decimal?)9.99m), "9.99\n" },
        };

    [Theory]
    [MemberData(nameof(WriteLineNullCases))]
    public void WriteLineNullAppendsOnlyNewLine(Action<SourceTextWriter> writeLine)
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writeLine(writer);
        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    public static TheoryData<Action<SourceTextWriter>> WriteLineNullCases =>
        new()
        {
            w => w.WriteLine((string?)null),
            w => w.WriteLine((bool?)null),
            w => w.WriteLine((char?)null),
            w => w.WriteLine((byte?)null),
            w => w.WriteLine((sbyte?)null),
            w => w.WriteLine((short?)null),
            w => w.WriteLine((ushort?)null),
            w => w.WriteLine((int?)null),
            w => w.WriteLine((uint?)null),
            w => w.WriteLine((long?)null),
            w => w.WriteLine((ulong?)null),
            w => w.WriteLine((float?)null),
            w => w.WriteLine((double?)null),
            w => w.WriteLine((decimal?)null),
            w => w.WriteLine((object?)null),
        };

    [Fact]
    public void WriteLineAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine();

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineWithCRLFNewLine()
    {
        var writer = new SourceTextWriter(newLine: "\r\n");
        writer.WriteLine("hello");

        Assert.Equal("hello\r\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineInterpolatedStringAppendsTextAndNewLine()
    {
        var writer = new SourceTextWriter();
        var x = 5;
        writer.WriteLine($"val={x}");

        Assert.Equal("val=5\n", writer.ToStringAndReset());
    }

    [Fact]
    public void ConsecutiveWriteLineProducesMultipleLines()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine("a");
        writer.WriteLine("b");

        Assert.Equal("a\nb\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineFollowedByWriteAppliesIndentation()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("first");
        writer.Write("second");

        Assert.Equal("first\n    second", writer.ToStringAndReset());
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

        Assert.Equal("line1\n    line2\n    line3", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineEmptyResultAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine(EmptyResult.Value);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }
}
