namespace Raiqub.Generators.T4CodeWriter;

internal static class GeneratorInfo
{
#if IS_COMPILED
    internal const string Version = ThisAssembly.AssemblyFileVersion;
#else
    internal const string Version = "1.1.0.0";
#endif
}
