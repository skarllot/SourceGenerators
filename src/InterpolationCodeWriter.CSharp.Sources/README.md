# Raiqub.Generators.InterpolationCodeWriter.CSharp.Sources

[![Build status](https://github.com/skarllot/SourceGenerators/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/skarllot/SourceGenerators/actions)
[![NuGet](https://img.shields.io/nuget/v/Raiqub.Generators.InterpolationCodeWriter.CSharp.Sources)](https://www.nuget.org/packages/Raiqub.Generators.InterpolationCodeWriter.CSharp.Sources/)
[![MIT License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/skarllot/SourceGenerators/main/LICENSE)

Provides source files for writing source generators that are embedded directly into your project. This is a development-only dependency with no runtime overhead.

This is the **recommended** package for most source generator projects. It embeds the source code of both `InterpolationCodeWriter` and `InterpolationCodeWriter.CSharp` directly into your project, avoiding any external assembly dependency.

## Installation

```shell
dotnet add package Raiqub.Generators.InterpolationCodeWriter.CSharp.Sources
```

## Quick Start

Implement `ICodeWriter<T>` to define a code writer and use `CodeWriterDispatcher<T>` to dispatch source generation:

```csharp
public class MyWriter : ICodeWriter<MyModel>
{
    public bool CanGenerateFor(MyModel model) => model.Properties.Count > 0;

    public string GetFileName(MyModel model) => $"{model.Namespace ?? "_"}.{model.Name}Extensions.g.cs";

    public void Write(SourceTextWriter writer, MyModel model)
    {
        writer.WriteLine($"namespace {model.Namespace};");
        writer.WriteLine();
        writer.WriteLine($"public static partial class {model.Name}Extensions");
        writer.WriteLine("{");
        writer.PushIndent();
        // Write members using string interpolation...
        writer.PopIndent();
        writer.WriteLine("}");
    }
}
```

Then dispatch generation using `CodeWriterDispatcher<T>`:

```csharp
private static readonly CodeWriterDispatcher<MyModel> s_dispatcher =
    new([new MyWriter()]);

private static void Emit(
    SourceProductionContext context,
    ImmutableArray<MyModel> types)
{
    s_dispatcher.GenerateSources(types, context);
}
```

## Compatibility

Requires .NET 6.0 SDK or later, but you can target earlier frameworks.

## Documentation

For the full documentation and guides, see the [project repository](https://github.com/skarllot/SourceGenerators).

## License

See the [LICENSE](https://github.com/skarllot/SourceGenerators/blob/main/LICENSE) file for details.
