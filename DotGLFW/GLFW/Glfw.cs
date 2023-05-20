using System.Runtime.InteropServices;
using System.Text;

namespace DotGLFW;

/// <summary>
///    The GLFW library.
/// </summary>
public static class Glfw
{
    /// <summary>
    /// This function initializes the GLFW library. Before most GLFW functions can be used, GLFW must be initialized, and before an application terminates GLFW should be terminated in order to free any resources allocated during or after initialization. <br/>
    /// If this function fails, it calls <see cref="Glfw.Terminate" /> before returning. If it succeeds, you should call <see cref="Glfw.Terminate" /> before the application exits. <br/>
    /// Additional calls to this function after successful initialization but before termination will return <c>true</c> immediately. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread.
    /// </remarks>
    /// <returns><c>true</c> if successful, or <c>false</c> if an error occurred.</returns>
    public static bool Init() => NativeGlfw.Init() != 0;

    /// <summary>
    /// This function destroys all remaining windows and cursors, restores any modified gamma ramps and frees any other allocated resources. Once this function is called, you must again call glfwInit successfully before you will be able to use most GLFW functions. <br/>
    /// If GLFW has been successfully initialized, this function should be called before the application exits. If initialization fails, there is no need to call this function, as it is called by glfwInit before it returns failure. <br/>
    /// This function has no effect if GLFW is not initialized. <br/>
    /// </summary>
    /// <remarks>
    /// This function must not be called from a callback. <br/>
    /// This function must only be called from the main thread.
    /// This function may be called before <see cref="Glfw.Init" />.
    /// </remarks>
    public static void Terminate() => NativeGlfw.Terminate();

    /// <summary>
    /// This function sets hints for the next initialization of GLFW. <br/>
    /// The values you set hints to are never reset by GLFW, but they only take effect during initialization. Once GLFW has been initialized, any values you set will be ignored until the library is terminated and initialized again. <br/>
    /// Some hints are platform specific. These may be set on any platform but they will only affect their specific platform. Other platforms will ignore them. Setting these hints requires no platform specific headers or functions. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread.
    /// This function may be called before <see cref="Glfw.Init" />.
    /// </remarks>
    public static void InitHint(InitHint hint, bool value) => NativeGlfw.InitHint((int)hint, value ? 1 : 0);

    /// <summary>
    /// This function retrieves the major, minor and revision numbers of the GLFW library. It is intended for when you are using GLFW as a shared library and want to ensure that you are using the minimum required version. <br/>
    /// </summary>
    /// <remarks>
    /// This function may be called before <see cref="Glfw.Init" />.
    /// This function may be called from any thread.
    /// </remarks>
    /// <param name="major">The major version of the library</param>
    /// <param name="minor">The minor version of the library</param>
    /// <param name="revision">The revision number of the library</param>
    public static void GetVersion(out int major, out int minor, out int revision)
    {
        NativeGlfw.GetVersion(out major, out minor, out revision);
    }

    /// <summary>
    /// This function returns the compile-time generated version string of the GLFW library binary. It describes the version, platform, compiler and any platform-specific compile-time options. It should not be confused with the OpenGL or OpenGL ES version string, queried with glGetString. <br/>
    /// </summary>
    /// <remarks>
    /// This function may be called before <see cref="Glfw.Init" />.
    /// This function may be called from any thread.
    /// </remarks>
    /// <returns>The ASCII encoded (UTF8) GLFW version string.</returns>
    public static string GetVersionString() => Marshal.PtrToStringUTF8(NativeGlfw.GetVersionString());

    /// <summary>
    /// This function returns and clears the error code of the last error that occurred on the calling thread, and a UTF8 encoded human-readable description of it. If no error has occurred since the last call, it returns <see cref="ErrorCode.NoError"/> and the description string is set to null.
    /// </summary>
    /// <remarks>
    /// This function may be called before <see cref="Glfw.Init" />. <br/>
    /// This function may be called from any thread.
    /// </remarks>
    /// <returns>The last error code.</returns>
    /// <param name="description">The UTF8 encoded human-readable string of the error.</param>
    public static ErrorCode GetError(out string description)
    {
        int error = NativeGlfw.GetError(out IntPtr descriptionPtr);
        description = Marshal.PtrToStringUTF8(descriptionPtr);
        return (ErrorCode)error;
    }

