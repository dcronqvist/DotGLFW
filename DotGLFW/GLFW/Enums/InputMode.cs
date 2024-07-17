namespace DotGLFW;

/// <summary>
/// All possible input modes.
/// </summary>
public static class InputMode
{
  /// <summary>
  /// Input mode for cursors.
  /// </summary>
  public static readonly InputModeType<CursorMode> Cursor = new InputModeType<CursorMode>(NativeGlfw.GLFW_CURSOR);

  /// <summary>
  /// Input mode for sticky keys.
  /// </summary>
  public static readonly InputModeType<bool> StickyKeys = new InputModeType<bool>(NativeGlfw.GLFW_STICKY_KEYS);

  /// <summary>
  /// Input mode for sticky mouse buttons.
  /// </summary>
  public static readonly InputModeType<bool> StickyMouseButtons = new InputModeType<bool>(NativeGlfw.GLFW_STICKY_MOUSE_BUTTONS);

  /// <summary>
  /// Input mode for lock key mods.
  /// </summary>
  public static readonly InputModeType<bool> LockKeyMods = new InputModeType<bool>(NativeGlfw.GLFW_LOCK_KEY_MODS);

  /// <summary>
  /// Input mode for raw mouse motion.
  /// </summary>
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