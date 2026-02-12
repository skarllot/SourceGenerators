namespace Raiqub.Generators.InterpolationCodeWriter.Tests;

public partial class SourceTextWriterTest
{
    [Fact]
    public void PushIndentDefaultIncrementsByOneLevel()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("first");
        writer.Write("second");

        Assert.Equal("first\n    second", writer.ToStringAndReset());
    }

    [Fact]
    public void PushIndentByMultipleLevels()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent(3);
        writer.WriteLine("first");
        writer.Write("second");

        Assert.Equal("first\n            second", writer.ToStringAndReset());
    }

    [Fact]
    public void PopIndentDefaultDecrementsByOneLevel()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent(2);
        writer.PopIndent();
        writer.WriteLine("first");
        writer.Write("second");

        Assert.Equal("first\n    second", writer.ToStringAndReset());
    }

    [Fact]
    public void PopIndentByMultipleLevels()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent(3);
        writer.PopIndent(2);
        writer.WriteLine("first");
        writer.Write("second");

        Assert.Equal("first\n    second", writer.ToStringAndReset());
    }

    [Fact]
    public void PopIndentBelowZeroThrowsInvalidOperationException()
    {
        var writer = new SourceTextWriter();

        Assert.Throws<InvalidOperationException>(() => writer.PopIndent());
    }

    [Fact]
    public void PopIndentExactlyToZeroDoesNotThrow()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent(2);
        writer.PopIndent(2);
        writer.WriteLine("first");
        writer.Write("second");

        Assert.Equal("first\nsecond", writer.ToStringAndReset());
    }

    [Fact]
    public void ClearIndentResetsToZero()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent(3);
        writer.ClearIndent();
        writer.WriteLine("first");
        writer.Write("second");

        Assert.Equal("first\nsecond", writer.ToStringAndReset());
    }

    [Fact]
    public void IndentationAppliesAfterNewLine()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.Write("first");
        writer.WriteLine();
        writer.Write("second");

        Assert.Equal("first\n    second", writer.ToStringAndReset());
    }

    [Fact]
    public void IndentationDoesNotApplyWithoutNewLine()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.Write("first");
        writer.Write("second");

        Assert.Equal("firstsecond", writer.ToStringAndReset());
    }

    [Fact]
    public void CustomCharsPerIndentation()
    {
        var writer = new SourceTextWriter(charsPerIndentation: 2);
        writer.PushIndent();
        writer.WriteLine("first");
        writer.Write("second");

        Assert.Equal("first\n  second", writer.ToStringAndReset());
    }

    [Fact]
    public void MultipleIndentationLevelsAccumulate()
    {
        var writer = new SourceTextWriter();
        writer.PushIndent();
        writer.WriteLine("level0");
        writer.Write("level1");
        writer.PushIndent();
        writer.WriteLine();
        writer.Write("level2");
        writer.PopIndent();
        writer.WriteLine();
        writer.Write("level1again");

        Assert.Equal("level0\n    level1\n        level2\n    level1again", writer.ToStringAndReset());
    }
}
