namespace DotGLFW;

/// <summary>
///     Window hints are set before <see cref="Glfw.CreateWindow" /> and affect how the window and its context should be created.
/// </summary>
public static class Hint
{
    /// <summary>
    /// Whether the windowed window will be resizable by the user.
    /// </summary>
    public static readonly HintType<bool> Resizable = new HintType<bool>(NativeGlfw.GLFW_RESIZABLE);

    /// <summary>
    /// Whether the windowed window will be initially visible.
    /// </summary>
    public static readonly HintType<bool> Visible = new HintType<bool>(NativeGlfw.GLFW_VISIBLE);

    /// <summary>
    /// Whether the windowed window will have window decorations such as a border, a close widget, etc.
    /// </summary>
    public static readonly HintType<bool> Decorated = new HintType<bool>(NativeGlfw.GLFW_DECORATED);

    /// <summary>
    /// Specifies whether the windowed mode window will be given input focus when created.
    /// </summary>
    public static readonly HintType<bool> Focused = new HintType<bool>(NativeGlfw.GLFW_FOCUSED);

    /// <summary>
    /// Specifies whether the full screen window will automatically iconify and restore the previous video mode on input focus loss.
    /// </summary>
    public static readonly HintType<bool> AutoIconify = new HintType<bool>(NativeGlfw.GLFW_AUTO_ICONIFY);

    /// <summary>
    /// Specifies whether the windowed mode window will be floating above other regular windows, also called topmost or always-on-top.
    /// </summary>
    public static readonly HintType<bool> Floating = new HintType<bool>(NativeGlfw.GLFW_FLOATING);

    /// <summary>
    /// Specifies whether the windowed mode window will be maximized when created.
    /// </summary>
    public static readonly HintType<bool> Maximized = new HintType<bool>(NativeGlfw.GLFW_MAXIMIZED);

    /// <summary>
    /// Specifies whether the cursor should be centered over newly created full screen windows.
    /// </summary>
    public static readonly HintType<bool> CenterCursor = new HintType<bool>(NativeGlfw.GLFW_CENTER_CURSOR);

    /// <summary>
    /// Specifies whether the window framebuffer will be transparent.
    /// </summary>
    public static readonly HintType<bool> TransparentFramebuffer = new HintType<bool>(NativeGlfw.GLFW_TRANSPARENT_FRAMEBUFFER);

    /// <summary>
    /// Specifies whether the window will be given input focus when <see cref="Glfw.ShowWindow" /> is called.
    /// </summary>
    public static readonly HintType<bool> FocusOnShow = new HintType<bool>(NativeGlfw.GLFW_FOCUS_ON_SHOW);

    /// <summary>
    /// Specifies whether the window content area should be resized based on the monitor content scale when the window is moved between monitors that use a different content scale; if enabled, window content scale will also be queried on creation of the window and whenever the window is moved to a monitor that has a different content scale from the current monitor.
    /// </summary>
    public static readonly HintType<bool> ScaleToMonitor = new HintType<bool>(NativeGlfw.GLFW_SCALE_TO_MONITOR);

    /// <summary>
    /// Specifies how many bits to use for the red component of the color buffer. Possible values are 0, 4 and 8.
    /// </summary>
    public static readonly HintType<int> RedBits = new HintType<int>(NativeGlfw.GLFW_RED_BITS);

    /// <summary>
    /// Specifies how many bits to use for the green component of the color buffer. Possible values are 0, 4 and 8.
    /// </summary>
    public static readonly HintType<int> GreenBits = new HintType<int>(NativeGlfw.GLFW_GREEN_BITS);

    /// <summary>
    /// Specifies how many bits to use for the blue component of the color buffer. Possible values are 0, 4 and 8.
    /// </summary>
    public static readonly HintType<int> BlueBits = new HintType<int>(NativeGlfw.GLFW_BLUE_BITS);

    /// <summary>
    /// Specifies how many bits to use for the alpha component of the color buffer. Possible values are 0, 4 and 8.
    /// </summary>
    public static readonly HintType<int> AlphaBits = new HintType<int>(NativeGlfw.GLFW_ALPHA_BITS);

