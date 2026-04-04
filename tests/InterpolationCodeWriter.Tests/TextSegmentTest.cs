using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public class TextSegmentTest
{
    [Theory]
    [MemberData(nameof(InterpolationWriteCases))]
    public void InterpolationWritesCorrectly(TextSegment seq, string expected)
    {
        var writer = new SourceTextWriter();
        seq.WriteTo(writer);
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSegment, string> InterpolationWriteCases =>
        new()
        {
            // Literal only
            { $"hello world", "hello world" },
            // Pure string
            { "hello world", "hello world" },
            { null, "" },
            // bool / char
            { $"flag={true}", "flag=True" },
            { $"char={'A'}", "char=A" },
            // Non-nullable integers
            { $"{(byte)42}", "42" },
            { $"{(byte)255:X}", "FF" },
            { $"{(sbyte)-1}", "-1" },
            { $"{(short)42}", "42" },
            { $"{(ushort)42}", "42" },
            { $"value={42}", "value=42" },
            { $"value={42:N2}", "value=42.00" },
            { $"{42u}", "42" },
            { $"{42L}", "42" },
            { $"{42ul}", "42" },
            // Non-nullable floats
            { $"{1.5f}", "1.5" },
            { $"{3.14}", "3.14" },
            { $"{9.99m}", "9.99" },
            { $"{9.99m:F1}", "10.0" },
            // Nullable with value
            { $"{(bool?)true}", "True" },
            { $"{(char?)'A'}", "A" },
            { $"{(byte?)42}", "42" },
            { $"{(sbyte?)-1}", "-1" },
            { $"{(short?)42}", "42" },
            { $"{(ushort?)42}", "42" },
            { $"x{(int?)7}y", "x7y" },
            { $"{(uint?)42u}", "42" },
            { $"{(long?)42L}", "42" },
            { $"{(ulong?)42ul}", "42" },
            { $"{(float?)1.5f}", "1.5" },
            { $"{(double?)3.14}", "3.14" },
            { $"{(decimal?)9.99m}", "9.99" },
            // Nullable null → writes nothing
            { $"x{(int?)null}y", "xy" },
            // string?
            { $"""before{"hello"}after""", "beforehelloafter" },
            { $"before{(string?)null}after", "beforeafter" },
            // object?
            { $"{(object)42}", "42" },
            { $"{(object)42:N2}", "42.00" },
            { $"n={(object?)null}", "n=" },
            // generic
            { $"{DayOfWeek.Friday}", "Friday" },
            { $"{DayOfWeek.Friday:D}", "5" },
            { $"{new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc):yyyy-MM-dd}", "2025-01-15" },
            // Nested TextSegment / TextSegment?
            { $"[{TextSegment.Create($"inner")}]", "[inner]" },
            { $"[{(TextSegment?)TextSegment.Create($"inner")}]", "[inner]" },
            // appendLine
            { TextSegment.Create($"hello", appendLine: true), "hello\n" },
            // Format provider
            { TextSegment.Create(CultureInfo.InvariantCulture, $"pi={Math.PI:F2}"), "pi=3.14" },
        };

    [Theory]
    [MemberData(nameof(AppendLineCases))]
    public void AppendLinePropertyReturnsExpectedValue(TextSegment seq, bool expected)
    {
        Assert.Equal(expected, seq.AppendLine);
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSegment, bool> AppendLineCases =>
        new() { { TextSegment.Create($"hello"), false }, { TextSegment.Create($"hello", appendLine: true), true } };

    [Theory]
    [MemberData(nameof(ToListCases))]
    public void ToListReturnsAllParts(TextSegment seq, IReadOnlyList<string?> expected)
    {
        var actual = seq.ToList();
        Assert.Equal(expected, actual);
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSegment, IReadOnlyList<string?>> ToListCases =>
        new()
        {
            // Literal only
            {
                $"hello world",
                new List<string?> { "hello world" }
            },
            // Literal + formatted value
            {
                $"value={42}",
                new List<string?> { "value=", "42" }
            },
            // Null string part
            {
                $"{(string?)null}",
                new List<string?> { null }
            },
            // Nullable null → null entry
            {
                $"x{(int?)null}y",
                new List<string?> { "x", null, "y" }
            },
            // Nested TextSegment: inner parts are flattened into the list
            {
                $"[{TextSegment.Create($"inner")}]",
                new List<string?> { "[", "inner", "]" }
            },
            // Nested nullable TextSegment
            {
                $"[{(TextSegment?)TextSegment.Create($"inner")}]",
                new List<string?> { "[", "inner", "]" }
            },
        };

    [Theory]
    [MemberData(nameof(DebuggerDisplayCases))]
    public void DebuggerDisplayShowsConcatenatedParts(TextSegment seq, string expected)
    {
        var prop = typeof(TextSegment).GetProperty("DebuggerDisplay", BindingFlags.NonPublic | BindingFlags.Instance)!;

        var actual = (string)prop.GetValue(seq)!;

        Assert.Equal(expected, actual);
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSegment, string> DebuggerDisplayCases =>
        new()
        {
            { TextSegment.Create($"value={42}"), "value=42" },
            { TextSegment.Create($"value={42}", appendLine: true), "value=42" + Environment.NewLine },
        };

    [Theory]
    [MemberData(nameof(WriteToWithAppendLineOverrideCases))]
    public void WriteToOverrideAppendLineWritesCorrectly(TextSegment seq, bool appendLine, string expected)
    {
        var writer = new SourceTextWriter();
        seq.WriteTo(writer, appendLine);
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSegment, bool, string> WriteToWithAppendLineOverrideCases =>
        new()
        {
            // Override false → suppresses the stored appendLine=true
            { TextSegment.Create($"hello", appendLine: true), false, "hello" },
            // Override true → appends line even though stored appendLine=false
            { TextSegment.Create($"hello", appendLine: false), true, "hello\n" },
            // No-op override: matches stored value (false)
            { TextSegment.Create($"hello"), false, "hello" },
            // No-op override: matches stored value (true)
            { TextSegment.Create($"hello", appendLine: true), true, "hello\n" },
        };

    [Fact]
    public void GrowFromMinimumCapacityPreservesAllParts()
    {
        // capacity starts at 1 (formattedCount=0 → 0*2+1=1)
        // second AppendLiteral triggers Grow: requiredMinCapacity=2, clamped to minimum 4
        var seg = new TextSegment(0, 0);
        seg.AppendLiteral("a");
        seg.AppendLiteral("b");

        var writer = new SourceTextWriter();
        seg.WriteTo(writer);

        Assert.Equal("ab", writer.ToStringAndReset());
    }

    [Fact]
    public void GrowDoublesCapacityWhenDoubleSuffices()
    {
        // Fill to capacity 4 (after first grow), then trigger a second grow to 8
        var seg = new TextSegment(0, 0);
        seg.AppendLiteral("1");
        seg.AppendLiteral("2"); // grow 1→4
        seg.AppendLiteral("3");
        seg.AppendLiteral("4");
        seg.AppendLiteral("5"); // grow 4→8

        var writer = new SourceTextWriter();
        seg.WriteTo(writer);

        Assert.Equal("12345", writer.ToStringAndReset());
    }

    [Fact]
    public void GrowMultipleTimesPreservesAllPartsInOrder()
    {
        // 1→4→8→16: verifies each resize keeps all prior elements intact
        var seg = new TextSegment(0, 0);
        for (int i = 1; i <= 9; i++)
        {
            seg.AppendLiteral(i.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        var writer = new SourceTextWriter();
        seg.WriteTo(writer);

        Assert.Equal("123456789", writer.ToStringAndReset());
    }

    [Fact]
    public void GrowPreservesPartsReturnedByToList()
    {
        var seg = new TextSegment(0, 0);
        seg.AppendLiteral("x");
        seg.AppendFormatted(42);  // grow 1→4
        seg.AppendLiteral("y");

        Assert.Equal(new List<string?> { "x", "42", "y" }, seg.ToList());
    }

    [Fact]
    public void GrowWithNestedTextSegmentAcrossMultipleResizes()
    {
        // Interleave literals and nested TextSegments to trigger 1→4→8 resizes
        // and verify nested segments render correctly throughout
        TextSegment inner1 = $"A";
        TextSegment inner2 = $"B";
        TextSegment inner3 = $"C";
        TextSegment inner4 = $"D";
        var seg = new TextSegment(0, 0);
        seg.AppendLiteral("[");
        seg.AppendFormatted(inner1); // grow 1→4
        seg.AppendLiteral(",");
        seg.AppendFormatted(inner2);
        seg.AppendLiteral(",");      // grow 4→8
        seg.AppendFormatted(inner3);
        seg.AppendLiteral(",");
        seg.AppendFormatted(inner4);
        seg.AppendLiteral("]");     // grow 8→16

        var writer = new SourceTextWriter();
        seg.WriteTo(writer);

        Assert.Equal("[A,B,C,D]", writer.ToStringAndReset());
    }

    [Fact]
    public void GrowWithNullableTextSegmentAcrossMultipleResizes()
    {
        // Mix non-null and null TextSegment? values across multiple resizes
        TextSegment? present = (TextSegment)$"X";
        TextSegment? absent = null;
        var seg = new TextSegment(0, 0);
        seg.AppendLiteral("[");
        seg.AppendFormatted(present); // grow 1→4
        seg.AppendLiteral(",");
        seg.AppendFormatted(absent);
        seg.AppendLiteral(",");       // grow 4→8
        seg.AppendFormatted(present);
        seg.AppendLiteral(",");
        seg.AppendFormatted(absent);
        seg.AppendLiteral("]");       // grow 8→16

        var writer = new SourceTextWriter();
        seg.WriteTo(writer);

        Assert.Equal("[X,,X,]", writer.ToStringAndReset());
    }
}
