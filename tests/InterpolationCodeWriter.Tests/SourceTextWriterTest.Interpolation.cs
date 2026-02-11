namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Fact]
    public void WriteInterpolatedStringWithLiteralsOnly()
    {
        var writer = new SourceTextWriter();
        writer.Write($"hello world");

        Assert.Equal("hello world", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedStringWithIntValue()
    {
        var writer = new SourceTextWriter();
        var x = 42;
        writer.Write($"value={x}");

        Assert.Equal("value=42", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedStringWithBoolValue()
    {
        var writer = new SourceTextWriter();
        var b = true;
        writer.Write($"flag={b}");

        Assert.Equal("flag=True", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedStringWithMultipleTypedValues()
    {
        var writer = new SourceTextWriter();
        var n = 1;
        var c = 'A';
        var b = false;
        writer.Write($"{n}-{c}-{b}");

        Assert.Equal("1-A-False", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedStringWithNullStringSkipsNull()
    {
        var writer = new SourceTextWriter();
        string? s = null;
        writer.Write($"before{s}after");

        Assert.Equal("beforeafter", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedStringWithNullNullableIntSkipsValue()
    {
        var writer = new SourceTextWriter();
        int? n = null;
        writer.Write($"x{n}y");

        Assert.Equal("xy", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedStringWithNonNullNullableWritesValue()
    {
        var writer = new SourceTextWriter();
        int? n = 7;
        writer.Write($"x{n}y");

        Assert.Equal("x7y", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedStringWithObjectValue()
    {
        var writer = new SourceTextWriter();
        object obj = new CustomToStringObject("hello");
        writer.Write($"val={obj}");

        Assert.Equal("val=hello", writer.ToStringAndReset());
    }

    [Fact]
    public void WriteInterpolatedStringWithNullObjectSkipsValue()
    {
        var writer = new SourceTextWriter();
        object? o = null;
        writer.Write($"a{o}b");

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

    private sealed class CustomToStringObject(string value)
    {
        public override string ToString() => value;
    }
}
