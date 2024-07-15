namespace DotGLFW;

/// <summary>
///     Robustness strategy.
/// </summary>
public enum ContextRobustness
{
  /// <summary>
  ///     No robustness strategy.
  /// </summary>
  NoRobustness = NativeGlfw.GLFW_NO_ROBUSTNESS,

  /// <summary>
  ///     No robustness strategy.
  /// </summary>
  NoResetNotification = NativeGlfw.GLFW_NO_RESET_NOTIFICATION,

  /// <summary>
  ///     No robustness strategy.
  /// </summary>
  LoseContextOnReset = NativeGlfw.GLFW_LOSE_CONTEXT_ON_RESET,
}