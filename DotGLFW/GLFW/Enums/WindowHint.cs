namespace DotGLFW;

/// <summary>
///     Window hints are set before <see cref="Glfw.CreateWindow" /> and affect how the window and its context should be created.
/// </summary>
public static class WindowHint
{
    public static readonly WindowHintType<bool> Resizable = new WindowHintType<bool>(NativeGlfw.GLFW_RESIZABLE);
    public static readonly WindowHintType<bool> Visible = new WindowHintType<bool>(NativeGlfw.GLFW_VISIBLE);
    public static readonly WindowHintType<bool> Decorated = new WindowHintType<bool>(NativeGlfw.GLFW_DECORATED);
    public static readonly WindowHintType<bool> Focused = new WindowHintType<bool>(NativeGlfw.GLFW_FOCUSED);
    public static readonly WindowHintType<bool> AutoIconify = new WindowHintType<bool>(NativeGlfw.GLFW_AUTO_ICONIFY);
    public static readonly WindowHintType<bool> Floating = new WindowHintType<bool>(NativeGlfw.GLFW_FLOATING);
    public static readonly WindowHintType<bool> Maximized = new WindowHintType<bool>(NativeGlfw.GLFW_MAXIMIZED);
    public static readonly WindowHintType<bool> CenterCursor = new WindowHintType<bool>(NativeGlfw.GLFW_CENTER_CURSOR);
    public static readonly WindowHintType<bool> TransparentFramebuffer = new WindowHintType<bool>(NativeGlfw.GLFW_TRANSPARENT_FRAMEBUFFER);
    public static readonly WindowHintType<bool> FocusOnShow = new WindowHintType<bool>(NativeGlfw.GLFW_FOCUS_ON_SHOW);
    public static readonly WindowHintType<bool> ScaleToMonitor = new WindowHintType<bool>(NativeGlfw.GLFW_SCALE_TO_MONITOR);

    public static readonly WindowHintType<int> RedBits = new WindowHintType<int>(NativeGlfw.GLFW_RED_BITS);
    public static readonly WindowHintType<int> GreenBits = new WindowHintType<int>(NativeGlfw.GLFW_GREEN_BITS);
    public static readonly WindowHintType<int> BlueBits = new WindowHintType<int>(NativeGlfw.GLFW_BLUE_BITS);
    public static readonly WindowHintType<int> AlphaBits = new WindowHintType<int>(NativeGlfw.GLFW_ALPHA_BITS);
    public static readonly WindowHintType<int> DepthBits = new WindowHintType<int>(NativeGlfw.GLFW_DEPTH_BITS);
    public static readonly WindowHintType<int> StencilBits = new WindowHintType<int>(NativeGlfw.GLFW_STENCIL_BITS);
    public static readonly WindowHintType<int> AccumRedBits = new WindowHintType<int>(NativeGlfw.GLFW_ACCUM_RED_BITS);
    public static readonly WindowHintType<int> AccumGreenBits = new WindowHintType<int>(NativeGlfw.GLFW_ACCUM_GREEN_BITS);
    public static readonly WindowHintType<int> AccumBlueBits = new WindowHintType<int>(NativeGlfw.GLFW_ACCUM_BLUE_BITS);
    public static readonly WindowHintType<int> AccumAlphaBits = new WindowHintType<int>(NativeGlfw.GLFW_ACCUM_ALPHA_BITS);
    public static readonly WindowHintType<int> AuxBuffers = new WindowHintType<int>(NativeGlfw.GLFW_AUX_BUFFERS);
    public static readonly WindowHintType<int> Samples = new WindowHintType<int>(NativeGlfw.GLFW_SAMPLES);
    public static readonly WindowHintType<int> RefreshRate = new WindowHintType<int>(NativeGlfw.GLFW_REFRESH_RATE);

    public static readonly WindowHintType<bool> Stereo = new WindowHintType<bool>(NativeGlfw.GLFW_STEREO);
    public static readonly WindowHintType<bool> SRGBCapable = new WindowHintType<bool>(NativeGlfw.GLFW_SRGB_CAPABLE);
    public static readonly WindowHintType<bool> DoubleBuffer = new WindowHintType<bool>(NativeGlfw.GLFW_DOUBLEBUFFER);

    public static readonly WindowHintType<ClientAPI> ClientAPI = new WindowHintType<ClientAPI>(NativeGlfw.GLFW_CLIENT_API);
    public static readonly WindowHintType<ContextCreationAPI> ContextCreationAPI = new WindowHintType<ContextCreationAPI>(NativeGlfw.GLFW_CONTEXT_CREATION_API);
    public static readonly WindowHintType<int> ContextVersionMajor = new WindowHintType<int>(NativeGlfw.GLFW_CONTEXT_VERSION_MAJOR);
    public static readonly WindowHintType<int> ContextVersionMinor = new WindowHintType<int>(NativeGlfw.GLFW_CONTEXT_VERSION_MINOR);
    public static readonly WindowHintType<ContextRobustness> ContextRobustness = new WindowHintType<ContextRobustness>(NativeGlfw.GLFW_CONTEXT_ROBUSTNESS);
    public static readonly WindowHintType<ContextReleaseBehaviour> ContextReleaseBehaviour = new WindowHintType<ContextReleaseBehaviour>(NativeGlfw.GLFW_CONTEXT_RELEASE_BEHAVIOR);

    public static readonly WindowHintType<bool> OpenGLForwardCompat = new WindowHintType<bool>(NativeGlfw.GLFW_OPENGL_FORWARD_COMPAT);
    public static readonly WindowHintType<bool> OpenGLDebugContext = new WindowHintType<bool>(NativeGlfw.GLFW_OPENGL_DEBUG_CONTEXT);
    public static readonly WindowHintType<OpenGLProfile> OpenGLProfile = new WindowHintType<OpenGLProfile>(NativeGlfw.GLFW_OPENGL_PROFILE);

    public static readonly WindowHintType<bool> CocoaRetinaFramebuffer = new WindowHintType<bool>(NativeGlfw.GLFW_COCOA_RETINA_FRAMEBUFFER);
    public static readonly WindowHintType<string> CocoaFrameName = new WindowHintType<string>(NativeGlfw.GLFW_COCOA_FRAME_NAME);
    public static readonly WindowHintType<bool> CocoaGraphicsSwitching = new WindowHintType<bool>(NativeGlfw.GLFW_COCOA_GRAPHICS_SWITCHING);

    public static readonly WindowHintType<string> X11ClassName = new WindowHintType<string>(NativeGlfw.GLFW_X11_CLASS_NAME);
    public static readonly WindowHintType<string> X11InstanceName = new WindowHintType<string>(NativeGlfw.GLFW_X11_INSTANCE_NAME);
}

/// <summary>
/// Wrapper class for window hints that only accept certain values.
/// Is used to make it easier for developers to see what values are expected or allowed.
/// </summary>
public class WindowHintType<T>
{
    internal int Hint { get; }

    internal WindowHintType(int hint)
    {
        Hint = hint;
    }
}