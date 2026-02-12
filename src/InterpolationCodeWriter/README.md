# Raiqub.Generators.InterpolationCodeWriter

[![Build status](https://github.com/skarllot/SourceGenerators/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/skarllot/SourceGenerators/actions)
[![NuGet](https://img.shields.io/nuget/v/Raiqub.Generators.InterpolationCodeWriter)](https://www.nuget.org/packages/Raiqub.Generators.InterpolationCodeWriter/)
[![MIT License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://raw.githubusercontent.com/skarllot/SourceGenerators/main/LICENSE)

A high-performance source text writer for .NET incremental source generators. Write generated source code using plain strings or C# string interpolations with automatic indentation support.

## Installation

```shell
dotnet add package Raiqub.Generators.InterpolationCodeWriter
```

## Quick Start

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

### Key Features

- **Automatic indentation**: Use `PushIndent()` / `PopIndent()` to manage indentation levels (4 spaces per level by default)
- **String interpolation support**: Write code using `$"..."` syntax directly
- **Culture-invariant formatting**: Numbers and other values are formatted using `InvariantCulture` by default
- **Configurable**: Customize line endings, indentation size, and format provider

## Compatibility

Targets .NET Standard 2.0, .NET 8, and .NET 10.

## Documentation

For the full documentation and guides, see the [project repository](https://github.com/skarllot/SourceGenerators).

## License

See the [LICENSE](https://github.com/skarllot/SourceGenerators/blob/main/LICENSE) file for details.
