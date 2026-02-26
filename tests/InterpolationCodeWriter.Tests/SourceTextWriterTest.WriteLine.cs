namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Fact]
    public void WriteLineAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine();

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineStringAppendsTextAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine("hello");

        Assert.Equal("hello\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullStringAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((string?)null);

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
    public void WriteLineIntAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((int?)42);

        Assert.Equal("42\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullIntAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((int?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineBoolAppendsValueAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((bool?)true);

        Assert.Equal("True\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineObjectAppendsToStringAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((object)"test");

        Assert.Equal("test\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullObjectAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((object?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
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
    public void WriteLineBoolNonNullableAppendsValueAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine(true);

        Assert.Equal("True\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineCharNonNullableAppendsCharacterAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine('X');

        Assert.Equal("X\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineByteAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((byte)255);

        Assert.Equal("255\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineSByteAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((sbyte)-1);

        Assert.Equal("-1\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineShortAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((short)1000);

        Assert.Equal("1000\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineUShortAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((ushort)2000);

        Assert.Equal("2000\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineIntNonNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine(42);

        Assert.Equal("42\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineUIntAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((uint)100);

        Assert.Equal("100\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineLongAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine(123L);

        Assert.Equal("123\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineULongAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((ulong)456);

        Assert.Equal("456\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineFloatAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine(1.5f);

        Assert.Equal("1.5\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineDoubleAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine(3.14);

        Assert.Equal("3.14\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineDecimalAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine(9.99m);

        Assert.Equal("9.99\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullBoolAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((bool?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineCharNullableAppendsCharacterAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((char?)'X');

        Assert.Equal("X\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullCharAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((char?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineByteNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((byte?)255);

        Assert.Equal("255\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullByteAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((byte?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineSByteNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((sbyte?)-1);

        Assert.Equal("-1\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullSByteAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((sbyte?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineShortNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((short?)1000);

        Assert.Equal("1000\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullShortAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((short?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineUShortNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((ushort?)2000);

        Assert.Equal("2000\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullUShortAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((ushort?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineUIntNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((uint?)100);

        Assert.Equal("100\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullUIntAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((uint?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineLongNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((long?)123);

        Assert.Equal("123\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullLongAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((long?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineULongNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((ulong?)456);

        Assert.Equal("456\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullULongAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((ulong?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineFloatNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((float?)1.5f);

        Assert.Equal("1.5\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullFloatAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((float?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineDoubleNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((double?)3.14);

        Assert.Equal("3.14\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullDoubleAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((double?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineDecimalNullableAppendsNumberAndNewLine()
    {
        var writer = new SourceTextWriter();
        writer.WriteLine((decimal?)9.99m);

        Assert.Equal("9.99\n", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteLineNullDecimalAppendsOnlyNewLine()
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writer.WriteLine((decimal?)null);

        Assert.Equal("start\n", writer.ToStringAndReset());
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
