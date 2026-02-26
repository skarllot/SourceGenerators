using System.Diagnostics.CodeAnalysis;

namespace Raiqub.Generators.InterpolationCodeWriter;

/// <summary>Represents an empty result that writes nothing to the output.</summary>
/// <remarks>
/// Use this type as the return value of a method called within an interpolated string
/// to indicate that the method handles writing directly to the <see cref="SourceTextWriter"/>
/// and produces no interpolated output itself.
/// </remarks>
public readonly record struct EmptyResult
{
    /// <summary>Gets the singleton empty result value.</summary>
    [SuppressMessage("ReSharper", "UnassignedReadonlyField")]
    public static readonly EmptyResult Value;

    /// <summary>Returns an empty string.</summary>
    /// <returns>An empty string.</returns>
    public override string ToString() => string.Empty;
}
