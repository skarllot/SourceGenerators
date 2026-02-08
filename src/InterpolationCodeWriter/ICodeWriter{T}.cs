namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>
/// Represents a code writer that generates source code based on a model.
/// </summary>
/// <typeparam name="T">The type of the model used for code generation.</typeparam>
/// <remarks>
/// Implementations should be stateless to ensure thread-safety and predictable behavior
/// in source generator contexts.
/// </remarks>
[System.CodeDom.Compiler.GeneratedCodeAttribute("Raiqub.Generators.InterpolationCodeWriter", GeneratorInfo.Version)]
public interface ICodeWriter<in T>
{
    /// <summary>
    /// Determines whether code should be generated for the specified model.
    /// </summary>
    /// <param name="model">The model to evaluate.</param>
    /// <returns><see langword="true"/> if code should be generated for the model; otherwise, <see langword="false"/>.</returns>
    bool CanGenerateFor(T model);

    /// <summary>
    /// Gets the file name for the generated source code based on the specified model.
    /// </summary>
    /// <param name="model">The model used to determine the file name.</param>
    /// <returns>The file name for the generated code.</returns>
    string GetFileName(T model);

    /// <summary>
    /// Writes the source code to the specified writer using the provided model.
    /// </summary>
    /// <param name="writer">The writer to output the generated source code to.</param>
    /// <param name="model">The model containing the data for code generation.</param>
    void Write(SourceTextWriter writer, T model);
}
