namespace DotGLFW;

/// <summary>
///     Context release behaviour.
/// </summary>
public enum ContextReleaseBehaviour
{
  /// <summary>
  ///     Any context release behaviour.
  /// </summary>
  AnyReleaseBehaviour = NativeGlfw.GLFW_ANY_RELEASE_BEHAVIOR,

  /// <summary>
  ///     Release context behaviour.
  /// </summary>
  ReleaseBehaviourFlush = NativeGlfw.GLFW_RELEASE_BEHAVIOR_FLUSH,

  /// <summary>
  ///     Release context behaviour.
  /// </summary>
  ReleaseBehaviourNone = NativeGlfw.GLFW_RELEASE_BEHAVIOR_NONE,
}