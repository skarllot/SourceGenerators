namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Theory]
    [MemberData(nameof(NumericWriteWithFormatCases))]
    [MemberData(nameof(NumericInterpolatedWithFormatCases))]
    [MemberData(nameof(NullableNumericWriteWithFormatCases))]
    [MemberData(nameof(NullableNumericInterpolatedWithFormatCases))]
    public void WriteNumericWithFormatAppliesFormat(Action<SourceTextWriter> write, string expected)
    {
        var writer = new SourceTextWriter();
        write(writer);
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    public static TheoryData<Action<SourceTextWriter>, string> NumericWriteWithFormatCases =>
        new()
        {
            { w => w.Write((byte)255, "X"), "FF" },
            { w => w.Write((sbyte)127, "X"), "7F" },
            { w => w.Write((short)255, "X"), "FF" },
            { w => w.Write((ushort)255, "X"), "FF" },
            { w => w.Write(255, "X"), "FF" },
            { w => w.Write(255u, "X"), "FF" },
            { w => w.Write(255L, "X"), "FF" },
            { w => w.Write(255ul, "X"), "FF" },
            { w => w.Write(3.14159f, "F2"), "3.14" },
            { w => w.Write(3.14159, "F2"), "3.14" },
            { w => w.Write(3.14159m, "F2"), "3.14" },
            { w => w.Write(42, format: null), "42" },
            { w => w.Write((object)255, "X"), "FF" },
            { w => w.Write<DayOfWeek>(DayOfWeek.Friday, "D"), "5" },
            {
                w => w.Write<DateTime>(new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc), "yyyy-MM-dd"),
                "2025-01-15"
            },
        };

    public static TheoryData<Action<SourceTextWriter>, string> NumericInterpolatedWithFormatCases =>
        new()
        {
            { w => w.Write($"{(byte)255:X}"), "FF" },
            { w => w.Write($"{(sbyte)127:X}"), "7F" },
            { w => w.Write($"{(short)255:X}"), "FF" },
            { w => w.Write($"{(ushort)255:X}"), "FF" },
            { w => w.Write($"{255:X}"), "FF" },
            { w => w.Write($"{255u:X}"), "FF" },
            { w => w.Write($"{255L:X}"), "FF" },
            { w => w.Write($"{255ul:X}"), "FF" },
            { w => w.Write($"{3.14159f:F2}"), "3.14" },
            { w => w.Write($"{3.14159:F2}"), "3.14" },
            { w => w.Write($"{3.14159m:F2}"), "3.14" },
            { w => w.Write($"{(object)255:X}"), "FF" },
            { w => w.Write($"{DayOfWeek.Friday:D}"), "5" },
            { w => w.Write($"{new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc):yyyy-MM-dd}"), "2025-01-15" },
        };

    public static TheoryData<Action<SourceTextWriter>, string> NullableNumericWriteWithFormatCases =>
        new()
        {
            { w => w.Write((byte?)255, "X"), "FF" },
            { w => w.Write((sbyte?)127, "X"), "7F" },
            { w => w.Write((short?)255, "X"), "FF" },
            { w => w.Write((ushort?)255, "X"), "FF" },
            { w => w.Write((int?)255, "X"), "FF" },
            { w => w.Write((uint?)255u, "X"), "FF" },
            { w => w.Write((long?)255L, "X"), "FF" },
            { w => w.Write((ulong?)255ul, "X"), "FF" },
            { w => w.Write((float?)3.14159f, "F2"), "3.14" },
            { w => w.Write((double?)3.14159, "F2"), "3.14" },
            { w => w.Write((decimal?)3.14159m, "F2"), "3.14" },
        };

    public static TheoryData<Action<SourceTextWriter>, string> NullableNumericInterpolatedWithFormatCases =>
        new()
        {
            { w => w.Write($"{(byte?)255:X}"), "FF" },
            { w => w.Write($"{(sbyte?)127:X}"), "7F" },
            { w => w.Write($"{(short?)255:X}"), "FF" },
            { w => w.Write($"{(ushort?)255:X}"), "FF" },
            { w => w.Write($"{(int?)255:X}"), "FF" },
            { w => w.Write($"{(uint?)255u:X}"), "FF" },
            { w => w.Write($"{(long?)255L:X}"), "FF" },
            { w => w.Write($"{(ulong?)255ul:X}"), "FF" },
            { w => w.Write($"{(float?)3.14159f:F2}"), "3.14" },
            { w => w.Write($"{(double?)3.14159:F2}"), "3.14" },
            { w => w.Write($"{(decimal?)3.14159m:F2}"), "3.14" },
        };
}
