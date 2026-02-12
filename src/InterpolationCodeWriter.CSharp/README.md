# Raiqub.Generators.InterpolationCodeWriter.CSharp

[![Build status](https://github.com/skarllot/SourceGenerators/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/skarllot/SourceGenerators/actions)
[![NuGet](https://img.shields.io/nuget/v/Raiqub.Generators.InterpolationCodeWriter.CSharp)](https://www.nuget.org/packages/Raiqub.Generators.InterpolationCodeWriter.CSharp/)
[![MIT License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/skarllot/SourceGenerators/main/LICENSE)

Provides Roslyn-specific utilities for .NET incremental source generators, including `CodeWriterDispatcher` and extensions for integrating `SourceTextWriter` with `SourceProductionContext`.

## Installation

```shell
dotnet add package Raiqub.Generators.InterpolationCodeWriter.CSharp
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

Targets .NET Standard 2.0.

## Documentation

For the full documentation and guides, see the [project repository](https://github.com/skarllot/SourceGenerators).

## License

See the [LICENSE](https://github.com/skarllot/SourceGenerators/blob/main/LICENSE) file for details.
