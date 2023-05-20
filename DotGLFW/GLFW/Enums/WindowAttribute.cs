namespace DotGLFW;

public static class WindowAttrib
{
    public static readonly WindowAttribType<bool> Focused = new WindowAttribType<bool>(NativeGlfw.GLFW_FOCUSED);
    public static readonly WindowAttribType<bool> Iconified = new WindowAttribType<bool>(NativeGlfw.GLFW_ICONIFIED);
    public static readonly WindowAttribType<bool> Maximized = new WindowAttribType<bool>(NativeGlfw.GLFW_MAXIMIZED);
    public static readonly WindowAttribType<bool> Hovered = new WindowAttribType<bool>(NativeGlfw.GLFW_HOVERED);
    public static readonly WindowAttribType<bool> Visible = new WindowAttribType<bool>(NativeGlfw.GLFW_VISIBLE);
    public static readonly WindowAttribType<bool> Resizable = new WindowAttribType<bool>(NativeGlfw.GLFW_RESIZABLE);
    public static readonly WindowAttribType<bool> Decorated = new WindowAttribType<bool>(NativeGlfw.GLFW_DECORATED);
    public static readonly WindowAttribType<bool> AutoIconify = new WindowAttribType<bool>(NativeGlfw.GLFW_AUTO_ICONIFY);
    public static readonly WindowAttribType<bool> Floating = new WindowAttribType<bool>(NativeGlfw.GLFW_FLOATING);
    public static readonly WindowAttribType<bool> TransparentFramebuffer = new WindowAttribType<bool>(NativeGlfw.GLFW_TRANSPARENT_FRAMEBUFFER);
    public static readonly WindowAttribType<bool> FocusOnShow = new WindowAttribType<bool>(NativeGlfw.GLFW_FOCUS_ON_SHOW);

    public static readonly WindowAttribType<ClientAPI> ClientAPI = new WindowAttribType<ClientAPI>(NativeGlfw.GLFW_CLIENT_API);
    public static readonly WindowAttribType<ContextCreationAPI> ContextCreationAPI = new WindowAttribType<ContextCreationAPI>(NativeGlfw.GLFW_CONTEXT_CREATION_API);
    public static readonly WindowAttribType<int> ContextVersionMajor = new WindowAttribType<int>(NativeGlfw.GLFW_CONTEXT_VERSION_MAJOR);
    public static readonly WindowAttribType<int> ContextVersionMinor = new WindowAttribType<int>(NativeGlfw.GLFW_CONTEXT_VERSION_MINOR);
    public static readonly WindowAttribType<int> ContextRevision = new WindowAttribType<int>(NativeGlfw.GLFW_CONTEXT_REVISION);

    public static readonly WindowAttribType<bool> OpenGLForwardCompat = new WindowAttribType<bool>(NativeGlfw.GLFW_OPENGL_FORWARD_COMPAT);
    public static readonly WindowAttribType<bool> OpenGLDebugContext = new WindowAttribType<bool>(NativeGlfw.GLFW_OPENGL_DEBUG_CONTEXT);
    public static readonly WindowAttribType<OpenGLProfile> OpenGLProfile = new WindowAttribType<OpenGLProfile>(NativeGlfw.GLFW_OPENGL_PROFILE);
    public static readonly WindowAttribType<ContextReleaseBehaviour> ContextReleaseBehaviour = new WindowAttribType<ContextReleaseBehaviour>(NativeGlfw.GLFW_CONTEXT_RELEASE_BEHAVIOR);

    public static readonly WindowAttribType<bool> ContextNoError = new WindowAttribType<bool>(NativeGlfw.GLFW_CONTEXT_NO_ERROR);
    public static readonly WindowAttribType<ContextRobustness> ContextRobustness = new WindowAttribType<ContextRobustness>(NativeGlfw.GLFW_CONTEXT_ROBUSTNESS);
}

/// <summary>
/// Wrapper class for window hints that only accept certain values.
/// Is used to make it easier for developers to see what values are expected or allowed.
/// </summary>
public class WindowAttribType<T>
{
    internal int Attribute { get; }

    internal WindowAttribType(int attribute)
    {
        Attribute = attribute;
    }
}