    /// <summary>
    /// Specifies how many bits to use for the depth buffer. Possible values are 0, 16 and 24.
    /// </summary>
    public static readonly HintType<int> DepthBits = new HintType<int>(NativeGlfw.GLFW_DEPTH_BITS);

    /// <summary>
    /// Specifies how many bits to use for the stencil buffer. Possible values are 0 and 8.
    /// </summary>
    public static readonly HintType<int> StencilBits = new HintType<int>(NativeGlfw.GLFW_STENCIL_BITS);

    /// <summary>
    /// Specifies how many bits to use for the red component of the accumulation buffer. Possible values are 0, 4 and 8.
    /// </summary>
    public static readonly HintType<int> AccumRedBits = new HintType<int>(NativeGlfw.GLFW_ACCUM_RED_BITS);

    /// <summary>
    /// Specifies how many bits to use for the green component of the accumulation buffer. Possible values are 0, 4 and 8.
    /// </summary>
    public static readonly HintType<int> AccumGreenBits = new HintType<int>(NativeGlfw.GLFW_ACCUM_GREEN_BITS);

    /// <summary>
    /// Specifies how many bits to use for the blue component of the accumulation buffer. Possible values are 0, 4 and 8.
    /// </summary>
    public static readonly HintType<int> AccumBlueBits = new HintType<int>(NativeGlfw.GLFW_ACCUM_BLUE_BITS);

    /// <summary>
    /// Specifies how many bits to use for the alpha component of the accumulation buffer. Possible values are 0, 4 and 8.
    /// </summary>
    public static readonly HintType<int> AccumAlphaBits = new HintType<int>(NativeGlfw.GLFW_ACCUM_ALPHA_BITS);

    /// <summary>
    /// Specifies the number of auxiliary buffers to use. Possible values are 0 and integers between 1 and 4, inclusive.
    /// </summary>
    public static readonly HintType<int> AuxBuffers = new HintType<int>(NativeGlfw.GLFW_AUX_BUFFERS);

    /// <summary>
    /// Specifies how many samples to use for multisampling. Possible values are integers between 0 and 16, inclusive.
    /// </summary>
    public static readonly HintType<int> Samples = new HintType<int>(NativeGlfw.GLFW_SAMPLES);

    /// <summary>
    /// Specifies the desired refresh rate for full screen windows. If set to <see cref="Glfw.DontCare" /> then the highest available refresh rate will be used. This hint is ignored for windowed mode windows.
    /// </summary>
    public static readonly HintType<int> RefreshRate = new HintType<int>(NativeGlfw.GLFW_REFRESH_RATE);

    /// <summary>
    /// Specifies whether to use stereoscopic rendering. This is a hard constraint.
    /// </summary>
    public static readonly HintType<bool> Stereo = new HintType<bool>(NativeGlfw.GLFW_STEREO);

    /// <summary>
    /// Specifies whether the framebuffer should be sRGB capable. 
    /// </summary>
    public static readonly HintType<bool> SRGBCapable = new HintType<bool>(NativeGlfw.GLFW_SRGB_CAPABLE);

    /// <summary>
    /// Specifies whether the framebuffer should be double buffered. You nearly always want to use double buffering. This is a hard constraint.
    /// </summary>
    public static readonly HintType<bool> DoubleBuffer = new HintType<bool>(NativeGlfw.GLFW_DOUBLEBUFFER);

    /// <summary>
    /// Specifies the client API to create the context for. Possible values are <see cref="ClientAPI.OpenGLAPI" />, <see cref="ClientAPI.OpenGLESAPI" /> and <see cref="ClientAPI.NoAPI" />.
    /// </summary>
    public static readonly HintType<ClientAPI> ClientAPI = new HintType<ClientAPI>(NativeGlfw.GLFW_CLIENT_API);

    /// <summary>
    /// Specifies the context creation API to use. Possible values are <see cref="ContextCreationAPI.NativeContextAPI" />, <see cref="ContextCreationAPI.EGLContextAPI" />, and <see cref="ContextCreationAPI.OSMesaContextAPI" />.
    /// </summary>
    public static readonly HintType<ContextCreationAPI> ContextCreationAPI = new HintType<ContextCreationAPI>(NativeGlfw.GLFW_CONTEXT_CREATION_API);

