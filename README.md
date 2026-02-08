<div style="text-align: center;">
  <img src="logos/banner-12x4.png" alt="SourceGenerators Logo" height="256" style="border-radius: 15px;" />
</div>

<h1 style="text-align: center;">SourceGenerators</h1>

<div style="text-align: center;">

[![Build status](https://github.com/skarllot/SourceGenerators/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/skarllot/SourceGenerators/actions)
[![OpenSSF Scorecard](https://api.securityscorecards.dev/projects/github.com/skarllot/SourceGenerators/badge)](https://securityscorecards.dev/viewer/?uri=github.com/skarllot/SourceGenerators)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/skarllot/SourceGenerators/main/LICENSE)

</div>

_Provides utility functions and helpers to aid in writing source files for source generators using high-performance writer and string interpolations_

[🏃 Quickstart](#quickstart) &nbsp; | &nbsp; [📗 Guide](#guide) &nbsp; | &nbsp; [📦 NuGet](#nuget-packages)

<hr />

A high-performance source text writer for .NET incremental source generators. Write generated source code using plain
strings or C# string interpolations with automatic indentation support.

## NuGet Packages
* [![NuGet](https://img.shields.io/nuget/v/Raiqub.Generators.InterpolationCodeWriter?label=&logo=nuget&style=flat-square)![NuGet](https://img.shields.io/nuget/dt/Raiqub.Generators.InterpolationCodeWriter?label=&style=flat-square)](https://www.nuget.org/packages/Raiqub.Generators.InterpolationCodeWriter/) **Raiqub.Generators.InterpolationCodeWriter**: provides a compiled library with `SourceTextWriter` for writing source code
* [![NuGet](https://img.shields.io/nuget/v/Raiqub.Generators.InterpolationCodeWriter.CSharp?label=&logo=nuget&style=flat-square)![NuGet](https://img.shields.io/nuget/dt/Raiqub.Generators.InterpolationCodeWriter.CSharp?label=&style=flat-square)](https://www.nuget.org/packages/Raiqub.Generators.InterpolationCodeWriter.CSharp/) **Raiqub.Generators.InterpolationCodeWriter.CSharp**: provides a compiled library with `CodeWriterDispatcher` and extensions for Roslyn source generators
* [![NuGet](https://img.shields.io/nuget/v/Raiqub.Generators.InterpolationCodeWriter.CSharp.Sources?label=&logo=nuget&style=flat-square)![NuGet](https://img.shields.io/nuget/dt/Raiqub.Generators.InterpolationCodeWriter.CSharp.Sources?label=&style=flat-square)](https://www.nuget.org/packages/Raiqub.Generators.InterpolationCodeWriter.CSharp.Sources/) **Raiqub.Generators.InterpolationCodeWriter.CSharp.Sources**: provides source files that are embedded into your project (no runtime dependency)

## Compatibility

The compiled libraries target .NET Standard 2.0, .NET 8, and .NET 10. The source files package requires at least the
.NET 6.0 SDK, but you can target earlier frameworks.

## Quickstart

Add the source files package to your source generator project:

```shell
dotnet add package Raiqub.Generators.InterpolationCodeWriter.CSharp.Sources
```

Then implement the `ICodeWriter<T>` interface to define your code writer:

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

## Guide

### SourceTextWriter

`SourceTextWriter` is a high-performance text writer built on `StringBuilder` with automatic indentation support. It accepts plain strings and C# string interpolations through a custom `InterpolatedStringHandler`.

```csharp
var writer = new SourceTextWriter();

writer.WriteLine("using System;");
writer.WriteLine();
writer.WriteLine($"namespace {ns};");
writer.WriteLine();
writer.WriteLine($"public class {className}");
writer.WriteLine("{");
writer.PushIndent();
writer.WriteLine($"public string Name {{ get; set; }}");
writer.PopIndent();
writer.WriteLine("}");

string result = writer.ToString();
```

Key features:
- **Automatic indentation**: Use `PushIndent()` / `PopIndent()` to manage indentation levels (4 spaces per level by default)
- **String interpolation support**: Write code using `$"..."` syntax directly
- **Culture-invariant formatting**: Numbers and other values are formatted using `InvariantCulture` by default
- **Configurable**: Customize line endings, indentation size, and format provider

### ICodeWriter / ICodeWriter&lt;T&gt;

`ICodeWriter` defines a stateless code writer that generates source code. `ICodeWriter<T>` extends it with model-based generation:

- `CanGenerateFor(T model)` - determines if code should be generated for the given model
- `GetFileName(T model)` - returns the file name for the generated source
- `Write(SourceTextWriter writer, T model)` - writes the source code using the provided writer and model

Implementations should be stateless to ensure thread-safety in source generator contexts.

### CodeWriterDispatcher

`CodeWriterDispatcher<T>` dispatches source generation across multiple code writers. It is stateless and safe to store as a static field:

```csharp
private static readonly CodeWriterDispatcher<MyModel> s_dispatcher =
    new([new MyWriter1(), new MyWriter2()]);
```

Then call `GenerateSources` to generate source files using the `SourceProductionContext`:

```csharp
private static void Emit(
    SourceProductionContext context,
    ImmutableArray<MyModel> types)
{
    s_dispatcher.GenerateSources(types, context);
}
```

An optional exception handler can be provided to report diagnostics instead of throwing:

```csharp
private static readonly CodeWriterDispatcher<MyModel> s_dispatcher =
    new(
        (ex, model) => Diagnostic.Create(...),
        new MyWriter1(), new MyWriter2());
```

## Contributing

If something is not working for you or if you think that the source file
should change, feel free to create an issue or Pull Request.
I will be happy to discuss and potentially integrate your ideas!

## License

See the [LICENSE](./LICENSE) file for details.
