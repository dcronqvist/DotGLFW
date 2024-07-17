namespace DotGLFW;

/// <summary>
///     Context creation API.
/// </summary>
public enum ContextCreationAPI
{
  /// <summary>
  ///     Native context API.
  /// </summary>
  NativeContextAPI = NativeGlfw.GLFW_NATIVE_CONTEXT_API,

  /// <summary>
  ///     EGL context API.
  /// </summary>
  EGLContextAPI = NativeGlfw.GLFW_EGL_CONTEXT_API,

  /// <summary>
  ///     OSMesa context API.
  /// </summary>
  OSMesaContextAPI = NativeGlfw.GLFW_OSMESA_CONTEXT_API,
}