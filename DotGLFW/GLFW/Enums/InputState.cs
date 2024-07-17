namespace DotGLFW;

/// <summary>
/// Input state for keys and mouse buttons.
/// </summary>
public enum InputState
{
  /// <summary>
  /// The key or mouse button was released.
  /// </summary>
  Release = NativeGlfw.GLFW_RELEASE,

  /// <summary>
  /// The key or mouse button was pressed.
  /// </summary>
  Press = NativeGlfw.GLFW_PRESS,

  /// <summary>
  /// The key was held down (repeating).
  /// </summary>
  Repeat = NativeGlfw.GLFW_REPEAT,
}