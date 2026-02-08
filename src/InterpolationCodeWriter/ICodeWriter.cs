namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>
/// Represents a code writer that generates source code.
/// </summary>
/// <remarks>
/// Implementations should be stateless to ensure thread-safety and predictable behavior
/// in source generator contexts.
/// </remarks>
public interface ICodeWriter
{
    /// <summary>
    /// Gets the file name for the generated source code.
    /// </summary>
    /// <returns>The file name for the generated code.</returns>
    string GetFileName();

    /// <summary>
    /// Writes the source code to the specified writer.
    /// </summary>
    /// <param name="writer">The writer to output the generated source code to.</param>
    void Write(SourceTextWriter writer);
}
