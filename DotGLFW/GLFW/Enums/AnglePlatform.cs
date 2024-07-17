namespace DotGLFW;

/// <summary>
/// Specifies the platform type (rendering backend) to request when using OpenGL ES and EGL via ANGLE.
/// </summary>
public enum AnglePlatform
{
  /// <summary>
  /// No platform specified.
  /// </summary>
  None = NativeGlfw.GLFW_ANGLE_PLATFORM_TYPE_NONE,

  /// <summary>
  /// OpenGL platform.
  /// </summary>
  OpenGL = NativeGlfw.GLFW_ANGLE_PLATFORM_TYPE_OPENGL,

  /// <summary>
  /// OpenGL ES platform.
  /// </summary>
  OpenGLES = NativeGlfw.GLFW_ANGLE_PLATFORM_TYPE_OPENGLES,

  /// <summary>
  /// Direct3D 9 platform.
  /// </summary>
  D3D9 = NativeGlfw.GLFW_ANGLE_PLATFORM_TYPE_D3D9,

  /// <summary>
  /// Direct3D 11 platform.
  /// </summary>
  D3D11 = NativeGlfw.GLFW_ANGLE_PLATFORM_TYPE_D3D11,

  /// <summary>
  /// Vulkan platform.
  /// </summary>
  Vulkan = NativeGlfw.GLFW_ANGLE_PLATFORM_TYPE_VULKAN,

  /// <summary>
  /// Metal platform.
  /// </summary>
  Metal = NativeGlfw.GLFW_ANGLE_PLATFORM_TYPE_METAL
}