    /// <summary>
    /// This function sets the error callback, which is called with an error code and a human-readable description each time a GLFW error occurs. <br/>
    /// The error code is set before the callback is called. Calling <see cref="Glfw.GetError(out string)" /> from the error callback will return the same value as the error code argument. <br/>
    /// The error callback is called on the thread where the error occurred. If you are using GLFW from multiple threads, your error callback needs to be written accordingly. <br/>
    /// Once set, the error callback remains set even after the library has been terminated. <br/>
    /// </summary>
    /// <remarks>
    /// This function may be called before <see cref="Glfw.Init" />.
    /// This function must only be called from the main thread.
    /// </remarks>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set.</returns>
    /// <param name="callback">The new callback, or <c>null</c> to remove the currently set callback.</param>
    public static GlfwErrorCallback SetErrorCallback(GlfwErrorCallback callback)
    {
        return NativeGlfw.SetErrorCallback(callback);
    }

    /// <summary>
    /// This function returns an array of <see cref="Monitor" /> for all currently connected monitors. The primary monitor is always first in the returned array. If no monitors were found, this function returns an empty array.
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread.
    /// </remarks>
    /// <returns>An array of monitor objects, or an empty array if no monitors were found or if an error occurred.</returns>
    public static Monitor[] GetMonitors()
    {
        IntPtr ptr = NativeGlfw.GetMonitors(out int count);

        if (ptr.Equals(IntPtr.Zero))
            return Array.Empty<Monitor>();

        Monitor[] monitors = new Monitor[count];
        for (int i = 0; i < count; i++)
        {
            monitors[i] = Marshal.PtrToStructure<Monitor>(ptr + (i * IntPtr.Size));
        }
        return monitors;
    }

    /// <summary>
    /// This function returns the primary monitor. This is usually the monitor where elements like the task bar or global menu bar are located.
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread.
    /// </remarks>
    /// <returns>The primary monitor, or <c>null</c> if no monitors were found or if an error occurred.</returns>
    public static Monitor GetPrimaryMonitor()
    {
        IntPtr ptr = NativeGlfw.GetPrimaryMonitor();
        return Marshal.PtrToStructure<Monitor>(ptr);
    }

    /// <summary>
    /// This function returns the position, in screen coordinates, of the upper-left corner of the specified monitor. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread.
    /// </remarks>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="x">The x-position of the monitor.</param>
    /// <param name="y">The y-position of the monitor.</param>
    public static void GetMonitorPos(Monitor monitor, out int x, out int y)
    {
        NativeGlfw.GetMonitorPos(monitor._handle, out x, out y);
    }

    /// <summary>
    /// This function returns the position, in screen coordinates, of the upper-left corner of the work area of the specified monitor along with the work area size in screen coordinates. The work area is defined as the area of the monitor not occluded by the operating system task bar where present. If no task bar exists then the work area is the monitor resolution in screen coordinates. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread.
    /// </remarks>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="x">The x-position of the monitor work area.</param>
    /// <param name="y">The y-position of the monitor work area.</param>
    /// <param name="width">The width of the monitor work area.</param>
    /// <param name="height">The height of the monitor work area.</param>
    public static void GetMonitorWorkarea(Monitor monitor, out int x, out int y, out int width, out int height)
    {
        NativeGlfw.GetMonitorWorkarea(monitor._handle, out x, out y, out width, out height);
    }

    /// <summary>
    /// This function returns the size, in millimetres, of the display area of the specified monitor. <br/>
    /// Some systems do not provide accurate monitor size information, either because the monitor EDID data is incorrect or because the driver does not report it accurately. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// Windows: On Windows 8 and earlier the physical size is calculated from the current resolution and system DPI instead of querying the monitor EDID data. <br/>
    /// </remarks>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="width">The width, in millimetres, of the monitor's display area.</param>
    /// <param name="height">The height, in millimetres, of the monitor's display area.</param>
    public static void GetMonitorPhysicalSize(Monitor monitor, out int width, out int height)
    {
        NativeGlfw.GetMonitorPhysicalSize(monitor._handle, out width, out height);
    }

    /// <summary>
    /// This function retrieves the content scale for the specified monitor. The content scale is the ratio between the current DPI and the platform's default DPI. This is especially important for text and any UI elements. If the pixel dimensions of your UI scaled by this look appropriate on your machine then it should appear at a reasonable size on other machines regardless of their DPI and scaling settings. This relies on the system DPI and scaling settings being somewhat correct. <br/>
    /// The content scale may depend on both the monitor resolution and pixel density and on user settings. It may be very different from the raw DPI calculated from the physical size and current resolution. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// </remarks>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="xscale">The x-axis content scale.</param>
    /// <param name="yscale">The y-axis content scale.</param>
    public static void GetMonitorContentScale(Monitor monitor, out float xscale, out float yscale)
    {
        NativeGlfw.GetMonitorContentScale(monitor._handle, out xscale, out yscale);
    }

