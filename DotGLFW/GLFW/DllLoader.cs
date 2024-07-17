using System.Runtime.InteropServices;

namespace DotGLFW;

internal static class DllLoader
{
  internal delegate IntPtr GetProcAddressDelegate(string name);

  internal static class Win32
  {
    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr LoadLibrary(string dllToLoad);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
  }

  internal static class Unix
  {
    [DllImport("libdl")]
    internal static extern IntPtr dlopen(string fileName, int flags);

    [DllImport("libdl")]
    internal static extern IntPtr dlsym(IntPtr handle, string name);

    [DllImport("libdl")]
    internal static extern IntPtr dlerror();
  }

  internal static GetProcAddressDelegate GetLoadFunctionPointerDelegate(string libraryName)
  {
    var assemblyDirectory = Path.GetDirectoryName(typeof(NativeGlfw).Assembly.Location);

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
      string assemblyPath = Path.Combine(
        assemblyDirectory,
        "runtimes",
        Environment.Is64BitProcess ? "win-x64" : "win-x86",
        "native",
        $"{libraryName}.dll"
      );

      // Discard the result, we only need it to be 
      // loaded into the process for later access
      var library = Win32.LoadLibrary(assemblyPath);
      return name => Win32.GetProcAddress(library, name);
    }

    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
      var runtimeSuffix = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "arm64" : "x64";
      string assemblyPath = Path.Combine(
        assemblyDirectory,
        "runtimes",
        $"osx-{runtimeSuffix}",
        "native",
        $"lib{libraryName}.dylib"
      );

      // Discard the result, we only need it to be 
      // loaded into the process for later access
      var library = Unix.dlopen(assemblyPath, 2);
      var errPtr = Unix.dlerror();
      if (errPtr != IntPtr.Zero)
      {
        var err = Marshal.PtrToStringAnsi(errPtr);
        throw new Exception($"Failed to load GLFW library: {err}");
      }
      return name => Unix.dlsym(library, name);
    }

    // Assume linux
    {
      var library = Unix.dlopen($"lib{libraryName}.so", 2);
      var errPtr = Unix.dlerror();
      if (errPtr != IntPtr.Zero)
      {
        var err = Marshal.PtrToStringAnsi(errPtr);
        throw new Exception($"Failed to load GLFW library: {err}");
      }
      return name => Unix.dlsym(library, name);
    }
  }
}
