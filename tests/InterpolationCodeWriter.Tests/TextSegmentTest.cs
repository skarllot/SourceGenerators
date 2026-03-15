using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public class TextSegmentTest
{
    [Fact]
    public void LargeInterpolationUsesArrayPoolAndWritesCorrectly()
    {
        const int count = 128; // capacity = count * 2 + 1 = 257 >= ArrayPoolThreshold (256)
        var handler = new TextSegment(0, count);
        for (var i = 0; i < count; i++)
            handler.AppendFormatted(i);

        var writer = new SourceTextWriter();
        handler.WriteToAndClear(writer);

        var expected = string.Concat(Enumerable.Range(0, count).Select(i => i.ToString(CultureInfo.InvariantCulture)));
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    [Theory]
    [MemberData(nameof(InterpolationWriteCases))]
    public void InterpolationWritesCorrectly(TextSegment seq, string expected)
    {
        var writer = new SourceTextWriter();
        seq.WriteToAndClear(writer);
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSegment, string> InterpolationWriteCases =>
        new()
        {
            // Literal only
            { $"hello world", "hello world" },
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
    public void ToListAndClearReturnsAllParts(TextSegment seq, IReadOnlyList<string?> expected)
    {
        var actual = seq.ToListAndClear();
        Assert.Equal(expected, actual);
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSegment, IReadOnlyList<string?>> ToListCases =>
        new()
        {
            // Literal only
            { $"hello world", new List<string?> { "hello world" } },
            // Literal + formatted value
            { $"value={42}", new List<string?> { "value=", "42" } },
            // Null string part
            { $"{(string?)null}", new List<string?> { null } },
            // Nullable null → null entry
            { $"x{(int?)null}y", new List<string?> { "x", null, "y" } },
            // Nested TextSegment: inner parts are flattened into the list
            { $"[{TextSegment.Create($"inner")}]", new List<string?> { "[", "inner", "]" } },
            // Nested nullable TextSegment
            { $"[{(TextSegment?)TextSegment.Create($"inner")}]", new List<string?> { "[", "inner", "]" } },
        };

    [Fact]
    public void ToListAndClearLargeInterpolationUsesArrayPoolAndReturnsCorrectly()
    {
        const int count = 128; // capacity = count * 2 + 1 = 257 >= ArrayPoolThreshold (256)
        var handler = new TextSegment(0, count);
        for (var i = 0; i < count; i++)
            handler.AppendFormatted(i);

        var actual = handler.ToListAndClear();

        var expected = Enumerable.Range(0, count).Select(i => i.ToString(CultureInfo.InvariantCulture)).ToList();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(WithAppendLineCases))]
    public void WithAppendLineSetsFlag(TextSegment seq, bool value, bool expected)
    {
        Assert.Equal(expected, seq.WithAppendLine(value).AppendLine);
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSegment, bool, bool> WithAppendLineCases =>
        new()
        {
            // Already false → false (returns same)
            { TextSegment.Create($"hello"), false, false },
            // false → true
            { TextSegment.Create($"hello"), true, true },
            // Already true → true (returns same)
            { TextSegment.Create($"hello", appendLine: true), true, true },
            // true → false
            { TextSegment.Create($"hello", appendLine: true), false, false },
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
}
