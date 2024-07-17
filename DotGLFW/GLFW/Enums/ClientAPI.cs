namespace DotGLFW;

/// <summary>
///     Client API.
/// </summary>
public enum ClientAPI
{
  /// <summary>
  ///     No API.
  /// </summary>
  NoAPI = NativeGlfw.GLFW_NO_API,

  /// <summary>
  ///     OpenGL API.
  /// </summary>
  OpenGLAPI = NativeGlfw.GLFW_OPENGL_API,

  /// <summary>
  ///     OpenGL ES API.
  /// </summary>
  OpenGLESAPI = NativeGlfw.GLFW_OPENGL_ES_API,
}