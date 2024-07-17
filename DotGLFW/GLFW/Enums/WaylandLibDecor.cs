namespace DotGLFW;

/// <summary>
/// Specifies whether to use libdecor for window decorations where available.
/// </summary>
public enum WaylandLibDecor
{
  /// <summary>
  /// Prefer libdecor for window decorations where available.
  /// </summary>
  PreferLibDecor = NativeGlfw.GLFW_WAYLAND_PREFER_LIBDECOR,

  /// <summary>
  /// Disable libdecor for window decorations where available.
  /// </summary>
  DisableLibDecor = NativeGlfw.GLFW_WAYLAND_DISABLE_LIBDECOR
}