using System.Runtime.InteropServices;
using System.Text;

namespace DotGLFW;

/// <summary>
///    The GLFW library.
/// </summary>
public static class Glfw
{
    /// <summary>
    /// Used in certain functions to indicate that the function should take the default value for that argument. <br/>
    /// </summary>
    public static readonly int DontCare = NativeGlfw.GLFW_DONT_CARE;

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

    // Holds the currently set managed error callback
    // A different native callback is used as a proxy to the managed callback.
    private static GlfwErrorCallback _errorCallback;
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
        GlfwErrorCallback oldCallback = _errorCallback;
        _errorCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetErrorCallback(null);
            return oldCallback;
        }

        NativeGlfw.SetErrorCallback((int errorCode, IntPtr description) =>
        {
            _errorCallback?.Invoke((ErrorCode)errorCode, Marshal.PtrToStringUTF8(description));
        });

        return oldCallback;
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
            monitors[i] = new Monitor(Marshal.ReadIntPtr(ptr, i * IntPtr.Size));
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
        return new Monitor(ptr);
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

    private static GlfwMonitorCallback _monitorCallback;
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
        GlfwMonitorCallback old = _monitorCallback;
        _monitorCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetMonitorCallback(null);
            return old;
        }

        NativeGlfw.SetMonitorCallback((IntPtr monitor, int @event) =>
        {
            Monitor m = Marshal.PtrToStructure<Monitor>(monitor);
            callback(m, (ConnectionState)@event);
        });

        return old;
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
    /// For gamma correct rendering with OpenGL or OpenGL ES, see the <see cref="Hint.SRGBCapable" /> hint.
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

    /// <summary>
    /// This function returns the current gamma ramp of the specified monitor. <br/>
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// Wayland: Gamma handling is a privileged protocol, this function will thus never be implemented and emits <see cref="ErrorCode.PlatformError" /> while returning NULL. <br/>
    /// </remarks>
    /// <returns>The current gamma ramp, or <c>null</c> if an error occurred.</returns>
    /// <param name="monitor">The monitor to query.</param>
    public static GammaRamp GetGammaRamp(Monitor monitor)
    {
        return Marshal.PtrToStructure<GammaRamp>(NativeGlfw.GetGammaRamp(monitor._handle));
    }

    /// <summary>
    /// This function sets the current gamma ramp for the specified monitor. The original gamma ramp for that monitor is saved by GLFW the first time this function is called and is restored by <see cref="Glfw.Terminate" />. <br/>
    /// The software controlled gamma ramp is applied in addition to the hardware gamma correction, which today is usually an approximation of sRGB gamma. This means that setting a perfectly linear ramp, or gamma 1.0, will produce the default (usually sRGB-like) behavior. <br/>
    /// For gamma correct rendering with OpenGL or OpenGL ES, see the <see cref="Hint.SRGBCapable" /> hint.
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// The size of the specified gamma ramp should match the size of the current ramp for that monitor. <br/>
    /// Windows: The gamma ramp size must be 256. <br/>
    /// Wayland: Gamma handling is a privileged protocol, this function will thus never be implemented and emits <see cref="ErrorCode.PlatformError" />.
    /// </remarks>
    /// <param name="monitor">The monitor whose gamma ramp to set.</param>
    /// <param name="ramp">The gamma ramp to use.</param>
    public static void SetGammaRamp(Monitor monitor, GammaRamp ramp)
    {
        // Get unmanaged pointer to struct
        IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf<GammaRamp>());
        Marshal.StructureToPtr(ramp, ptr, false);
        NativeGlfw.SetGammaRamp(monitor._handle, ptr);

        // ^ Copies the struct, so we can free the memory
        Marshal.FreeHGlobal(ptr);
    }

    /// <summary>
    /// This function resets all window hints to their default values. <br/>
    /// </summary>
    public static void DefaultWindowHints()
    {
        NativeGlfw.DefaultWindowHints();
    }

    /// <summary>
    /// This function sets hints for the next call to <see cref="Glfw.CreateWindow" />. The hints, once set, retain their values until changed by a call to this function or <see cref="Glfw.DefaultWindowHints" />, or until the library is terminated.
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// Hints that take string values must be set using <see cref="Glfw.WindowHintString" /> instead.
    /// </remarks>
    /// <param name="hint">The window hint to set.</param>
    /// <param name="value">The new value of the window hint.</param>
    public static void WindowHint<T>(HintType<T> hint, T value) where T : struct
    {
        int newValue = Convert.ToInt32(value);
        NativeGlfw.WindowHint(hint.Hint, newValue);
    }

    /// <summary>
    /// This function sets hints for the next call to <see cref="Glfw.CreateWindow" />. The hints, once set, retain their values until changed by a call to this function or <see cref="Glfw.DefaultWindowHints" />, or until the library is terminated.
    /// </summary>
    /// <remarks>
    /// This function must only be called from the main thread. <br/>
    /// </remarks>
    /// <param name="hint">The string window hint to set.</param>
    /// <param name="value">The new value of the window hint.</param>
    public static void WindowHintString(HintType<string> hint, string value)
    {
        NativeGlfw.WindowHintString(hint.Hint, value);
    }

    /// <summary>
    /// This function creates a window and its associated context. Most of the options controlling how the window and its context should be created are specified with window hints. Refer to <see href="https://www.glfw.org/docs/latest/group__window.html#ga3555a418df92ad53f917597fe2f64aeb" /> for the full documentation. 
    /// </summary>
    /// <returns>The handle of the created window, or null if an error occurred.</returns>
    /// <param name="width">The desired width, in screen coordinates, of the window. This must be greater than zero.</param>
    /// <param name="height">The desired height, in screen coordinates, of the window. This must be greater than zero.</param>
    /// <param name="title">The initial, UTF-8 encoded window title.</param>
    /// <param name="monitor">The monitor to use for full screen mode, or null for windowed mode.</param>
    /// <param name="share">The window whose context to share resources with, or null to not share resources.</param>
    public static Window CreateWindow(int width, int height, string title, Monitor monitor, Window share)
    {
        IntPtr ptr = NativeGlfw.CreateWindow(width, height, title, monitor?._handle ?? IntPtr.Zero, share?._handle ?? IntPtr.Zero);

        if (ptr == IntPtr.Zero)
        {
            return null;
        }

        return new Window(ptr);
    }

    /// <summary>
    /// This function destroys the specified window and its context. On calling this function, no further callbacks will be called for that window. <br/>
    /// If the context of the specified window is current on the main thread, it is detached before being destroyed.
    /// </summary>
    /// <param name="window">The window to destroy.</param>
    public static void DestroyWindow(Window window)
    {
        NativeGlfw.DestroyWindow(window._handle);
    }

    /// <summary>
    /// This function returns the value of the close flag of the specified window. <br/>
    /// </summary>
    /// <returns>True if the window should close, or false if the window should not close.</returns>
    /// <param name="window">The window to query.</param>
    public static bool WindowShouldClose(Window window)
    {
        return NativeGlfw.WindowShouldClose(window._handle) != 0;
    }

    /// <summary>
    /// This function sets the value of the close flag of the specified window. This can be used to override the user's attempt to close the window, or to signal that it should be closed. <br/>
    /// </summary>
    /// <param name="window">The window whose flag to change.</param>
    /// <param name="value">The new value.</param>
    public static void SetWindowShouldClose(Window window, bool value)
    {
        NativeGlfw.SetWindowShouldClose(window._handle, value ? 1 : 0);
    }

    /// <summary>
    /// This function sets the window title, encoded as UTF-8, of the specified window. <br/>
    /// </summary>
    /// <param name="window">The window whose title to change.</param>
    /// <param name="title">The UTF-8 encoded window title.</param>
    public static void SetWindowTitle(Window window, string title)
    {
        NativeGlfw.SetWindowTitle(window._handle, title);
    }

    /// <summary>
    /// This function sets the icon of the specified window. If passed an array of candidate images, those of or closest to the sizes desired by the system are selected. If no images are specified, the window reverts to its default icon. <br/>
    /// The pixels are 32-bit, little-endian, non-premultiplied RGBA, i.e. eight bits per channel with the red channel first. They are arranged canonically as packed sequential rows, starting from the top-left corner. <br/>
    /// The desired image sizes varies depending on platform and system settings. The selected images will be rescaled as needed. Good sizes include 16x16, 32x32 and 48x48. <br/>
    /// </summary>
    /// <param name="window">The window whose icon to set.</param>
    /// <param name="images">The images to create the icon from. If this is an empty array, the icon will be cleared to its default.</param>
    public static void SetWindowIcon(Window window, Image[] images)
    {
        if (images.Length == 0)
        {
            NativeGlfw.SetWindowIcon(window._handle, 0, IntPtr.Zero);
            return;
        }

        int imageSize = Marshal.SizeOf<Image>();
        IntPtr ptr = Marshal.AllocHGlobal(imageSize * images.Length);

        for (int i = 0; i < images.Length; i++)
        {
            Marshal.StructureToPtr(images[i], ptr + imageSize * i, false);
        }

        NativeGlfw.SetWindowIcon(window._handle, images.Length, ptr);
        Marshal.FreeHGlobal(ptr);
    }

    /// <summary>
    /// This function retrieves the position, in screen coordinates, of the upper-left corner of the content area of the specified window. <br/>
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="x">Where to store the x-coordinate of the upper-left corner of the content area</param>
    /// <param name="y">Where to store the y-coordinate of the upper-left corner of the content area</param>
    public static void GetWindowPos(Window window, out int x, out int y)
    {
        NativeGlfw.GetWindowPos(window._handle, out x, out y);
    }

    /// <summary>
    /// This function sets the position, in screen coordinates, of the upper-left corner of the content area of the specified windowed mode window. <br/>
    /// If the window is a full screen window, this function does nothing. <br/>
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the content area.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the content area.</param>
    public static void SetWindowPos(Window window, int x, int y)
    {
        NativeGlfw.SetWindowPos(window._handle, x, y);
    }

    /// <summary>
    /// This function retrieves the size, in screen coordinates, of the content area of the specified window. <br/>
    /// </summary>
    /// <param name="window">The window whose size to retrieve.</param>
    /// <param name="width">Where to store the width, in screen coordinates, of the content area.</param>
    /// <param name="height">Where to store the height, in screen coordinates, of the content area.</param>
    public static void GetWindowSize(Window window, out int width, out int height)
    {
        NativeGlfw.GetWindowSize(window._handle, out width, out height);
    }

    /// <summary>
    /// This function sets the size limits of the content area of the specified window. If the window is full screen, the size limits only take effect once it is made windowed. If the window is not resizable, this function does nothing. <br/>
    /// The size limits are applied immediately to a windowed mode window and may cause it to be resized. <br/>
    /// The maximum dimensions must be greater than or equal to the minimum dimensions and all must be greater than or equal to zero. <br/>
    /// </summary>
    /// <param name="window">The window to set limits for.</param>
    /// <param name="minWidth">The minimum width, in screen coordinates, of the content area, or <see cref="DontCare"/>.</param>
    /// <param name="minHeight">The minimum height, in screen coordinates, of the content area, or <see cref="DontCare"/>.</param>
    /// <param name="maxWidth">The maximum width, in screen coordinates, of the content area, or <see cref="DontCare"/>.</param>
    /// <param name="maxHeight">The maximum height, in screen coordinates, of the content area, or <see cref="DontCare"/>.</param>
    public static void SetWindowSizeLimits(Window window, int minWidth, int minHeight, int maxWidth, int maxHeight)
    {
        NativeGlfw.SetWindowSizeLimits(window._handle, minWidth, minHeight, maxWidth, maxHeight);
    }

    /// <summary>
    /// This function sets the required aspect ratio of the content area of the specified window. If the window is full screen, the aspect ratio only takes effect once it is made windowed. If the window is not resizable, this function does nothing. <br/>
    /// The aspect ratio is specified as a numerator and a denominator and both values must be greater than zero. For example, the common 16:9 aspect ratio is specified as 16 and 9, respectively. <br/>
    /// If the numerator and denominator is set to <see cref="DontCare"/> then the aspect ratio limit is disabled. <br/>
    /// The aspect ratio is applied immediately to a windowed mode window and may cause it to be resized. <br/>
    /// </summary>
    /// <param name="window">The window to set limits for.</param>
    /// <param name="numerator">The numerator of the desired aspect ratio, or <see cref="DontCare"/>.</param>
    /// <param name="denominator">The denominator of the desired aspect ratio, or <see cref="DontCare"/>.</param>
    public static void SetWindowAspectRatio(Window window, int numerator, int denominator)
    {
        NativeGlfw.SetWindowAspectRatio(window._handle, numerator, denominator);
    }

    /// <summary>
    /// This function sets the size, in screen coordinates, of the content area of the specified window. <br/>
    /// For full screen windows, this function updates the resolution of its desired video mode and switches to the video mode closest to it, without affecting the window's context. <br/>
    /// As the context is unaffected, the bit depths of the framebuffer remain unchanged. <br/>
    /// If you wish to update the refresh rate of the desired video mode in addition to its resolution, see <see cref="Glfw.SetWindowMonitor(Window, Monitor, int, int, int, int, int)"/>. <br/>
    /// The window manager may put limits on what sizes are allowed. GLFW cannot and should not override these limits. <br/>
    /// </summary>
    /// <param name="window">The window to resize.</param>
    /// <param name="width">The desired width, in screen coordinates, of the window content area.</param>
    /// <param name="height">The desired height, in screen coordinates, of the window content area.</param>
    public static void SetWindowSize(Window window, int width, int height)
    {
        NativeGlfw.SetWindowSize(window._handle, width, height);
    }

    /// <summary>
    /// This function retrieves the size, in pixels, of the framebuffer of the specified window. <br/>
    /// If you wish to retrieve the size of the window in screen coordinates, see <see cref="GetWindowSize(Window, out int, out int)"/>. <br/>
    /// </summary>
    /// <param name="window">The window whose framebuffer to query.</param>
    /// <param name="width">Where to store the width, in pixels, of the framebuffer.</param>
    /// <param name="height">Where to store the height, in pixels, of the framebuffer.</param>
    public static void GetFramebufferSize(Window window, out int width, out int height)
    {
        NativeGlfw.GetFramebufferSize(window._handle, out width, out height);
    }

    /// <summary>
    /// This function retrieves the size, in screen coordinates, of each edge of the frame of the specified window. This size includes the title bar, if the window has one. The size of the frame may vary depending on the window-related hints used to create it.
    /// </summary>
    /// <param name="window">The window whose frame size to query.</param>
    /// <param name="left">Where to store the size, in screen coordinates, of the left edge of the window frame.</param>
    /// <param name="top">Where to store the size, in screen coordinates, of the top edge of the window frame.</param>
    /// <param name="right">Where to store the size, in screen coordinates, of the right edge of the window frame.</param>
    /// <param name="bottom">Where to store the size, in screen coordinates, of the bottom edge of the window frame.</param>
    public static void GetWindowFrameSize(Window window, out int left, out int top, out int right, out int bottom)
    {
        NativeGlfw.GetWindowFrameSize(window._handle, out left, out top, out right, out bottom);
    }

    /// <summary>
    /// This function retrieves the content scale for the specified window. The content scale is the ratio between the current DPI and the platform's default DPI. This is especially important for text and any UI elements. If the pixel dimensions of your UI scaled by this look appropriate on your machine then it should appear at a reasonable size on other machines regardless of their DPI and scaling settings. This relies on the system DPI and scaling settings being somewhat correct.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="xScale">Where to store the x-axis content scale.</param>
    /// <param name="yScale">Where to store the y-axis content scale.</param>
    public static void GetWindowContentScale(Window window, out float xScale, out float yScale)
    {
        NativeGlfw.GetWindowContentScale(window._handle, out xScale, out yScale);
    }

    /// <summary>
    /// This function returns the opacity of the window, including any decorations.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The opacity value of the specified window.</returns>
    public static float GetWindowOpacity(Window window)
    {
        return NativeGlfw.GetWindowOpacity(window._handle);
    }

    /// <summary>
    /// This function sets the opacity of the whole window, including any decorations. <br/>
    /// The opacity (or alpha) value is a positive finite number between zero and one, where zero is fully transparent and one is fully opaque. <br/>
    /// </summary>
    /// <param name="window">The window to set the opacity for.</param>
    /// <param name="opacity">The desired opacity of the specified window.</param>
    public static void SetWindowOpacity(Window window, float opacity)
    {
        NativeGlfw.SetWindowOpacity(window._handle, opacity);
    }

    /// <summary>
    /// This function iconifies (minimizes) the specified window if it was previously restored. <br/>
    /// If the window is already iconified, this function does nothing. <br/>
    /// If the specified window is a full screen window, the original monitor resolution is restored until the window is restored. <br/>
    /// </summary>
    /// <param name="window">The window to iconify.</param>
    public static void IconifyWindow(Window window)
    {
        NativeGlfw.IconifyWindow(window._handle);
    }

    /// <summary>
    /// This function restores the specified window if it was previously iconified (minimized) or maximized. If the window is already restored, this function does nothing.
    /// If the specified window is a full screen window, the resolution chosen for the window is restored on the selected monitor. <br/>
    /// </summary>
    /// <param name="window">The window to restore.</param>
    public static void RestoreWindow(Window window)
    {
        NativeGlfw.RestoreWindow(window._handle);
    }

    /// <summary>
    /// This function maximizes the specified window if it was previously not maximized. If the window is already maximized, this function does nothing.
    /// If the specified window is a full screen window, this function does nothing. <br/>
    /// </summary>
    /// <param name="window">The window to maximize.</param>
    public static void MaximizeWindow(Window window)
    {
        NativeGlfw.MaximizeWindow(window._handle);
    }

    /// <summary>
    /// This function makes the specified window visible if it was previously hidden. If the window is already visible or is in full screen mode, this function does nothing.
    /// </summary>
    /// <param name="window">The window to make visible.</param>
    public static void ShowWindow(Window window)
    {
        NativeGlfw.ShowWindow(window._handle);
    }

    /// <summary>
    /// This function hides the specified window if it was previously visible. If the window is already hidden or is in full screen mode, this function does nothing.
    /// </summary>
    /// <param name="window">The window to hide.</param>
    public static void HideWindow(Window window)
    {
        NativeGlfw.HideWindow(window._handle);
    }

    /// <summary>
    /// This function brings the specified window to front and sets input focus. The window should already be visible and not iconified.
    /// Do not use this function to steal focus from other applications unless you are certain that is what the user wants. Focus stealing can be extremely disruptive.
    /// For a less disruptive way of getting the user's attention, see attention requests.
    /// </summary>
    public static void FocusWindow(Window window)
    {
        NativeGlfw.FocusWindow(window._handle);
    }

    /// <summary>
    /// This function requests user attention to the specified window. On platforms where this is not supported, attention is requested to the application as a whole.
    /// Once the user has given attention, usually by focusing the window or application, the system will end the request automatically.
    /// </summary>
    /// <param name="window">The window to request attention to.</param>
    public static void RequestWindowAttention(Window window)
    {
        NativeGlfw.RequestWindowAttention(window._handle);
    }

    /// <summary>
    /// This function returns the handle of the monitor that the specified window is in full screen on.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The monitor, or null if the window is in windowed mode or an error occurred.</returns>
    public static Monitor GetWindowMonitor(Window window)
    {
        IntPtr ptr = NativeGlfw.GetWindowMonitor(window._handle);
        return Marshal.PtrToStructure<Monitor>(ptr);
    }

    /// <summary>
    /// This function sets the monitor that the window uses for full screen mode or, if the monitor is <see cref="Monitor.NULL" />, makes it windowed mode.
    /// </summary>
    /// <param name="window">The window whose monitor, size or video mode to set.</param>
    /// <param name="monitor">The desired monitor, or <see cref="Monitor.NULL" /> to set windowed mode.</param>
    /// <param name="x">The desired x-coordinate of the upper-left corner of the client area.</param>
    /// <param name="y">The desired y-coordinate of the upper-left corner of the client area.</param>
    /// <param name="width">The desired with, in screen coordinates, of the client area or video mode.</param>
    /// <param name="height">The desired height, in screen coordinates, of the client area or video mode.</param>
    /// <param name="refreshRate">The desired refresh rate, in Hz, of the video mode, or <see cref="DontCare" />.</param>
    public static void SetWindowMonitor(Window window, Monitor monitor, int x, int y, int width, int height, int refreshRate)
    {
        NativeGlfw.SetWindowMonitor(window._handle, monitor._handle, x, y, width, height, refreshRate);
    }

    /// <summary>
    /// This function returns the value of an attribute of the specified window or its OpenGL or OpenGL ES context.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="attrib">The window attribute whose value to return.</param>
    /// <returns>The value of the attribute, or zero if an error occurred.</returns>
    public static bool GetWindowAttrib(Window window, WindowAttribType<bool> attrib)
    {
        int value = NativeGlfw.GetWindowAttrib(window._handle, attrib.Attribute);
        return value != 0;
    }

    /// <summary>
    /// This function returns the value of an attribute of the specified window or its OpenGL or OpenGL ES context.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="attrib">The window attribute whose value to return.</param>
    /// <returns>The value of the attribute, or zero if an error occurred.</returns>
    public static int GetWindowAttrib(Window window, WindowAttribType<int> attrib)
    {
        int value = NativeGlfw.GetWindowAttrib(window._handle, attrib.Attribute);
        return value;
    }

    /// <summary>
    /// This function returns the value of an attribute of the specified window or its OpenGL or OpenGL ES context.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="attrib">The window attribute whose value to return.</param>
    /// <returns>The value of the attribute, or zero if an error occurred.</returns>
    public static T GetWindowAttrib<T>(Window window, WindowAttribType<T> attrib) where T : Enum
    {
        int value = NativeGlfw.GetWindowAttrib(window._handle, attrib.Attribute);
        return (T)Enum.ToObject(typeof(T), value);
    }

    /// <summary>
    /// This function sets the value of an attribute of the specified window. Can only be used to set the following boolean attributes.
    /// <see cref="WindowAttrib.Decorated" />, <see cref="WindowAttrib.Resizable" />, <see cref="WindowAttrib.Floating" />, <see cref="WindowAttrib.AutoIconify" /> and <see cref="WindowAttrib.FocusOnShow" />.
    /// </summary>
    /// <param name="window">The window to set the attribute for.</param>
    /// <param name="attrib">The attribute to set.</param>
    /// <param name="value">The new value of the attribute.</param>
    public static void SetWindowAttrib(Window window, WindowAttribType<bool> attrib, bool value)
    {
        NativeGlfw.SetWindowAttrib(window._handle, attrib.Attribute, value ? 1 : 0);
    }

    /// <summary>
    /// This function sets the user-defined pointer of the specified window. The current value is retained until the window is destroyed. The initial value is <see cref="IntPtr.Zero" />.
    /// </summary>
    /// <param name="window">The window whose pointer to set.</param>
    /// <param name="pointer">The new value.</param>
    public static void SetWindowUserPointer(Window window, IntPtr pointer)
    {
        NativeGlfw.SetWindowUserPointer(window._handle, pointer);
    }

    /// <summary>
    /// This function returns the current value of the user-defined pointer of the specified window. The initial value is <see cref="IntPtr.Zero" />.
    /// </summary>
    /// <param name="window">The window whose pointer to return.</param>
    /// <returns>The user-defined pointer of the window.</returns>
    public static IntPtr GetWindowUserPointer(Window window)
    {
        return NativeGlfw.GetWindowUserPointer(window._handle);
    }

    // Holds the currently bound managed windowPosCallback
    private static GlfwWindowPosCallback _windowPosCallback;

    /// <summary>
    /// Sets the position callback for the specified window
    /// </summary>
    /// <param name="window">The window to set the callback for</param>
    /// <param name="callback">The callback to set</param>
    /// <returns>The previously set callback</returns>
    public static GlfwWindowPosCallback SetWindowPosCallback(Window window, GlfwWindowPosCallback callback)
    {
        GlfwWindowPosCallback old = _windowPosCallback;
        _windowPosCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetWindowPosCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetWindowPosCallback(window._handle, (IntPtr ptr, int x, int y) =>
        {
            Window w = new Window(ptr);
            _windowPosCallback(w, x, y);
        });

        return old;
    }

    // Holds the currently bound managed windowSizeCallback
    private static GlfwWindowSizeCallback _windowSizeCallback;

    /// <summary>
    /// Sets the size callback for the specified window
    /// </summary>
    /// <param name="window">The window to set the callback for</param>
    /// <param name="callback">The callback to set</param>
    /// <returns>The previously set callback</returns>
    public static GlfwWindowSizeCallback SetWindowSizeCallback(Window window, GlfwWindowSizeCallback callback)
    {
        GlfwWindowSizeCallback old = _windowSizeCallback;
        _windowSizeCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetWindowSizeCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetWindowSizeCallback(window._handle, (IntPtr ptr, int width, int height) =>
        {
            Window w = new Window(ptr);
            _windowSizeCallback(w, width, height);
        });

        return old;
    }

    // Holds the currently bound managed windowCloseCallback
    private static GlfwWindowCloseCallback _windowCloseCallback;

    /// <summary>
    /// Sets the close callback for the specified window
    /// </summary>
    /// <param name="window">The window to set the callback for</param>
    /// <param name="callback">The callback to set</param>
    /// <returns>The previously set callback</returns>
    public static GlfwWindowCloseCallback SetWindowCloseCallback(Window window, GlfwWindowCloseCallback callback)
    {
        GlfwWindowCloseCallback old = _windowCloseCallback;
        _windowCloseCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetWindowCloseCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetWindowCloseCallback(window._handle, (IntPtr ptr) =>
        {
            Window w = new Window(ptr);
            _windowCloseCallback(w);
        });

        return old;
    }

    // Holds the currently bound managed windowRefreshCallback
    private static GlfwWindowRefreshCallback _windowRefreshCallback;

    /// <summary>
    /// Sets the refresh callback for the specified window
    /// </summary>
    /// <param name="window">The window to set the callback for</param>
    /// <param name="callback">The callback to set</param>
    /// <returns>The previously set callback</returns>
    public static GlfwWindowRefreshCallback SetWindowRefreshCallback(Window window, GlfwWindowRefreshCallback callback)
    {
        GlfwWindowRefreshCallback old = _windowRefreshCallback;
        _windowRefreshCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetWindowRefreshCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetWindowRefreshCallback(window._handle, (IntPtr ptr) =>
        {
            Window w = new Window(ptr);
            _windowRefreshCallback(w);
        });

        return old;
    }

    // Holds the currently bound managed windowFocusCallback
    private static GlfwWindowFocusCallback _windowFocusCallback;

    /// <summary>
    /// Sets the focus callback for the specified window
    /// </summary>
    /// <param name="window">The window to set the callback for</param>
    /// <param name="callback">The callback to set</param>
    /// <returns>The previously set callback</returns>
    public static GlfwWindowFocusCallback SetWindowFocusCallback(Window window, GlfwWindowFocusCallback callback)
    {
        GlfwWindowFocusCallback old = _windowFocusCallback;
        _windowFocusCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetWindowFocusCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetWindowFocusCallback(window._handle, (IntPtr ptr, int focused) =>
        {
            Window w = new Window(ptr);
            _windowFocusCallback(w, focused != 0);
        });

        return old;
    }

    // Holds the currently bound managed windowIconifyCallback
    private static GlfwWindowIconifyCallback _windowIconifyCallback;

    /// <summary>
    /// Sets the iconify callback for the specified window
    /// </summary>
    /// <param name="window">The window to set the callback for</param>
    /// <param name="callback">The callback to set</param>
    /// <returns>The previously set callback</returns>
    public static GlfwWindowIconifyCallback SetWindowIconifyCallback(Window window, GlfwWindowIconifyCallback callback)
    {
        GlfwWindowIconifyCallback old = _windowIconifyCallback;
        _windowIconifyCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetWindowIconifyCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetWindowIconifyCallback(window._handle, (IntPtr ptr, int iconified) =>
        {
            Window w = new Window(ptr);
            _windowIconifyCallback(w, iconified != 0);
        });

        return old;
    }

    // Holds the currently bound managed windowMaximizeCallback
    private static GlfwWindowMaximizeCallback _windowMaximizeCallback;

    /// <summary>
    /// Sets the maximize callback for the specified window
    /// </summary>
    /// <param name="window">The window to set the callback for</param>
    /// <param name="callback">The callback to set</param>
    /// <returns>The previously set callback</returns>
    public static GlfwWindowMaximizeCallback SetWindowMaximizeCallback(Window window, GlfwWindowMaximizeCallback callback)
    {
        GlfwWindowMaximizeCallback old = _windowMaximizeCallback;
        _windowMaximizeCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetWindowMaximizeCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetWindowMaximizeCallback(window._handle, (IntPtr ptr, int maximized) =>
        {
            Window w = new Window(ptr);
            _windowMaximizeCallback(w, maximized != 0);
        });

        return old;
    }

    // Holds the currently bound managed framebufferSizeCallback
    private static GlfwFramebufferSizeCallback _framebufferSizeCallback;

    /// <summary>
    /// Sets the framebuffer size callback for the specified window
    /// </summary>
    /// <param name="window">The window to set the callback for</param>
    /// <param name="callback">The callback to set</param>
    /// <returns>The previously set callback</returns>
    public static GlfwFramebufferSizeCallback SetFramebufferSizeCallback(Window window, GlfwFramebufferSizeCallback callback)
    {
        GlfwFramebufferSizeCallback old = _framebufferSizeCallback;
        _framebufferSizeCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetFramebufferSizeCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetFramebufferSizeCallback(window._handle, (IntPtr ptr, int width, int height) =>
        {
            Window w = new Window(ptr);
            _framebufferSizeCallback(w, width, height);
        });

        return old;
    }

    // Holds the currently bound managed windowContentScaleCallback
    private static GlfwWindowContentScaleCallback _windowContentScaleCallback;

    /// <summary>
    /// Sets the content scale callback for the specified window
    /// </summary>
    /// <param name="window">The window to set the callback for</param>
    /// <param name="callback">The callback to set</param>
    /// <returns>The previously set callback</returns>
    public static GlfwWindowContentScaleCallback SetWindowContentScaleCallback(Window window, GlfwWindowContentScaleCallback callback)
    {
        GlfwWindowContentScaleCallback old = _windowContentScaleCallback;
        _windowContentScaleCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetWindowContentScaleCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetWindowContentScaleCallback(window._handle, (IntPtr ptr, float xScale, float yScale) =>
        {
            Window w = new Window(ptr);
            _windowContentScaleCallback(w, xScale, yScale);
        });

        return old;
    }

    /// <summary>
    /// This function processes only those events that are already in the event queue and then returns immediately. Processing events will cause the window and input callbacks associated with those events to be called.
    /// </summary>
    public static void PollEvents()
    {
        NativeGlfw.PollEvents();
    }

    /// <summary>
    /// This function puts the calling thread to sleep until at least one event is available in the event queue. Once one or more events are available, it behaves exactly like <see cref="PollEvents" />, i.e. the events in the queue are processed and the function then returns immediately. Processing events will cause the window and input callbacks associated with those events to be called.
    /// </summary>
    public static void WaitEvents()
    {
        NativeGlfw.WaitEvents();
    }

    /// <summary>
    /// This function puts the calling thread to sleep until at least one event is available in the event queue, or until the specified timeout is reached. If one or more events are available, it behaves exactly like <see cref="PollEvents" />, i.e. the events in the queue are processed and the function then returns immediately. Processing events will cause the window and input callbacks associated with those events to be called.
    /// </summary>
    public static void WaitEventsTimeout(double timeout)
    {
        NativeGlfw.WaitEventsTimeout(timeout);
    }

    /// <summary>
    /// This function posts an empty event from the current thread to the event queue, causing <see cref="WaitEvents" /> or <see cref="WaitEventsTimeout" /> to return.
    /// </summary>
    public static void PostEmptyEvent()
    {
        NativeGlfw.PostEmptyEvent();
    }

    /// <summary>
    /// This function returns the value of an input option for the specified window.
    /// </summary>
    /// <param name="window">The window to query</param>
    /// <param name="mode">The mode to get the current value of</param>
    /// <returns>The current value of the specified input mode</returns>
    public static T GetInputMode<T>(Window window, InputModeType<T> mode) where T : Enum
    {
        int modeAsInt = Convert.ToInt32(mode);
        int currentMode = NativeGlfw.GetInputMode(window._handle, modeAsInt);
        return (T)Enum.ToObject(typeof(T), currentMode);
    }

    /// <summary>
    /// This function returns the value of an input option for the specified window.
    /// </summary>
    /// <param name="window">The window to query</param>
    /// <param name="mode">The mode to get the current value of</param>
    /// <returns>The current value of the specified input mode</returns>
    public static bool GetInputMode(Window window, InputModeType<bool> mode)
    {
        int modeAsInt = Convert.ToInt32(mode);
        return NativeGlfw.GetInputMode(window._handle, modeAsInt) != 0;
    }

    /// <summary>
    /// This function sets an input mode option for the specified window.
    /// </summary>
    /// <param name="window">The window whose input mode to set</param>
    /// <param name="mode">The input mode to set</param>
    /// <param name="value">The new value of the specified input mode</param>
    public static void SetInputMode(Window window, InputModeType<bool> mode, bool value)
    {
        int modeAsInt = Convert.ToInt32(mode);
        NativeGlfw.SetInputMode(window._handle, modeAsInt, value ? 1 : 0);
    }

    /// <summary>
    /// This function sets an input mode option for the specified window.
    /// </summary>
    /// <param name="window">The window whose input mode to set</param>
    /// <param name="mode">The input mode to set</param>
    /// <param name="value">The new value of the specified input mode</param>
    public static void SetInputMode<T>(Window window, InputModeType<T> mode, T value) where T : Enum
    {
        int modeAsInt = Convert.ToInt32(mode);
        int valueAsInt = Convert.ToInt32(value);
        NativeGlfw.SetInputMode(window._handle, modeAsInt, valueAsInt);
    }

    /// <summary>
    /// This function returns whether raw mouse motion is supported on the current system. This status does not change after GLFW has been initialized so you only need to check this once. If you attempt to enable raw motion on a system that does not support it, GLFW_PLATFORM_ERROR will be emitted.
    /// </summary>
    /// <returns>Whether raw mouse motion is supported on the current system</returns>
    public static bool RawMouseMotionSupported()
    {
        return NativeGlfw.RawMouseMotionSupported() != 0;
    }

    /// <summary>
    /// This function returns the name of the specified printable key, encoded as UTF-8. This is typically the character that key would produce without any modifier keys, intended for displaying key bindings to the user. For dead keys, it is typically the diacritic it would add to a character.
    /// </summary>
    /// <param name="key">The key to query, or <see cref="Keys.Unknown" /></param>
    /// <param name="scancode">The scancode of the key to query</param>
    /// <returns>The UTF-8 encoded, layout-specific name of the key</returns>
    public static string GetKeyName(Keys key, int scancode)
    {
        IntPtr ptr = NativeGlfw.GetKeyName((int)key, scancode);
        return Marshal.PtrToStringUTF8(ptr);
    }

    /// <summary>
    /// This function returns the platform-specific scancode of the specified key.
    /// </summary>
    /// <param name="key">The key to query</param>
    /// <returns>The platform-specific scancode for the key, or <see cref="Keys.Unknown" /> if an error occurred</returns>
    public static int GetKeyScancode(Keys key)
    {
        return NativeGlfw.GetKeyScancode((int)key);
    }

    /// <summary>
    /// This function returns the last state reported for the specified key to the specified window. The returned state is one of <see cref="InputState.Press" /> or <see cref="InputState.Release" />. The action <see cref="InputState.Repeat" /> is only reported to the key callback.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="key">The desired key</param>
    /// <returns>One of <see cref="InputState.Press" /> or <see cref="InputState.Release" /></returns>
    public static InputState GetKey(Window window, Keys key)
    {
        return (InputState)NativeGlfw.GetKey(window._handle, (int)key);
    }

    /// <summary>
    /// This function returns the last state reported for the specified mouse button to the specified window
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="button">The desired mouse button</param>
    /// <returns>One of <see cref="InputState.Press" /> or <see cref="InputState.Release" /></returns>
    public static InputState GetMouseButton(Window window, MouseButton button)
    {
        return (InputState)NativeGlfw.GetMouseButton(window._handle, (int)button);
    }

    /// <summary>
    /// This function returns the position of the cursor, in screen coordinates, relative to the upper-left corner of the content area of the specified window.
    /// </summary>
    /// <remarks>
    /// If the cursor is disabled (with <see cref="CursorMode.Disabled" />) then the cursor position is unbounded and limited only by the minimum and maximum values of a <see cref="double" />.
    /// The coordinate can be converted to their integer equivalents with the <see cref="Math.Floor(double)" /> function. Casting directly to an integer type works for positive coordinates, but fails for negative ones.
    /// </remarks>
    /// <param name="window">The desired window</param>
    /// <param name="x">Where to store the cursor x-coordinate, relative to the left edge of the content area</param>
    /// <param name="y">Where to store the cursor y-coordinate, relative to the top edge of the content area</param>
    public static void GetCursorPos(Window window, out double x, out double y)
    {
        NativeGlfw.GetCursorPos(window._handle, out x, out y);
    }

    /// <summary>
    /// This function sets the position, in screen coordinates, of the cursor relative to the upper-left corner of the content area of the specified window. The window must have input focus. If the window does not have input focus when this function is called, it fails silently.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="x">The desired x-coordinate, relative to the left edge of the content area</param>
    /// <param name="y">The desired y-coordinate, relative to the top edge of the content area</param>
    public static void SetCursorPos(Window window, double x, double y)
    {
        NativeGlfw.SetCursorPos(window._handle, x, y);
    }

    /// <summary>
    /// Creates a new custom cursor image that can be set for a window with <see cref="SetCursor" />. The cursor can be destroyed with <see cref="DestroyCursor" />. Any remaining cursors are destroyed by <see cref="Terminate" />.
    /// </summary>
    /// <param name="image">The desired cursor image</param>
    /// <param name="xhot">The desired x-coordinate, in pixels, of the cursor hotspot</param>
    /// <param name="yhot">The desired y-coordinate, in pixels, of the cursor hotspot</param>
    public static Cursor CreateCursor(Image image, int xhot, int yhot)
    {
        IntPtr imagePtr = Marshal.AllocHGlobal(Marshal.SizeOf<Image>());
        Marshal.StructureToPtr(image, imagePtr, false);

        IntPtr handle = NativeGlfw.CreateCursor(imagePtr, xhot, yhot);

        Marshal.FreeHGlobal(imagePtr);
        return new Cursor(handle);
    }

    /// <summary>
    /// Returns a cursor with a standard shape, that can be set for a window with <see cref="SetCursor" />.
    /// </summary>
    /// <param name="shape">One of the standard shapes</param>
    /// <returns>A new cursor ready to use or <see cref="Cursor.NULL" /> if an error occurred</returns>
    public static Cursor CreateStandardCursor(CursorShape shape)
    {
        IntPtr handle = NativeGlfw.CreateStandardCursor((int)shape);
        return new Cursor(handle);
    }

    /// <summary>
    /// Destroys a cursor previously created with <see cref="CreateCursor" />. Any remaining cursors will be destroyed by <see cref="Terminate" />.
    /// </summary>
    /// <param name="cursor">The cursor object to destroy</param>
    public static void DestroyCursor(Cursor cursor)
    {
        NativeGlfw.DestroyCursor(cursor._handle);
    }

    /// <summary>
    /// This function sets the cursor image to be used when the cursor is over the content area of the specified window. The set cursor will only be visible when the cursor mode of the window is <see cref="CursorMode.Normal" />.
    /// On some platforms, the set cursor may not be visible unless the window also has input focus.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="cursor">The cursor to set, or <see cref="Cursor.NULL" /> to switch back to the default arrow cursor</param>
    public static void SetCursor(Window window, Cursor cursor)
    {
        NativeGlfw.SetCursor(window._handle, cursor._handle);
    }

    // Holds the currently bound managed glfwKeyCallback
    private static GlfwKeyCallback _keyCallback;

    /// <summary>
    /// This function sets the key callback of the specified window, which is called when a key is pressed, repeated or released.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="callback">The new key callback, or <c>null</c> to remove the currently set callback</param>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set</returns>
    public static GlfwKeyCallback SetKeyCallback(Window window, GlfwKeyCallback callback)
    {
        GlfwKeyCallback old = _keyCallback;
        _keyCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetKeyCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetKeyCallback(window._handle, (IntPtr ptr, int key, int scancode, int action, int modifiers) =>
        {
            Window w = new Window(ptr);
            callback(w, (Keys)key, scancode, (InputState)action, (ModifierKeys)modifiers);
        });

        return old;
    }

    // Holds the currently bound managed glfwCharCallback
    private static GlfwCharCallback _charCallback;

    /// <summary>
    /// This function sets the character callback of the specified window, which is called when a Unicode character is input.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="callback">The new character callback, or <c>null</c> to remove the currently set callback</param>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set</returns>
    public static GlfwCharCallback SetCharCallback(Window window, GlfwCharCallback callback)
    {
        GlfwCharCallback old = _charCallback;
        _charCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetCharCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetCharCallback(window._handle, (IntPtr ptr, uint codepoint) =>
        {
            Window w = new Window(ptr);
            callback(w, codepoint);
        });

        return old;
    }

    // Holds the currently bound managed glfwCharModsCallback
    private static GlfwCharModsCallback _charModsCallback;

    /// <summary>
    /// This function sets the Unicode character with modifiers callback of the specified window, which is called when a Unicode character is input regardless of what modifier keys are used.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="callback">The new character with modifiers callback, or <c>null</c> to remove the currently set callback</param>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set</returns>
    public static GlfwCharModsCallback SetCharModsCallback(Window window, GlfwCharModsCallback callback)
    {
        GlfwCharModsCallback old = _charModsCallback;
        _charModsCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetCharModsCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetCharModsCallback(window._handle, (IntPtr ptr, uint codepoint, int mods) =>
        {
            Window w = new Window(ptr);
            callback(w, codepoint, (ModifierKeys)mods);
        });

        return old;
    }

    // Holds the currently bound managed glfwMouseButtonCallback
    private static GlfwMouseButtonCallback _mouseButtonCallback;

    /// <summary>
    /// This function sets the mouse button callback of the specified window, which is called when a mouse button is pressed or released.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="callback">The new mouse button callback, or <c>null</c> to remove the currently set callback</param>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set</returns>
    public static GlfwMouseButtonCallback SetMouseButtonCallback(Window window, GlfwMouseButtonCallback callback)
    {
        GlfwMouseButtonCallback old = _mouseButtonCallback;
        _mouseButtonCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetMouseButtonCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetMouseButtonCallback(window._handle, (IntPtr ptr, int button, int action, int mods) =>
        {
            Window w = new Window(ptr);
            callback(w, (MouseButton)button, (InputState)action, (ModifierKeys)mods);
        });

        return old;
    }

    // Holds the currently bound managed glfwCursorPosCallback
    private static GlfwCursorPosCallback _cursorPosCallback;

    /// <summary>
    /// This function sets the cursor position callback of the specified window, which is called when the cursor is moved.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="callback">The new cursor position callback, or <c>null</c> to remove the currently set callback</param>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set</returns>
    public static GlfwCursorPosCallback SetCursorPosCallback(Window window, GlfwCursorPosCallback callback)
    {
        GlfwCursorPosCallback old = _cursorPosCallback;
        _cursorPosCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetCursorPosCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetCursorPosCallback(window._handle, (IntPtr ptr, double x, double y) =>
        {
            Window w = new Window(ptr);
            callback(w, x, y);
        });

        return old;
    }

    // Holds the currently bound managed glfwCursorEnterCallback
    private static GlfwCursorEnterCallback _cursorEnterCallback;

    /// <summary>
    /// This function sets the cursor enter/exit callback of the specified window, which is called when the cursor enters or leaves the client area of the window.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="callback">The new cursor enter/exit callback, or <c>null</c> to remove the currently set callback</param>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set</returns>
    public static GlfwCursorEnterCallback SetCursorEnterCallback(Window window, GlfwCursorEnterCallback callback)
    {
        GlfwCursorEnterCallback old = _cursorEnterCallback;
        _cursorEnterCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetCursorEnterCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetCursorEnterCallback(window._handle, (IntPtr ptr, int entered) =>
        {
            Window w = new Window(ptr);
            callback(w, entered != 0);
        });

        return old;
    }

    // Holds the currently bound managed glfwScrollCallback
    private static GlfwScrollCallback _scrollCallback;

    /// <summary>
    /// This function sets the scroll callback of the specified window, which is called when a scrolling device is used, such as a mouse wheel or scrolling area of a touchpad.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="callback">The new scroll callback, or <c>null</c> to remove the currently set callback</param>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set</returns>
    public static GlfwScrollCallback SetScrollCallback(Window window, GlfwScrollCallback callback)
    {
        GlfwScrollCallback old = _scrollCallback;
        _scrollCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetScrollCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetScrollCallback(window._handle, (IntPtr ptr, double x, double y) =>
        {
            Window w = new Window(ptr);
            callback(w, x, y);
        });

        return old;
    }

    // Holds the currently bound managed glfwDropCallback
    private static GlfwDropCallback _dropCallback;

    /// <summary>
    /// This function sets the file drop callback of the specified window, which is called when one or more dragged files are dropped on the window.
    /// </summary>
    /// <param name="window">The desired window</param>
    /// <param name="callback">The new file drop callback, or <c>null</c> to remove the currently set callback</param>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set</returns>
    public static GlfwDropCallback SetDropCallback(Window window, GlfwDropCallback callback)
    {
        GlfwDropCallback old = _dropCallback;
        _dropCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetDropCallback(window._handle, null);
            return old;
        }

        NativeGlfw.SetDropCallback(window._handle, (IntPtr ptr, int count, IntPtr names) =>
        {
            Window w = new Window(ptr);

            string[] fileNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                IntPtr name = Marshal.ReadIntPtr(names, i * IntPtr.Size);
                fileNames[i] = Marshal.PtrToStringUTF8(name);
            }

            callback(w, fileNames);
        });

        return old;
    }

    /// <summary>
    /// This function returns whether the specified joystick is present.
    /// </summary>
    /// <param name="joystick">The joystick to query</param>
    /// <returns><c>true</c> if the joystick is present, or <c>false</c> otherwise</returns>
    public static bool JoystickPresent(Joystick joystick)
    {
        return NativeGlfw.JoystickPresent((int)joystick) != 0;
    }

    /// <summary>
    /// This function returns the values of all axes of the specified joystick. Each element in the array is a value between -1.0 and 1.0.
    /// </summary>
    /// <param name="joystick">The joystick to query</param>
    /// <returns>An array of axis values, or an empty array if the joystick is not present</returns>
    public static float[] GetJoystickAxes(Joystick joystick)
    {
        int count;
        IntPtr axes = NativeGlfw.GetJoystickAxes((int)joystick, out count);

        if (count == 0 || axes == IntPtr.Zero)
            return new float[0];

        float[] result = new float[count];

        for (int i = 0; i < count; i++)
        {
            float value = Marshal.PtrToStructure<float>(axes + i * sizeof(float));
            result[i] = value;
        }

        return result;
    }

    /// <summary>
    /// This function returns the state of all buttons of the specified joystick. Each element in the array is either <see cref="InputState.Press"/> or <see cref="InputState.Release"/>.
    /// </summary>
    /// <param name="joystick">The joystick to query</param>
    /// <returns>An array of button states, or an empty array if the joystick is not present</returns>
    public static InputState[] GetJoystickButtons(Joystick joystick)
    {
        int count;
        IntPtr buttons = NativeGlfw.GetJoystickButtons((int)joystick, out count);

        if (count == 0 || buttons == IntPtr.Zero)
            return new InputState[0];

        InputState[] result = new InputState[count];

        for (int i = 0; i < count; i++)
        {
            byte value = Marshal.ReadByte(buttons, i);
            result[i] = (InputState)value;
        }

        return result;
    }

    /// <summary>
    /// This function returns the state of all hats of the specified joystick. Each element in the array is a value describing the position of the hat.
    /// </summary>
    /// <param name="joystick">The joystick to query</param>
    /// <returns>An array of hat states, or an empty array if the joystick is not present</returns>
    public static JoystickHat[] GetJoystickHats(Joystick joystick)
    {
        int count;
        IntPtr hats = NativeGlfw.GetJoystickHats((int)joystick, out count);

        if (count == 0 || hats == IntPtr.Zero)
            return new JoystickHat[0];

        JoystickHat[] result = new JoystickHat[count];

        for (int i = 0; i < count; i++)
        {
            byte value = Marshal.ReadByte(hats, i);
            result[i] = (JoystickHat)value;
        }

        return result;
    }

    /// <summary>
    /// This function returns the name, encoded as UTF-8, of the specified joystick.
    /// </summary>
    /// <param name="joystick">The joystick to query</param>
    /// <returns>The UTF-8 encoded name of the joystick, or <c>null</c> if the joystick is not present</returns>
    public static string GetJoystickName(Joystick joystick)
    {
        return Marshal.PtrToStringUTF8(NativeGlfw.GetJoystickName((int)joystick));
    }

    /// <summary>
    /// This function returns the SDL compatible GUID, as a UTF-8 encoded hexadecimal string, of the specified joystick.
    /// </summary>
    /// <param name="joystick">The joystick to query</param>
    /// <returns>The UTF-8 encoded GUID of the joystick, or <c>null</c> if the joystick is not present</returns>
    public static string GetJoystickGUID(Joystick joystick)
    {
        return Marshal.PtrToStringUTF8(NativeGlfw.GetJoystickGUID((int)joystick));
    }

    /// <summary>
    /// This function sets the user pointer of the specified joystick.
    /// </summary>
    /// <param name="joystick">The joystick whose pointer to set</param>
    /// <param name="pointer">The new value</param>
    public static void SetJoystickUserPointer(Joystick joystick, IntPtr pointer)
    {
        NativeGlfw.SetJoystickUserPointer((int)joystick, pointer);
    }

    /// <summary>
    /// This function returns the user pointer of the specified joystick.
    /// </summary>
    /// <param name="joystick">The joystick whose pointer to return</param>
    /// <returns>The previously set pointer, or <c>null</c> if no pointer was set</returns>
    public static IntPtr GetJoystickUserPointer(Joystick joystick)
    {
        return NativeGlfw.GetJoystickUserPointer((int)joystick);
    }

    /// <summary>
    /// This function returns whether the specified joystick has a gamepad mapping.
    /// </summary>
    /// <param name="joystick">The joystick to query</param>
    /// <returns><c>true</c> if a joystick is both present and has a gamepad mapping, or <c>false</c> otherwise</returns>
    public static bool JoystickIsGamepad(Joystick joystick)
    {
        return NativeGlfw.JoystickIsGamepad((int)joystick) != 0;
    }

    // Holds the currently bound managed joystickCallback
    private static GlfwJoystickCallback _joystickCallback;

    /// <summary>
    /// Sets the joystick configuration callback, or removes the currently set callback.
    /// </summary>
    /// <param name="callback">The new callback, or <c>null</c> to remove the currently set callback</param>
    /// <returns>The previously set callback, or <c>null</c> if no callback was set</returns>
    public static GlfwJoystickCallback SetJoystickCallback(GlfwJoystickCallback callback)
    {
        GlfwJoystickCallback old = _joystickCallback;
        _joystickCallback = callback;

        if (callback is null)
        {
            NativeGlfw.SetJoystickCallback(null);
            return old;
        }

        NativeGlfw.SetJoystickCallback((int jid, int @event) =>
        {
            callback((Joystick)jid, (ConnectionState)@event);
        });

        return old;
    }

    /// <summary>
    /// This function parses the specified ASCII encoded string and updates the internal list with any gamepad mappings it finds. This string may contain either a single gamepad mapping or many mappings separated by newlines. The parser supports the full format of the gamecontrollerdb.txt source file including empty lines and comments.
    /// </summary>
    /// <param name="mapping">The string containing the gamepad mappings</param>
    /// <returns><c>true</c> if successful, or <c>false</c> if an error occurred</returns>
    public static bool UpdateGamepadMappings(string mapping)
    {
        return NativeGlfw.UpdateGamepadMappings(mapping) != 0;
    }

    /// <summary>
    /// This function returns the human-readable name of the gamepad from the gamepad mapping assigned to the specified joystick.
    /// </summary>
    /// <param name="joystick">The joystick to query</param>
    /// <returns>The UTF-8 encoded name of the gamepad, or <c>null</c> if the joystick is not present or no mapping is available</returns>
    public static string GetGamepadName(Joystick joystick)
    {
        return Marshal.PtrToStringUTF8(NativeGlfw.GetGamepadName((int)joystick));
    }

    /// <summary>
    /// This function retrieves the state of the specified joystick remapped to an Xbox-like gamepad
    /// </summary>
    /// <param name="joystick">The joystick to query</param>
    /// <param name="state">The gamepad input state of the joystick</param>
    /// <returns><c>true</c> if successful, or <c>false</c> if the joystick is not present</returns>
    public static bool GetGamepadState(Joystick joystick, out GamepadState state)
    {
        int result = NativeGlfw.GetGamepadState((int)joystick, out IntPtr nativeState);

        if (result == 0)
        {
            state = default;
            return false;
        }

        state = Marshal.PtrToStructure<GamepadState>(nativeState);

        return true;
    }

    /// <summary>
    /// This function sets the system clipboard to the specified, UTF-8 encoded string.
    /// </summary>
    /// <param name="window">The window that will own the clipboard contents (deprecated, any valid window or <see cref="Window.NULL" />)</param>
    /// <param name="string">A UTF-8 encoded string</param>
    public static void SetClipboardString(Window window, string @string)
    {
        IntPtr nativeString = Marshal.StringToHGlobalUni(@string);
        NativeGlfw.SetClipboardString(window._handle, nativeString);
    }

    /// <summary>
    /// This function returns the contents of the system clipboard, if it contains or is convertible to a UTF-8 encoded string.
    /// </summary>
    /// <param name="window">The window that will request the clipboard contents (deprecated, any valid window or <see cref="Window.NULL" />)</param>
    /// <returns>The contents of the clipboard as a UTF-8 encoded string, or <c>null</c> if an error occurred</returns>
    public static string GetClipboardString(Window window)
    {
        IntPtr nativeString = NativeGlfw.GetClipboardString(window._handle);
        return Marshal.PtrToStringUTF8(nativeString);
    }

    /// <summary>
    /// This function returns the current GLFW time, in seconds. Unless the time has been set using glfwSetTime it measures time elapsed since GLFW was initialized.
    /// </summary>
    /// <returns>The current time, in seconds, or zero if an error occurred</returns>
    public static double GetTime()
    {
        return NativeGlfw.GetTime();
    }

    /// <summary>
    /// This function sets the current GLFW time, in seconds. The value must be a positive finite number less than or equal to 18446744073.0, which is approximately 584.5 years.
    /// </summary>
    /// <param name="time">The new value, in seconds</param>
    public static void SetTime(double time)
    {
        NativeGlfw.SetTime(time);
    }

    /// <summary>
    /// This function returns the current value of the raw timer, measured in 1 / frequency seconds.
    /// </summary>
    /// <returns>The value of the timer, or zero if an error occurred</returns>
    public static ulong GetTimerValue()
    {
        return NativeGlfw.GetTimerValue();
    }

    /// <summary>
    /// This function returns the frequency, in Hz, of the raw timer.
    /// </summary>
    /// <returns>The frequency of the timer, in Hz, or zero if an error occurred</returns>
    public static ulong GetTimerFrequency()
    {
        return NativeGlfw.GetTimerFrequency();
    }

    /// <summary>
    /// This function makes the OpenGL or OpenGL ES context of the specified window current on the calling thread. A context must only be made current on a single thread at a time and each thread can have only a single current context at a time.
    /// </summary>
    /// <param name="window">The window whose context to make current, or <see cref="Window.NULL" /> to detach the current context</param>
    public static void MakeContextCurrent(Window window)
    {
        NativeGlfw.MakeContextCurrent(window._handle);
    }

    /// <summary>
    /// This function returns the window whose context is current on the calling thread.
    /// </summary>
    /// <returns>The window whose context is current, or <see cref="Window.NULL" /> if no window's context is current</returns>
    public static Window GetCurrentContext()
    {
        IntPtr handle = NativeGlfw.GetCurrentContext();
        return new Window(handle);
    }

    /// <summary>
    /// This function swaps the front and back buffers of the specified window when rendering with OpenGL or OpenGL ES. If the swap interval is greater than zero, the GPU driver waits the specified number of screen updates before swapping the buffers.
    /// </summary>
    /// <param name="window">The window whose buffers to swap</param>
    public static void SwapBuffers(Window window)
    {
        NativeGlfw.SwapBuffers(window._handle);
    }

    /// <summary>
    /// This function sets the swap interval for the current OpenGL or OpenGL ES context, i.e. the number of screen updates to wait from the time <see cref="SwapBuffers" /> was called before swapping the buffers and returning. This is sometimes called vertical synchronization, vertical retrace synchronization or just vsync.
    /// </summary>
    /// <param name="interval">The minimum number of screen updates to wait for until the buffers are swapped by <see cref="SwapBuffers" /></param>
    public static void SwapInterval(int interval)
    {
        NativeGlfw.SwapInterval(interval);
    }

    /// <summary>
    /// This function returns whether the specified API extension is supported by the current OpenGL or OpenGL ES context. It searches both for client API extension and context creation API extensions.
    /// </summary>
    /// <param name="extension">The ASCII encoded name of the extension</param>
    public static bool ExtensionSupported(string extension)
    {
        return NativeGlfw.ExtensionSupported(extension) != 0;
    }

    /// <summary>
    /// This function returns the address of the specified function for the current OpenGL or OpenGL ES context. Before calling this function, you must have made the context of the specified window current.
    /// </summary>
    /// <param name="procname">The ASCII encoded name of the function</param>
    public static IntPtr GetProcAddress(string procname)
    {
        return NativeGlfw.GetProcAddress(procname);
    }

    /// <summary>
    /// This function returns whether the Vulkan loader and any minimally functional ICD have been found.
    /// </summary>
    public static bool VulkanSupported()
    {
        return NativeGlfw.VulkanSupported() != 0;
    }

    /// <summary>
    /// This function returns an array of names of Vulkan instance extensions required by GLFW for creating Vulkan surfaces for GLFW windows. If successful, the list will always contain VK_KHR_surface, so if you don't require any additional extensions you can pass this list directly to the VkInstanceCreateInfo struct.
    /// </summary>
    /// <returns>An array of ASCII encoded extension names, or an empty array if an error occurred</returns>
    public static string[] GetRequiredInstanceExtensions()
    {
        uint count;
        IntPtr extensions = NativeGlfw.GetRequiredInstanceExtensions(out count);

        if (count == 0 || extensions == IntPtr.Zero)
            return new string[0];

        string[] result = new string[count];

        for (int i = 0; i < count; i++)
        {
            IntPtr extension = Marshal.ReadIntPtr(extensions, i * IntPtr.Size);
            result[i] = Marshal.PtrToStringUTF8(extension);
        }

        return result;
    }

    /// <summary>
    /// This function returns the address of the specified Vulkan core or extension function for the specified instance. If instance is set to <see cref="IntPtr.Zero" />, the returned function address is for the global uninstanced function.
    /// </summary>
    /// <param name="vkInstance">The Vulkan instance to query, or <see cref="IntPtr.Zero" /> for the global uninstanced function</param>
    /// <param name="procname">The ASCII encoded name of the function</param>
    /// <returns>The address of the function, or <see cref="IntPtr.Zero" /> if an error occurred</returns>
    public static IntPtr GetInstanceProcAddress(IntPtr vkInstance, string procname)
    {
        return NativeGlfw.GetInstanceProcAddress(vkInstance, procname);
    }

    /// <summary>
    /// This function returns whether the specified queue family of the specified physical device supports presentation to the platform GLFW was built for.
    /// </summary>
    /// <param name="vkInstance">The instance that the physical device belongs to</param>
    /// <param name="vkPhysicalDevice">The physical device that the queue family belongs to</param>
    /// <param name="queuefamily">The index of the queue family to query</param>
    public static bool GetPhysicalDevicePresentationSupport(IntPtr vkInstance, IntPtr vkPhysicalDevice, uint queuefamily)
    {
        return NativeGlfw.GetPhysicalDevicePresentationSupport(vkInstance, vkPhysicalDevice, queuefamily) != 0;
    }

    /// <summary>
    /// This function creates a Vulkan surface for the specified window.
    /// </summary>
    /// <param name="vkInstance">The Vulkan instance to create the surface in</param>
    /// <param name="window">The window to create the surface for</param>
    /// <param name="allocator">The allocator to use, or <see cref="IntPtr.Zero" /> to use the default allocator</param>
    /// <param name="surface">Where to store the handle of the surface. This is set to <see cref="IntPtr.Zero" /> if an error occurred</param>
    /// <returns>Vulkan specific result code</returns>
    public static int CreateWindowSurface(IntPtr vkInstance, Window window, IntPtr allocator, out IntPtr surface)
    {
        return NativeGlfw.CreateWindowSurface(vkInstance, window._handle, allocator, out surface);
    }
}