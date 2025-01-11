# SourceGenerators

[![Build status](https://github.com/skarllot/SourceGenerators/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/skarllot/SourceGenerators/actions)
[![OpenSSF Scorecard](https://api.securityscorecards.dev/projects/github.com/skarllot/SourceGenerators/badge)](https://securityscorecards.dev/viewer/?uri=github.com/skarllot/SourceGenerators)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/skarllot/SourceGenerators/main/LICENSE)

_Provides utility functions and helpers to aid in writing source files for source generators based on T4 templates_

[🏃 Quickstart](#quickstart) &nbsp; | &nbsp; [📗 Guide](#guide) &nbsp; | &nbsp; [📦 NuGet](#nuget-packages)

<hr />

Provides a high performance T4 base class on compiled library or source files package.

## NuGet Packages
* [![NuGet](https://img.shields.io/nuget/v/Raiqub.T4Template?label=&logo=nuget&style=flat-square)![NuGet](https://img.shields.io/nuget/dt/Raiqub.T4Template?label=&style=flat-square)](https://www.nuget.org/packages/Raiqub.T4Template/) **Raiqub.T4Template**: provides a compiled library with a high performance T4 base class for general use
* [![NuGet](https://img.shields.io/nuget/v/Raiqub.Generators.T4CodeWriter?label=&logo=nuget&style=flat-square)![NuGet](https://img.shields.io/nuget/dt/Raiqub.Generators.T4CodeWriter?label=&style=flat-square)](https://www.nuget.org/packages/Raiqub.Generators.T4CodeWriter/) **Raiqub.Generators.T4CodeWriter**: provides a compiled library with a high performance T4 base class for code generation
* [![NuGet](https://img.shields.io/nuget/v/Raiqub.Generators.T4CodeWriter.Sources?label=&logo=nuget&style=flat-square)![NuGet](https://img.shields.io/nuget/dt/Raiqub.Generators.T4CodeWriter.Sources?label=&style=flat-square)](https://www.nuget.org/packages/Raiqub.Generators.T4CodeWriter.Sources/) **Raiqub.Generators.T4CodeWriter.Sources**: provides source files with a high performance T4 base class

## Compatibility

Raiqub T4 Code Writer library requires .NET Standard 2.0 runtime to run, and the source files requires at least the .NET 6 SDK to run, but you can target earlier frameworks.

## Quickstart

Add the package to your application using

```shell
dotnet add package Raiqub.Generators.T4CodeWriter.Sources
```

Adding the package the base class `CodeWriterBase<T>` will be available as a base class for T4 templates.

## Guide

### CodeWriterBase / T4TemplateBase

To override the base class of a T4 template set the [inherits attribute](https://learn.microsoft.com/en-us/visualstudio/modeling/t4-template-directive?view=vs-2022#inherits-attribute). For example:

```t4
<#@ template debug="false" linePragmas="false" hostspecific="false" language="C#" inherits="CodeWriterBase<MyModel>" #>
```

The constructor of `CodeWriterBase` class need to receive a `StringBuilder` instance, then T4 templates that inherits from `CodeWriterBase` need to define a constructor passing a `StringBuilder` instance to base class. For example:

```t4
<#+
    public MyWriter(StringBuilder builder) : base(builder)
    {
    }
#>
```

Additionally the method `GetFileName` must be implemented to define the name of file of the generated source. For example:

```t4
<#+
    public override string GetFileName() => $"{Model.Namespace ?? "_"}.{Model.Name}Extensions.g.cs";
#>
```

Optionally the method `CanGenerateFor` can be implemented to determine if source code should be generated according with the specified model state. For example:

```t4
<#+
    protected override bool CanGenerateFor(MyModel model) => model.Properties.Count > 0;
#>
```

### CodeWriterDispatcher

The `CodeWriterDispatcher` is a dispatcher for code writers that generate compilation source. As it does not mutate any internal state is safe to define it as a static readonly field. Example:

```csharp
private static readonly CodeWriterDispatcher<MyModel> s_dispatcher =
    new(
        sb => new MyWriter1(sb),
        sb => new MyWriter2(sb));
```

Then just call the `GenerateSources` method to generate source files using the `SourceProductionContext`. For example:

```csharp
private static void Emit(
    Compilation compilation,
    SourceProductionContext context,
    ImmutableArray<MyModel> types)
{
    // ...
    s_dispatcher.GenerateSources(typesToGenerate, context);
}
```

## Contributing

If something is not working for you or if you think that the source file
should change, feel free to create an issue or Pull Request.
I will be happy to discuss and potentially integrate your ideas!

## License

See the [LICENSE](./LICENSE) file for details.
