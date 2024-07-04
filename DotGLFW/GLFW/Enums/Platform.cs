namespace DotGLFW;

/// <summary>
///   Specifies the platform type (rendering backend) to request when using OpenGL ES and EGL via ANGLE.
/// </summary>
public enum Platform
{
    /// <summary>
    /// No platform specified.
    /// </summary>
    AnyPlatform = NativeGlfw.GLFW_ANY_PLATFORM,

    /// <summary>
    /// Windows platform.
    /// </summary>
    Win32 = NativeGlfw.GLFW_PLATFORM_WIN32,

    /// <summary>
    /// macOS platform.
    /// </summary>
    Cocoa = NativeGlfw.GLFW_PLATFORM_COCOA,

    /// <summary>
    /// Linux platform.
    /// </summary>
    Wayland = NativeGlfw.GLFW_PLATFORM_WAYLAND,

    /// <summary>
    /// Linux platform.
    /// </summary>
    X11 = NativeGlfw.GLFW_PLATFORM_X11,

    /// <summary>
    /// Null platform.
    /// </summary>
    Null = NativeGlfw.GLFW_PLATFORM_NULL
}