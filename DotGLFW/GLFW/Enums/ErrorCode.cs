namespace DotGLFW;

/// <summary>
///     Error codes are set when an error occurs. Read more here <see href="https://www.glfw.org/docs/latest/intro_guide.html#error_handling" />. <br/>
///     Also see <see href="https://www.glfw.org/docs/latest/group__errors.html" />.
/// </summary>
public enum ErrorCode
{
    /// <summary>
    ///  No error has occurred.
    /// </summary>
    /// <remarks>
    ///  Yay!
    /// </remarks>
    NoError = NativeGlfw.GLFW_NO_ERROR,

    /// <summary>
    /// This occurs if a GLFW function was called that must not be called unless the library is initialized with <see cref="Glfw.Init" />.
    /// </summary>
    /// <remarks>
    /// Application programmer error. Initialize GLFW before calling any function that requires initialization.
    /// </remarks>
    NotInitialized = NativeGlfw.GLFW_NOT_INITIALIZED,

    /// <summary>
    /// This occurs if a GLFW function was called that needs and operates on the current OpenGL or OpenGL ES context but no context is current on the calling thread. One such function is <see cref="Glfw.SwapInterval" />.
    /// </summary>
    /// <remarks>
    /// Application programmer error. Ensure a context is current before calling functions that require a current context.
    /// </remarks>
    NoCurrentContext = NativeGlfw.GLFW_NO_CURRENT_CONTEXT,

    /// <summary>
    /// One of the arguments to the function was an invalid enum value, for example requesting GLFW_RED_BITS with glfwGetWindowAttrib. //TODO: Fix refs
    /// </summary>
    /// <remarks>
    /// Application programmer error. Fix the offending call.
    /// </remarks>
    InvalidEnum = NativeGlfw.GLFW_INVALID_ENUM,

    /// <summary>
    /// One of the arguments to the function was an invalid value, for example requesting a non-existent OpenGL or OpenGL ES version like 2.7. <br/>
    /// Requesting a valid but unavailable OpenGL or OpenGL ES version will instead result in a <see cref="ErrorCode.VersionUnavailable" /> error.
    /// </summary>
    /// <remarks>
    /// Application programmer error. Fix the offending call.
    /// </remarks>
    InvalidValue = NativeGlfw.GLFW_INVALID_VALUE,

    /// <summary>
    /// A memory allocation failed.
    /// </summary>
    /// <remarks>
    /// A bug in GLFW or the underlying operating system. Report the bug to the official GLFW issue tracker at <see href="https://github.com/glfw/glfw/issues" />.
    /// </remarks>
    OutOfMemory = NativeGlfw.GLFW_OUT_OF_MEMORY,

    /// <summary>
    /// GLFW could not find support for the requested API on the system.
    /// </summary>
    /// <remarks>
    /// The installed graphics driver does not support the requested API, or does not support it via the chosen context creation backend. Below are a few examples. <br/>
    /// Some pre-installed Windows graphics drivers do not support OpenGL. AMD only supports OpenGL ES via EGL, while Nvidia and Intel only support it via a WGL or GLX extension. <br/>
    /// OS X does not provide OpenGL ES at all. The Mesa EGL, OpenGL and OpenGL ES libraries do not interface with the Nvidia binary driver. Older graphics drivers do not support Vulkan.
    /// </remarks>
    ApiUnavailable = NativeGlfw.GLFW_API_UNAVAILABLE,

    /// <summary>
    /// The requested OpenGL or OpenGL ES version (including any requested context or framebuffer hints) is not available on this machine. <br/>
    /// </summary>
    /// <remarks>
    /// The machine does not support your requirements. If your application is sufficiently flexible, downgrade your requirements and try again. Otherwise, inform the user that their machine does not match your requirements. <br/>
    /// Future invalid OpenGL and OpenGL ES versions, for example OpenGL 4.8 if 5.0 comes out before the 4.x series gets that far, also fail with this error and not <see cref="ErrorCode.InvalidValue" />, because GLFW cannot know what future versions will exist.
    /// </remarks>
    VersionUnavailable = NativeGlfw.GLFW_VERSION_UNAVAILABLE,

    /// <summary>
    /// A platform-specific error occurred that does not match any of the more specific categories.
    /// </summary>
    /// <remarks>
    /// A bug or configuration error in GLFW, the underlying operating system or its drivers, or a lack of required resources. Report the issue to our issue tracker at <see href="https://github.com/glfw/glfw/issues" />.
    /// </remarks>
    PlatformError = NativeGlfw.GLFW_PLATFORM_ERROR,

    /// <summary>
    /// If emitted during window creation, the requested pixel format is not supported. <br/>
    /// If emitted when querying the clipboard, the contents of the clipboard could not be converted to the requested format. <br/>
    /// </summary>
    /// <remarks>
    /// If emitted during window creation, one or more hard constraints did not match any of the available pixel formats. If your application is sufficiently flexible, downgrade your requirements and try again. Otherwise, inform the user that their machine does not match your requirements. <br/>
    /// If emitted when querying the clipboard, ignore the error or report it to the user, as appropriate.
    /// </remarks>
    FormatUnavailable = NativeGlfw.GLFW_FORMAT_UNAVAILABLE,

    /// <summary>
    /// A window that does not have an OpenGL or OpenGL ES context was passed to a function that requires it to have one.
    /// </summary>
    /// <remarks>
    /// Application programmer error. Fix the offending call.
    /// </remarks>
    NoWindowContext = NativeGlfw.GLFW_NO_WINDOW_CONTEXT,
}