using System.Reflection;
using System.Runtime.Loader;

namespace InvoiceTool.Infrastructure.PdfLibrary;

internal static class NativeLibraryLoader
{
    private static bool _loaded = false;

    public static void Load()
    {
        if (_loaded) return;

        string dllPath;

        if (OperatingSystem.IsWindows())
        {
            dllPath = Path.Combine(AppContext.BaseDirectory, "runtimes", "win-x64", "native", "libwkhtmltox.dll");
        }
        else if (OperatingSystem.IsLinux())
        {
            dllPath = Path.Combine(AppContext.BaseDirectory, "runtimes", "linux-x64", "native", "libwkhtmltox.so");
        }
        else if (OperatingSystem.IsMacOS())
        {
            dllPath = Path.Combine(AppContext.BaseDirectory, "runtimes", "osx-x64", "native", "libwkhtmltox.dylib");
        }
        else
        {
            throw new PlatformNotSupportedException("Unsupported OS for PDF conversion.");
        }

#if DEBUG
        dllPath = Path.Combine(AppContext.BaseDirectory, "NativeLibs", "libwkhtmltox.dll");
# endif

        if (!File.Exists(dllPath))
            throw new FileNotFoundException($"Native library not found: {dllPath}");

        var loader = new CustomAssemblyLoadContext();
        loader.LoadUnmanagedLibrary(dllPath);

        _loaded = true;
    }

    private class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string absolutePath) => LoadUnmanagedDll(absolutePath);
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllPath) => LoadUnmanagedDllFromPath(unmanagedDllPath);
        protected override Assembly Load(AssemblyName assemblyName) => null!;
    }
}

