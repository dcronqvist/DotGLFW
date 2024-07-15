namespace DotGLFW;

/// <summary>
///     Connection state.
/// </summary>
public enum ConnectionState
{
  /// <summary>
  ///     The monitor was connected.
  /// </summary>
  Connected = NativeGlfw.GLFW_CONNECTED,

  /// <summary>
  ///     The monitor was disconnected.
  /// </summary>
  Disconnected = NativeGlfw.GLFW_DISCONNECTED,
}