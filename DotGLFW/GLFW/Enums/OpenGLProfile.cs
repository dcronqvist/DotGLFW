namespace DotGLFW;

/// <summary>
///     OpenGL profile.
/// </summary>
public enum OpenGLProfile
{
  /// <summary>
  ///     Any OpenGL profile.
  /// </summary>
  AnyProfile = NativeGlfw.GLFW_OPENGL_ANY_PROFILE,

  /// <summary>
  ///     OpenGL core profile.
  /// </summary>
  CoreProfile = NativeGlfw.GLFW_OPENGL_CORE_PROFILE,

  /// <summary>
  ///     OpenGL compatibility profile.
  /// </summary>
  CompatProfile = NativeGlfw.GLFW_OPENGL_COMPAT_PROFILE,
}