# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A high-performance source text writer library for .NET incremental source generators. Provides `SourceTextWriter` with automatic indentation and C# string interpolation support via a custom `InterpolatedStringHandler`. Published as three NuGet packages under the `Raiqub.Generators` namespace.

## Build Notes

- Solution file is `SourceGenerators.slnx` (XML-based slnx format)
- Git submodule `modules/polyfill` must be initialized (`git submodule update --init`)

## Project Structure

### Source Projects (`src/`)

| Project | NuGet Package | Purpose |
|---------|--------------|---------|
| `InterpolationCodeWriter` | `Raiqub.Generators.InterpolationCodeWriter` | Core `SourceTextWriter` with indentation and interpolation. Targets netstandard2.0, net8.0, net10.0 |
| `InterpolationCodeWriter.CSharp` | `Raiqub.Generators.InterpolationCodeWriter.CSharp` | Roslyn integration: `CodeWriterDispatcher<T>`, `ICodeWriter<T>`, extensions for `SourceProductionContext`. Targets netstandard2.0 |
| `InterpolationCodeWriter.CSharp.Sources` | `Raiqub.Generators.InterpolationCodeWriter.CSharp.Sources` | Source-only package (no runtime dependency). Uses `Microsoft.Build.NoTargets` SDK to bundle all `.cs` files from the above projects as embedded content files |
| `InternalPolyfill` | *(not packaged)* | Polyfills for netstandard2.0 compatibility. Shared via `<Compile Include>` linking, not project references |

### Test Projects (`tests/`)

- `InterpolationCodeWriter.Tests` — tests for core writer (references `InternalPolyfill` + `InterpolationCodeWriter`)
- `InterpolationCodeWriter.CSharp.Tests` — tests for Roslyn integration
- `InterpolationCodeWriter.CSharp10.Tests` — tests targeting C# 10 language features

Tests use **xUnit** with central package management (`tests/Directory.Packages.props`).

## Architecture Notes

- **Dual compilation model**: Projects define `IS_COMPILED` constant. The same source files are compiled as a library (InterpolationCodeWriter) and embedded as source files (InterpolationCodeWriter.CSharp.Sources). Code may use `#if IS_COMPILED` for conditional compilation.
- **InternalPolyfill sharing**: `InternalPolyfill` files are included via `<Compile Include="..\InternalPolyfill\**\*.cs">` into projects that target netstandard2.0, not via project reference.
- **SourceTextWriter is partial**: Split across `SourceTextWriter.cs`, `SourceTextWriter.Write.cs`, `SourceTextWriter.WriteLine.cs`, `SourceTextWriter.Indentation.cs`, and `SourceTextWriter.Interpolation.cs`.
- **Versioning**: Uses Nerdbank.GitVersioning (`version.json`). Version path filter is `src/` only.

## Coding Conventions

- Root namespace pattern: `Raiqub.Generators.{ProjectName}` (set in `Directory.Build.props`)
- C# language version: 12 globally, 14 for net10.0 targets, 10 for InternalPolyfill
- Nullable: disabled globally; enabled globally in test projects; source files must opt-in with `#nullable enable`
- Implicit usings: disabled globally (enabled in test projects)
- Warnings as errors enabled
- Static fields: `s_` prefix. Private/internal fields: `_camelCase`
- Constants: PascalCase
- Braces on new lines (Allman style)
