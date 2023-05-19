using System.Runtime.InteropServices;
using System.Security;

namespace DotGLFW;

[SuppressUnmanagedCodeSecurity]
public static class Glfw
{
    internal const string LIBRARY = "glfw3";

    [DllImport(LIBRARY, EntryPoint = "glfwInit", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Init();

    [DllImport(LIBRARY, EntryPoint = "glfwTerminate", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Terminate();

    [DllImport(LIBRARY, EntryPoint = "glfwInitHint", CallingConvention = CallingConvention.Cdecl)]
    public static extern void InitHint(int hint, int value);

    [DllImport(LIBRARY, EntryPoint = "glfwGetVersion", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetVersion(out int major, out int minor, out int rev);

    [DllImport(LIBRARY, EntryPoint = "glfwGetVersionString", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetVersionString();

    [DllImport(LIBRARY, EntryPoint = "glfwGetError", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetError(out IntPtr description);

    [DllImport(LIBRARY, EntryPoint = "glfwSetErrorCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwErrorCallback SetErrorCallback(GlfwErrorCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitors", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetMonitors(out int count);

    [DllImport(LIBRARY, EntryPoint = "glfwGetPrimaryMonitor", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetPrimaryMonitor();

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorPos", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetMonitorPos(IntPtr monitor, out int xpos, out int ypos);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorWorkarea", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetMonitorWorkarea(IntPtr monitor, out int xpos, out int ypos, out int width, out int height);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorPhysicalSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetMonitorPhysicalSize(IntPtr monitor, out int widthMM, out int heightMM);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorContentScale", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetMonitorContentScale(IntPtr monitor, out float xscale, out float yscale);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorName", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetMonitorName(IntPtr monitor);

    [DllImport(LIBRARY, EntryPoint = "glfwSetMonitorUserPointer", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetMonitorUserPointer(IntPtr monitor, IntPtr pointer);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorUserPointer", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetMonitorUserPointer(IntPtr monitor);

    [DllImport(LIBRARY, EntryPoint = "glfwSetMonitorCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern GlfwMonitorCallback SetMonitorCallback(GlfwMonitorCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwGetVideoModes", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetVideoModes(IntPtr monitor, out int count);

    [DllImport(LIBRARY, EntryPoint = "glfwGetVideoMode", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetVideoMode(IntPtr monitor);

    [DllImport(LIBRARY, EntryPoint = "glfwSetGamma", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetGamma(IntPtr monitor, float gamma);

    [DllImport(LIBRARY, EntryPoint = "glfwGetGammaRamp", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetGammaRamp(IntPtr monitor);

    [DllImport(LIBRARY, EntryPoint = "glfwSetGammaRamp", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetGammaRamp(IntPtr monitor, IntPtr ramp);

    [DllImport(LIBRARY, EntryPoint = "glfwDefaultWindowHints", CallingConvention = CallingConvention.Cdecl)]
    public static extern void DefaultWindowHints();

    [DllImport(LIBRARY, EntryPoint = "glfwWindowHint", CallingConvention = CallingConvention.Cdecl)]
    public static extern void WindowHint(int hint, int value);

    [DllImport(LIBRARY, EntryPoint = "glfwWindowHintString", CallingConvention = CallingConvention.Cdecl)]
    public static extern void WindowHintString(int hint, string value);

    [DllImport(LIBRARY, EntryPoint = "glfwCreateWindow", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr CreateWindow(int width, int height, string title, IntPtr monitor, IntPtr share);

    [DllImport(LIBRARY, EntryPoint = "glfwDestroyWindow", CallingConvention = CallingConvention.Cdecl)]
    public static extern void DestroyWindow(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwWindowShouldClose", CallingConvention = CallingConvention.Cdecl)]
    public static extern int WindowShouldClose(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowShouldClose", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowShouldClose(IntPtr window, int value);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowTitle", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowTitle(IntPtr window, string title);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowIcon", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowIcon(IntPtr window, int count, IntPtr images);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowPos", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetWindowPos(IntPtr window, out int xpos, out int ypos);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowPos", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowPos(IntPtr window, int xpos, int ypos);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetWindowSize(IntPtr window, out int width, out int height);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowSizeLimits", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowSizeLimits(IntPtr window, int minwidth, int minheight, int maxwidth, int maxheight);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowAspectRatio", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowAspectRatio(IntPtr window, int numer, int denom);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowSize(IntPtr window, int width, int height);

    [DllImport(LIBRARY, EntryPoint = "glfwGetFramebufferSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetFramebufferSize(IntPtr window, out int width, out int height);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowFrameSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetWindowFrameSize(IntPtr window, out int left, out int top, out int right, out int bottom);
}