    /// <summary>
    /// Specifies the client API major version that the created context must be compatible with. The exact behavior of this hint depends on the requested client API.
    /// </summary>
    public static readonly HintType<int> ContextVersionMajor = new HintType<int>(NativeGlfw.GLFW_CONTEXT_VERSION_MAJOR);

    /// <summary>
    /// Specifies the client API minor version that the created context must be compatible with. The exact behavior of this hint depends on the requested client API.
    /// </summary>
    public static readonly HintType<int> ContextVersionMinor = new HintType<int>(NativeGlfw.GLFW_CONTEXT_VERSION_MINOR);

    /// <summary>
    /// Specifies the client API revision number that the created context must be compatible with. The exact behavior of this hint depends on the requested client API.
    /// </summary>
    public static readonly HintType<ContextRobustness> ContextRobustness = new HintType<ContextRobustness>(NativeGlfw.GLFW_CONTEXT_ROBUSTNESS);

    /// <summary>
    /// Specifies the release behavior to be used by the context. Possible values are <see cref="ContextReleaseBehaviour.AnyReleaseBehaviour" />, <see cref="ContextReleaseBehaviour.ReleaseBehaviourFlush" /> and <see cref="ContextReleaseBehaviour.ReleaseBehaviourNone" />.
    /// </summary>
    public static readonly HintType<ContextReleaseBehaviour> ContextReleaseBehaviour = new HintType<ContextReleaseBehaviour>(NativeGlfw.GLFW_CONTEXT_RELEASE_BEHAVIOR);

    /// <summary>
    /// Specifies whether to use OpenGL forward compatibility. If set to <c>true</c>, then no deprecated functionality will be supported by the created context. Possible values are <c>true</c> and <c>false</c>.
    /// </summary>
    public static readonly HintType<bool> OpenGLForwardCompat = new HintType<bool>(NativeGlfw.GLFW_OPENGL_FORWARD_COMPAT);

    /// <summary>
    /// Specifies whether to use OpenGL debug context. If set to <c>true</c>, then debug messages will be generated by the context. Possible values are <c>true</c> and <c>false</c>.
    /// </summary>
    public static readonly HintType<bool> OpenGLDebugContext = new HintType<bool>(NativeGlfw.GLFW_OPENGL_DEBUG_CONTEXT);

    /// <summary>
    /// Specifies the OpenGL profile used by the context. Possible values are <see cref="OpenGLProfile.AnyProfile" />, <see cref="OpenGLProfile.CoreProfile" /> and <see cref="OpenGLProfile.CompatProfile" />.
    /// </summary>
    public static readonly HintType<OpenGLProfile> OpenGLProfile = new HintType<OpenGLProfile>(NativeGlfw.GLFW_OPENGL_PROFILE);

    /// <summary>
    /// Specifies whether to use full resolution framebuffers on Retina displays.
    /// </summary>
    public static readonly HintType<bool> CocoaRetinaFramebuffer = new HintType<bool>(NativeGlfw.GLFW_COCOA_RETINA_FRAMEBUFFER);

    /// <summary>
    /// Specifies the UTF-8 encoded name to use for autosaving the window frame, or <c>null</c> for no autosave. This is only supported on OS X.
    /// </summary>
    public static readonly HintType<string> CocoaFrameName = new HintType<string>(NativeGlfw.GLFW_COCOA_FRAME_NAME);

    /// <summary>
    /// Specifies whether to enable automatic graphics switching, i.e. to allow the system to choose the integrated GPU for the OpenGL context and move it between GPUs if necessary or whether to force it to always run on the discrete GPU. This is only supported on OS X.
    /// </summary>
    public static readonly HintType<bool> CocoaGraphicsSwitching = new HintType<bool>(NativeGlfw.GLFW_COCOA_GRAPHICS_SWITCHING);

    /// <summary>
    /// Specifies the desired ASCII encoded class part of the ICCCM class hint for the window. This is only supported on X11.
    /// </summary>
    public static readonly HintType<string> X11ClassName = new HintType<string>(NativeGlfw.GLFW_X11_CLASS_NAME);

    /// <summary>
    /// Specifies the desired ASCII encoded instance part of the ICCCM class hint for the window. This is only supported on X11.
    /// </summary>
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