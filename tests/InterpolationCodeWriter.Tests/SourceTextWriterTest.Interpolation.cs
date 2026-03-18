using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Theory]
    [MemberData(nameof(WriteInterpolationCases))]
    public void WriteInterpolatedStringHandlerWritesCorrectly(Action<SourceTextWriter> write, string expected)
    {
        var writer = new SourceTextWriter();
        write(writer);
        Assert.Equal(expected, writer.ToStringAndReset());
    }

    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider")]
    public static TheoryData<Action<SourceTextWriter>, string> WriteInterpolationCases =>
        new()
        {
            // AppendLiteral
            { w => w.Write($"hello world"), "hello world" },
            // AppendFormatted(bool)
            { w => w.Write($"flag={true}"), "flag=True" },
            { w => w.Write($"flag={false}"), "flag=False" },
            // AppendFormatted(char)
            { w => w.Write($"char={'A'}"), "char=A" },
            // AppendFormatted(byte) / AppendFormatted(byte, string?)
            { w => w.Write($"{(byte)42}"), "42" },
            { w => w.Write($"{(byte)255:X}"), "FF" },
            // AppendFormatted(sbyte)
            { w => w.Write($"{(sbyte)-1}"), "-1" },
            // AppendFormatted(short) / AppendFormatted(short, string?)
            { w => w.Write($"{(short)42}"), "42" },
            { w => w.Write($"{(short)1000:N0}"), "1,000" },
            // AppendFormatted(ushort)
            { w => w.Write($"{(ushort)42}"), "42" },
            // AppendFormatted(int) / AppendFormatted(int, string?)
            { w => w.Write($"value={42}"), "value=42" },
            { w => w.Write($"value={42:N2}"), "value=42.00" },
            // AppendFormatted(uint)
            { w => w.Write($"{42u}"), "42" },
            // AppendFormatted(long)
            { w => w.Write($"{42L}"), "42" },
            // AppendFormatted(ulong)
            { w => w.Write($"{42ul}"), "42" },
            // AppendFormatted(float)
            { w => w.Write($"{1.5f}"), "1.5" },
            // AppendFormatted(double) / AppendFormatted(double, string?)
            { w => w.Write($"{3.14}"), "3.14" },
            { w => w.Write($"{Math.PI:F4}"), "3.1416" },
            // AppendFormatted(decimal) / AppendFormatted(decimal, string?)
            { w => w.Write($"{9.99m}"), "9.99" },
            { w => w.Write($"{9.99m:F1}"), "10.0" },
            // AppendFormatted(bool?) - with value and null
            { w => w.Write($"{(bool?)true}"), "True" },
            { w => w.Write($"x{(bool?)null}y"), "xy" },
            // AppendFormatted(char?) - with value and null
            { w => w.Write($"{(char?)'Z'}"), "Z" },
            { w => w.Write($"x{(char?)null}y"), "xy" },
            // AppendFormatted(byte?) / AppendFormatted(byte?, string?)
            { w => w.Write($"{(byte?)42}"), "42" },
            { w => w.Write($"{(byte?)255:X}"), "FF" },
            { w => w.Write($"x{(byte?)null}y"), "xy" },
            // AppendFormatted(sbyte?)
            { w => w.Write($"{(sbyte?)-1}"), "-1" },
            { w => w.Write($"x{(sbyte?)null}y"), "xy" },
            // AppendFormatted(short?)
            { w => w.Write($"{(short?)42}"), "42" },
            { w => w.Write($"x{(short?)null}y"), "xy" },
            // AppendFormatted(ushort?)
            { w => w.Write($"{(ushort?)42}"), "42" },
            { w => w.Write($"x{(ushort?)null}y"), "xy" },
            // AppendFormatted(int?) / AppendFormatted(int?, string?)
            { w => w.Write($"x{(int?)7}y"), "x7y" },
            { w => w.Write($"{(int?)42:N2}"), "42.00" },
            { w => w.Write($"x{(int?)null}y"), "xy" },
            // AppendFormatted(uint?)
            { w => w.Write($"{(uint?)42u}"), "42" },
            { w => w.Write($"x{(uint?)null}y"), "xy" },
            // AppendFormatted(long?)
            { w => w.Write($"{(long?)42L}"), "42" },
            { w => w.Write($"x{(long?)null}y"), "xy" },
            // AppendFormatted(ulong?)
            { w => w.Write($"{(ulong?)42ul}"), "42" },
            { w => w.Write($"x{(ulong?)null}y"), "xy" },
            // AppendFormatted(float?)
            { w => w.Write($"{(float?)1.5f}"), "1.5" },
            { w => w.Write($"x{(float?)null}y"), "xy" },
            // AppendFormatted(double?)
            { w => w.Write($"{(double?)3.14}"), "3.14" },
            { w => w.Write($"x{(double?)null}y"), "xy" },
            // AppendFormatted(decimal?)
            { w => w.Write($"{(decimal?)9.99m}"), "9.99" },
            { w => w.Write($"x{(decimal?)null}y"), "xy" },
            // AppendFormatted(string?) - non-null and null
            { w => w.Write($"""before{"hello"}after"""), "beforehelloafter" },
            { w => w.Write($"before{(string?)null}after"), "beforeafter" },
            // AppendFormatted(object?) - non-null (non-IFormattable), null, with format
            { w => { object obj = new CustomToStringObject("hello"); w.Write($"val={obj}"); }, "val=hello" },
            { w => w.Write($"a{(object?)null}b"), "ab" },
            { w => w.Write($"{(object)42:N2}"), "42.00" },
            // AppendFormatted<T>(T) / AppendFormatted<T>(T, string?) - generic via enum
            { w => w.Write($"{DayOfWeek.Friday}"), "Friday" },
            { w => w.Write($"{DayOfWeek.Friday:D}"), "5" },
            // AppendFormatted(string?[]) - null parts are skipped
            { w => { var arr = new string?[] { "x", null, "y" }; w.Write($"{arr}"); }, "xy" },
            // AppendFormatted(IReadOnlyList<string?>) - null parts are skipped
            { w => { IReadOnlyList<string?> list = new List<string?> { "a", null, "b" }; w.Write($"{list}"); }, "ab" },
            // Multiple mixed values in one interpolation
            { w => { int n = 1; char c = 'A'; bool b = false; w.Write($"{n}-{c}-{b}"); }, "1-A-False" },
        };

    [Fact]
    public void WriteInterpolatedStringWithReadOnlySpanOfChar()
    {
        var writer = new SourceTextWriter();
        ReadOnlySpan<char> span = "hello span".AsSpan();
        writer.Write($"{span}");

        Assert.Equal("hello span", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedStringWithReadOnlySpanOfStrings()
    {
        var writer = new SourceTextWriter();
        ReadOnlySpan<string?> parts = new string?[] { "a", null, "b" };
        writer.Write($"{parts}");

        Assert.Equal("ab", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedMultiLineStringWithIndentation()
    {
        var writer = new SourceTextWriter();
        var className = "MyClass";
        var methodName = "DoWork";
        writer.PushIndent();
        writer.WriteLine("public class {");
        writer.Write(
            $@"void {methodName}()
{{
    Console.WriteLine(""{className}"");
}}"
        );

        Assert.Equal(
            "public class {\n    void DoWork()\n    {\n        Console.WriteLine(\"MyClass\");\n    }",
            writer.ToStringAndReset()
        );
    }

    [Fact]
    public void WriteNestedInterpolated()
    {
        var writer = new SourceTextWriter();
        writer.Write($"<{NestedInterpolate(writer)}>");

        Assert.Equal("<nested>", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteNestedSegmentInterpolated()
    {
        var writer = new SourceTextWriter();
        writer.Write($"<{SegmentInterpolate("John")}>");

        Assert.Equal("<Hello John!>", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteNullableSegmentWithValue()
    {
        var writer = new SourceTextWriter();
        TextSegment? seg = SegmentInterpolate("John");
        writer.Write($"<{seg}>");

        Assert.Equal("<Hello John!>", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteNullableSegmentWithNull()
    {
        var writer = new SourceTextWriter();
        TextSegment? seg = null;
        writer.Write($"<{seg}>");

        Assert.Equal("<>", writer.ToStringAndReset());
    }

    private static EmptyResult NestedInterpolate(SourceTextWriter writer)
    {
        writer.Write("nested");
        return default;
    }

    private static TextSegment SegmentInterpolate(string name) => $"Hello {name}!";

    private sealed class CustomToStringObject(string value)
    {
        public override string ToString() => value;
    }
}
