namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Theory]
    [MemberData(nameof(WriteLineWithFormatCases))]
    [MemberData(nameof(WriteLineNullableWithFormatCases))]
    public void WriteLineWithFormatAppliesFormat(Action<SourceTextWriter> writeLine, string expected)
    {
        var writer = new SourceTextWriter();
        writeLine(writer);
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    public static TheoryData<Action<SourceTextWriter>, string> WriteLineWithFormatCases =>
        new()
        {
            { w => w.WriteLine((byte)255, "X"), "FF\n" },
            { w => w.WriteLine((sbyte)127, "X"), "7F\n" },
            { w => w.WriteLine((short)255, "X"), "FF\n" },
            { w => w.WriteLine((ushort)255, "X"), "FF\n" },
            { w => w.WriteLine(255, "X"), "FF\n" },
            { w => w.WriteLine(255u, "X"), "FF\n" },
            { w => w.WriteLine(255L, "X"), "FF\n" },
            { w => w.WriteLine(255ul, "X"), "FF\n" },
            { w => w.WriteLine(3.14159f, "F2"), "3.14\n" },
            { w => w.WriteLine(3.14159, "F2"), "3.14\n" },
            { w => w.WriteLine(3.14159m, "F2"), "3.14\n" },
            { w => w.WriteLine((object)255, "X"), "FF\n" },
            { w => w.WriteLine<DayOfWeek>(DayOfWeek.Friday, "D"), "5\n" },
        };

    public static TheoryData<Action<SourceTextWriter>, string> WriteLineNullableWithFormatCases =>
        new()
        {
            { w => w.WriteLine((byte?)255, "X"), "FF\n" },
            { w => w.WriteLine((sbyte?)127, "X"), "7F\n" },
            { w => w.WriteLine((short?)255, "X"), "FF\n" },
            { w => w.WriteLine((ushort?)255, "X"), "FF\n" },
            { w => w.WriteLine((int?)255, "X"), "FF\n" },
            { w => w.WriteLine((uint?)255u, "X"), "FF\n" },
            { w => w.WriteLine((long?)255L, "X"), "FF\n" },
            { w => w.WriteLine((ulong?)255ul, "X"), "FF\n" },
            { w => w.WriteLine((float?)3.14159f, "F2"), "3.14\n" },
            { w => w.WriteLine((double?)3.14159, "F2"), "3.14\n" },
            { w => w.WriteLine((decimal?)3.14159m, "F2"), "3.14\n" },
        };

    [Theory]
    [MemberData(nameof(WriteLineNullWithFormatCases))]
    public void WriteLineNullWithFormatAppendsOnlyNewLine(Action<SourceTextWriter> writeLine)
    {
        var writer = new SourceTextWriter();
        writer.Write("start");
        writeLine(writer);
        Assert.Equal("start\n", writer.ToStringAndReset());
    }

    public static TheoryData<Action<SourceTextWriter>> WriteLineNullWithFormatCases =>
        new()
        {
            w => w.WriteLine((byte?)null, "X"),
            w => w.WriteLine((sbyte?)null, "X"),
            w => w.WriteLine((short?)null, "X"),
            w => w.WriteLine((ushort?)null, "X"),
            w => w.WriteLine((int?)null, "X"),
            w => w.WriteLine((uint?)null, "X"),
            w => w.WriteLine((long?)null, "X"),
            w => w.WriteLine((ulong?)null, "X"),
            w => w.WriteLine((float?)null, "F2"),
            w => w.WriteLine((double?)null, "F2"),
            w => w.WriteLine((decimal?)null, "F2"),
            w => w.WriteLine((object?)null, "X"),
        };
}
