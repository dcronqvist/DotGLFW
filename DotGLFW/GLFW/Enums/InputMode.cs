namespace DotGLFW;

public static class InputMode
{
    public static readonly InputModeType<CursorMode> Cursor = new InputModeType<CursorMode>(NativeGlfw.GLFW_CURSOR);
    public static readonly InputModeType<bool> StickyKeys = new InputModeType<bool>(NativeGlfw.GLFW_STICKY_KEYS);
    public static readonly InputModeType<bool> StickyMouseButtons = new InputModeType<bool>(NativeGlfw.GLFW_STICKY_MOUSE_BUTTONS);
    public static readonly InputModeType<bool> LockKeyMods = new InputModeType<bool>(NativeGlfw.GLFW_LOCK_KEY_MODS);
    public static readonly InputModeType<bool> RawMouseMotion = new InputModeType<bool>(NativeGlfw.GLFW_RAW_MOUSE_MOTION);
}

/// <summary>
/// Wrapper class for window hints that only accept certain values.
/// Is used to make it easier for developers to see what values are expected or allowed.
/// </summary>
public class InputModeType<T>
{
    internal int Mode { get; }

    internal InputModeType(int mode)
    {
        Mode = mode;
    }
}