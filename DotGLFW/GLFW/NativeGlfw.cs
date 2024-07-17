using System.Runtime.InteropServices;

namespace DotGLFW;

public unsafe partial class NativeGlfw
{
  private static T LoadFunctionOnlyOnPlatform<T>(OSPlatform platform, string name) where T : Delegate
  {
    if (RuntimeInformation.IsOSPlatform(platform))
      return LoadFunction<T>(name);

    return null;
  }

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  private delegate IntPtr d_glfwGetWin32Window(GLFWwindow* window);
  private static d_glfwGetWin32Window p_glfwGetWin32Window =
    LoadFunctionOnlyOnPlatform<d_glfwGetWin32Window>(OSPlatform.Windows, "glfwGetWin32Window");
  /// <summary>
  /// This function returns the Win32 window handle of the specified window.
  /// </summary>
  public static IntPtr glfwGetWin32Window(GLFWwindow* window) =>
    p_glfwGetWin32Window?.Invoke(window) ?? throw new PlatformNotSupportedException($"Cannot call {nameof(glfwGetWin32Window)} on this platform: {RuntimeInformation.OSDescription}");

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  private delegate IntPtr d_glfwGetCocoaWindow(GLFWwindow* window);
  private static d_glfwGetCocoaWindow p_glfwGetCocoaWindow =
    LoadFunctionOnlyOnPlatform<d_glfwGetCocoaWindow>(OSPlatform.OSX, "glfwGetCocoaWindow");
  /// <summary>
  /// This function returns the Cocoa NSWindow* of the specified window.
  /// </summary>
  public static IntPtr glfwGetCocoaWindow(GLFWwindow* window) =>
    p_glfwGetCocoaWindow?.Invoke(window) ?? throw new PlatformNotSupportedException($"Cannot call {nameof(glfwGetCocoaWindow)} on this platform: {RuntimeInformation.OSDescription}");

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  private delegate IntPtr d_glfwGetX11Window(GLFWwindow* window);
  private static d_glfwGetX11Window p_glfwGetX11Window =
    LoadFunctionOnlyOnPlatform<d_glfwGetX11Window>(OSPlatform.Linux, "glfwGetX11Window");
  /// <summary>
  /// This function returns the X11 window handle of the specified window.
  /// </summary>
  public static IntPtr glfwGetX11Window(GLFWwindow* window) =>
    p_glfwGetX11Window?.Invoke(window) ?? throw new PlatformNotSupportedException($"Cannot call {nameof(glfwGetX11Window)} on this platform: {RuntimeInformation.OSDescription}");

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  private delegate IntPtr d_glfwGetWaylandWindow(GLFWwindow* window);
  private static d_glfwGetWaylandWindow p_glfwGetWaylandWindow =
    LoadFunctionOnlyOnPlatform<d_glfwGetWaylandWindow>(OSPlatform.Linux, "glfwGetWaylandWindow");
  /// <summary>
  /// This function returns the Wayland wl_surface* of the specified window.
  /// </summary>
  public static IntPtr glfwGetWaylandWindow(GLFWwindow* window) =>
    p_glfwGetWaylandWindow?.Invoke(window) ?? throw new PlatformNotSupportedException($"Cannot call {nameof(glfwGetWaylandWindow)} on this platform: {RuntimeInformation.OSDescription}");
}
