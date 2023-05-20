namespace DotGLFW;

/// <summary>
///     Window hints are set before <see cref="Glfw.CreateWindow" /> and affect how the window and its context should be created.
/// </summary>
public static class Hint
{
    public static readonly HintType<bool> Resizable = new HintType<bool>(NativeGlfw.GLFW_RESIZABLE);
    public static readonly HintType<bool> Visible = new HintType<bool>(NativeGlfw.GLFW_VISIBLE);
    public static readonly HintType<bool> Decorated = new HintType<bool>(NativeGlfw.GLFW_DECORATED);
    public static readonly HintType<bool> Focused = new HintType<bool>(NativeGlfw.GLFW_FOCUSED);
    public static readonly HintType<bool> AutoIconify = new HintType<bool>(NativeGlfw.GLFW_AUTO_ICONIFY);
    public static readonly HintType<bool> Floating = new HintType<bool>(NativeGlfw.GLFW_FLOATING);
    public static readonly HintType<bool> Maximized = new HintType<bool>(NativeGlfw.GLFW_MAXIMIZED);
    public static readonly HintType<bool> CenterCursor = new HintType<bool>(NativeGlfw.GLFW_CENTER_CURSOR);
    public static readonly HintType<bool> TransparentFramebuffer = new HintType<bool>(NativeGlfw.GLFW_TRANSPARENT_FRAMEBUFFER);
    public static readonly HintType<bool> FocusOnShow = new HintType<bool>(NativeGlfw.GLFW_FOCUS_ON_SHOW);
    public static readonly HintType<bool> ScaleToMonitor = new HintType<bool>(NativeGlfw.GLFW_SCALE_TO_MONITOR);

    public static readonly HintType<int> RedBits = new HintType<int>(NativeGlfw.GLFW_RED_BITS);
    public static readonly HintType<int> GreenBits = new HintType<int>(NativeGlfw.GLFW_GREEN_BITS);
    public static readonly HintType<int> BlueBits = new HintType<int>(NativeGlfw.GLFW_BLUE_BITS);
    public static readonly HintType<int> AlphaBits = new HintType<int>(NativeGlfw.GLFW_ALPHA_BITS);
    public static readonly HintType<int> DepthBits = new HintType<int>(NativeGlfw.GLFW_DEPTH_BITS);
    public static readonly HintType<int> StencilBits = new HintType<int>(NativeGlfw.GLFW_STENCIL_BITS);
    public static readonly HintType<int> AccumRedBits = new HintType<int>(NativeGlfw.GLFW_ACCUM_RED_BITS);
    public static readonly HintType<int> AccumGreenBits = new HintType<int>(NativeGlfw.GLFW_ACCUM_GREEN_BITS);
    public static readonly HintType<int> AccumBlueBits = new HintType<int>(NativeGlfw.GLFW_ACCUM_BLUE_BITS);
    public static readonly HintType<int> AccumAlphaBits = new HintType<int>(NativeGlfw.GLFW_ACCUM_ALPHA_BITS);
    public static readonly HintType<int> AuxBuffers = new HintType<int>(NativeGlfw.GLFW_AUX_BUFFERS);
    public static readonly HintType<int> Samples = new HintType<int>(NativeGlfw.GLFW_SAMPLES);
    public static readonly HintType<int> RefreshRate = new HintType<int>(NativeGlfw.GLFW_REFRESH_RATE);

    public static readonly HintType<bool> Stereo = new HintType<bool>(NativeGlfw.GLFW_STEREO);
    public static readonly HintType<bool> SRGBCapable = new HintType<bool>(NativeGlfw.GLFW_SRGB_CAPABLE);
    public static readonly HintType<bool> DoubleBuffer = new HintType<bool>(NativeGlfw.GLFW_DOUBLEBUFFER);

    public static readonly HintType<ClientAPI> ClientAPI = new HintType<ClientAPI>(NativeGlfw.GLFW_CLIENT_API);
    public static readonly HintType<ContextCreationAPI> ContextCreationAPI = new HintType<ContextCreationAPI>(NativeGlfw.GLFW_CONTEXT_CREATION_API);
    public static readonly HintType<int> ContextVersionMajor = new HintType<int>(NativeGlfw.GLFW_CONTEXT_VERSION_MAJOR);
    public static readonly HintType<int> ContextVersionMinor = new HintType<int>(NativeGlfw.GLFW_CONTEXT_VERSION_MINOR);
    public static readonly HintType<ContextRobustness> ContextRobustness = new HintType<ContextRobustness>(NativeGlfw.GLFW_CONTEXT_ROBUSTNESS);
    public static readonly HintType<ContextReleaseBehaviour> ContextReleaseBehaviour = new HintType<ContextReleaseBehaviour>(NativeGlfw.GLFW_CONTEXT_RELEASE_BEHAVIOR);

    public static readonly HintType<bool> OpenGLForwardCompat = new HintType<bool>(NativeGlfw.GLFW_OPENGL_FORWARD_COMPAT);
    public static readonly HintType<bool> OpenGLDebugContext = new HintType<bool>(NativeGlfw.GLFW_OPENGL_DEBUG_CONTEXT);
    public static readonly HintType<OpenGLProfile> OpenGLProfile = new HintType<OpenGLProfile>(NativeGlfw.GLFW_OPENGL_PROFILE);

    public static readonly HintType<bool> CocoaRetinaFramebuffer = new HintType<bool>(NativeGlfw.GLFW_COCOA_RETINA_FRAMEBUFFER);
    public static readonly HintType<string> CocoaFrameName = new HintType<string>(NativeGlfw.GLFW_COCOA_FRAME_NAME);
    public static readonly HintType<bool> CocoaGraphicsSwitching = new HintType<bool>(NativeGlfw.GLFW_COCOA_GRAPHICS_SWITCHING);

    public static readonly HintType<string> X11ClassName = new HintType<string>(NativeGlfw.GLFW_X11_CLASS_NAME);
    public static readonly HintType<string> X11InstanceName = new HintType<string>(NativeGlfw.GLFW_X11_INSTANCE_NAME);
}

/// <summary>
/// Wrapper class for window hints that only accept certain values.
/// Is used to make it easier for developers to see what values are expected or allowed.
/// </summary>
public class HintType<T>
{
    internal int Hint { get; }

    internal HintType(int hint)
    {
        Hint = hint;
    }
}