using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Raiqub.Generators.InterpolationCodeWriter.Collections;

namespace Raiqub.Generators.InterpolationCodeWriter.Tests.Collections;

public class TextSequenceTest
{
    [Theory]
    [MemberData(nameof(WriteToAndClearCases))]
    public void WriteToAndClearWritesCorrectly(TextSequence seq, string expected)
    {
        var writer = new SourceTextWriter();
        seq.WriteToAndClear(writer);
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    public static TheoryData<TextSequence, string> WriteToAndClearCases =>
        new()
        {
            { TextSequence.Create([]), "" },
            { TextSequence.Create(["hello"]), "hello" },
            { TextSequence.Create(["a", "b", "c"]), "abc" },
            { TextSequence.Create([null, "b", null]), "b" },
            { TextSequence.Create([null, "hello"]), "hello" },
            { TextSequence.Create(["hello", null]), "hello" },
            { TextSequence.Create(["hello\n", null]), "hello\n" },
            { TextSequence.Create(["hello"], appendLine: true), "hello\n" },
            { TextSequence.Create(["a", "b"], appendLine: true), "ab\n" },
        };

    [Theory]
    [MemberData(nameof(EnumerationCases))]
    public void ToArrayAndClearEnumeratesCorrectly(TextSequence seq, string?[] expected)
    {
        Assert.Equal(expected, seq.ToListAndClear());
    }

    public static TheoryData<TextSequence, string?[]> EnumerationCases =>
        new()
        {
            { TextSequence.Create([]), [] },
            { TextSequence.Create(["x", "y", "z"]), ["x", "y", "z"] },
            { TextSequence.Create(["a", null, "b"]), ["a", null, "b"] },
        };

    [Fact]
    public void LargeSequenceUsesArrayPoolAndWritesCorrectly()
    {
        var parts = Enumerable.Range(0, 256).Select(i => i.ToString(CultureInfo.InvariantCulture)).ToArray();
        var seq = TextSequence.Create(parts);
        var writer = new SourceTextWriter();

        seq.WriteToAndClear(writer);

        Assert.Equal(string.Concat(parts), writer.ToStringAndReset());
    }

    [Fact]
    public void LargeInterpolationUsesArrayPoolAndWritesCorrectly()
    {
        const int count = 128; // capacity = count * 2 + 1 = 257 >= ArrayPoolThreshold (256)
        var handler = new TextSequence.CreateInterpolatedStringHandler(0, count);
        for (var i = 0; i < count; i++)
            handler.AppendFormatted(i);
        var seq = handler.Build(appendLine: false);

        var writer = new SourceTextWriter();
        seq.WriteToAndClear(writer);

        var expected = string.Concat(Enumerable.Range(0, count).Select(i => i.ToString(CultureInfo.InvariantCulture)));
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    [Theory]
    [MemberData(nameof(InterpolationWriteCases))]
    public void InterpolationWritesCorrectly(TextSequence seq, string expected)
    {
        var writer = new SourceTextWriter();
        seq.WriteToAndClear(writer);
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSequence, string> InterpolationWriteCases =>
        new()
        {
            // Literal only
            { TextSequence.Create($"hello world"), "hello world" },
            // bool / char
            { TextSequence.Create($"flag={true}"), "flag=True" },
            { TextSequence.Create($"char={'A'}"), "char=A" },
            // Non-nullable integers
            { TextSequence.Create($"{(byte)42}"), "42" },
            { TextSequence.Create($"{(byte)255:X}"), "FF" },
            { TextSequence.Create($"{(sbyte)-1}"), "-1" },
            { TextSequence.Create($"{(short)42}"), "42" },
            { TextSequence.Create($"{(ushort)42}"), "42" },
            { TextSequence.Create($"value={42}"), "value=42" },
            { TextSequence.Create($"value={42:N2}"), "value=42.00" },
            { TextSequence.Create($"{42u}"), "42" },
            { TextSequence.Create($"{42L}"), "42" },
            { TextSequence.Create($"{42ul}"), "42" },
            // Non-nullable floats
            { TextSequence.Create($"{1.5f}"), "1.5" },
            { TextSequence.Create($"{3.14}"), "3.14" },
            { TextSequence.Create($"{9.99m}"), "9.99" },
            { TextSequence.Create($"{9.99m:F1}"), "10.0" },
            // Nullable with value
            { TextSequence.Create($"{(bool?)true}"), "True" },
            { TextSequence.Create($"{(char?)'A'}"), "A" },
            { TextSequence.Create($"{(byte?)42}"), "42" },
            { TextSequence.Create($"{(sbyte?)-1}"), "-1" },
            { TextSequence.Create($"{(short?)42}"), "42" },
            { TextSequence.Create($"{(ushort?)42}"), "42" },
            { TextSequence.Create($"x{(int?)7}y"), "x7y" },
            { TextSequence.Create($"{(uint?)42u}"), "42" },
            { TextSequence.Create($"{(long?)42L}"), "42" },
            { TextSequence.Create($"{(ulong?)42ul}"), "42" },
            { TextSequence.Create($"{(float?)1.5f}"), "1.5" },
            { TextSequence.Create($"{(double?)3.14}"), "3.14" },
            { TextSequence.Create($"{(decimal?)9.99m}"), "9.99" },
            // Nullable null → writes nothing
            { TextSequence.Create($"x{(int?)null}y"), "xy" },
            // string?
            { TextSequence.Create($"""before{"hello"}after"""), "beforehelloafter" },
            { TextSequence.Create($"before{(string?)null}after"), "beforeafter" },
            // object?
            { TextSequence.Create($"{(object)42}"), "42" },
            { TextSequence.Create($"{(object)42:N2}"), "42.00" },
            { TextSequence.Create($"n={(object?)null}"), "n=" },
            // generic
            { TextSequence.Create($"{DayOfWeek.Friday}"), "Friday" },
            { TextSequence.Create($"{DayOfWeek.Friday:D}"), "5" },
            { TextSequence.Create($"{new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc):yyyy-MM-dd}"), "2025-01-15" },
            // Nested TextSequence / TextSequence?
            { TextSequence.Create($"[{TextSequence.Create($"inner")}]"), "[inner]" },
            { TextSequence.Create($"[{(TextSequence?)TextSequence.Create($"inner")}]"), "[inner]" },
            // appendLine
            { TextSequence.Create($"hello", appendLine: true), "hello\n" },
            // Format provider
            { TextSequence.Create(CultureInfo.InvariantCulture, $"pi={Math.PI:F2}"), "pi=3.14" },
        };

    [Theory]
    [MemberData(nameof(AppendLineCases))]
    public void AppendLinePropertyReturnsExpectedValue(TextSequence seq, bool expected)
    {
        Assert.Equal(expected, seq.AppendLine);
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSequence, bool> AppendLineCases =>
        new()
        {
            { TextSequence.Create([]), false },
            { TextSequence.Create([], appendLine: true), true },
            { TextSequence.Create(["hello"]), false },
            { TextSequence.Create(["hello"], appendLine: true), true },
            { TextSequence.Create($"hello"), false },
            { TextSequence.Create($"hello", appendLine: true), true },
        };

    [Theory]
    [MemberData(nameof(DebuggerDisplayCases))]
    public void DebuggerDisplayShowsConcatenatedParts(TextSequence seq, string expected)
    {
        var prop = typeof(TextSequence).GetProperty(
            "DebuggerDisplay",
            BindingFlags.NonPublic | BindingFlags.Instance)!;

        var actual = (string)prop.GetValue(seq)!;

        Assert.Equal(expected, actual);
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<TextSequence, string> DebuggerDisplayCases =>
        new()
        {
            { TextSequence.Create([]), "" },
            { TextSequence.Create(["hello", " ", "world"]), "hello world" },
            { TextSequence.Create(["hello"], appendLine: true), "hello" + Environment.NewLine },
            { TextSequence.Create($"value={42}"), "value=42" },
            { TextSequence.Create($"value={42}", appendLine: true), "value=42" + Environment.NewLine },
        };
}