    /// <summary>
    /// This function returns a human-readable name, encoded as UTF-8, of the specified monitor. The name typically reflects the make and model of the monitor and is not guaranteed to be unique among the connected monitors. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// </remarks>
    /// <returns>The UTF-8 encoded name of the monitor, or <c>null</c> if an error occurred.</returns>
    /// <param name="monitor">The monitor to query.</param>
    public static string GetMonitorName(Monitor monitor)
    {
        IntPtr ptr = NativeGlfw.GetMonitorName(monitor._handle);
        return Marshal.PtrToStringUTF8(ptr);
    }

    /// <summary>
    /// This function sets the user pointer of the specified monitor. The current value is retained until the monitor is disconnected. The initial value is <c>null</c>. <br/>
    /// </summary>
    /// <remarks>
    /// This function can be called from any thread. Access is not synchronized. <br/>
    /// </remarks>
    /// <param name="monitor">The monitor whose pointer to set.</param>
    /// <param name="pointer">The new value.</param>
    public static void SetMonitorUserPointer(Monitor monitor, IntPtr pointer)
    {
        NativeGlfw.SetMonitorUserPointer(monitor._handle, pointer);
    }

    /// <summary>
    /// This function returns the user pointer of the specified monitor. The initial value is <c>null</c>. <br/>
    /// This function may be called from the monitor callback, even for a monitor that is being disconnected. <br/>
    /// </summary>
    /// <remarks>
    /// This function can be called from any thread. Access is not synchronized. <br/>
    /// </remarks>
    /// <returns>The user pointer of the monitor, or <c>null</c> if no user pointer is set.</returns>
    /// <param name="monitor">The monitor whose pointer to return.</param>
    public static IntPtr GetMonitorUserPointer(Monitor monitor)
    {
        return NativeGlfw.GetMonitorUserPointer(monitor._handle);
    }

    /// <summary>
    /// This function sets the monitor configuration callback, or removes the currently set callback. This is called when a monitor is connected to or disconnected from the system. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// </remarks>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set or the library had not been initialized.</returns>
    /// <param name="callback">The new callback, or <c>null</c> to remove the currently set callback.</param>
    public static GlfwMonitorCallback SetMonitorCallback(GlfwMonitorCallback callback)
    {
        return NativeGlfw.SetMonitorCallback(callback);
    }

    /// <summary>
    /// This function returns an array of all video modes supported by the specified monitor. The returned array is sorted in ascending order, first by color bit depth (the sum of all channel depths), then by resolution area (the product of width and height), then resolution width and finally by refresh rate. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// </remarks>
    /// <returns>An array of video modes, or and empty array if an error occurred.</returns>
    /// <param name="monitor">The monitor to query.</param>
    public static VideoMode[] GetVideoModes(Monitor monitor)
    {
        IntPtr ptr = NativeGlfw.GetVideoModes(monitor._handle, out int count);

        if (ptr == IntPtr.Zero)
            return Array.Empty<VideoMode>();

        VideoMode[] modes = new VideoMode[count];
        for (int i = 0; i < count; i++)
        {
            modes[i] = Marshal.PtrToStructure<VideoMode>(ptr + i * Marshal.SizeOf<VideoMode>());
        }
        return modes;
    }

    /// <summary>
    /// This function returns the current mode of the specified monitor. If you have created a full screen window for that monitor, the return value will depend on whether that window is iconified. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// </remarks>
    /// <returns>The current mode of the monitor, or <c>null</c> if an error occurred.</returns>
    /// <param name="monitor">The monitor to query.</param>
    public static VideoMode GetVideoMode(Monitor monitor)
    {
        return Marshal.PtrToStructure<VideoMode>(NativeGlfw.GetVideoMode(monitor._handle));
    }

    /// <summary>
    /// This function generates an appropriately sized gamma ramp from the specified exponent and then calls <see cref="Glfw.SetGammaRamp" /> with it. The value must be a finite number greater than zero. <br/>
    /// The software controlled gamma ramp is applied in addition to the hardware gamma correction, which today is usually an approximation of sRGB gamma. This means that setting a perfectly linear ramp, or gamma 1.0, will produce the default (usually sRGB-like) behavior. <br/>
    /// For gamma correct rendering with OpenGL or OpenGL ES, see the GLFW_SRGB_CAPABLE hint. TODO: Fix this ref
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// </remarks>
    /// <param name="monitor">The monitor whose gamma ramp to set.</param>
    /// <param name="gamma">The desired exponent.</param>
    public static void SetGamma(Monitor monitor, float gamma)
    {
        NativeGlfw.SetGamma(monitor._handle, gamma);
    }
}