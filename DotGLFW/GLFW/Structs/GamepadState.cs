using System.Runtime.InteropServices;

namespace DotGLFW;

public partial class Gamepadstate
{
  /// <summary>
  /// Get the state of a gamepad button.
  /// </summary>
  /// <param name="button">The button to check.</param>
  /// <returns>The state of the button.</returns>
  public InputState GetButtonState(GamepadButton button) => (InputState)Buttons[(int)button];

  /// <summary>
  /// Get the state of a gamepad axis.
  /// </summary>
  /// <param name="axis">The axis to check.</param>
  /// <returns>The state of the axis.</returns>
  public float GetAxis(GamepadAxis axis) => Axes[(int)axis];
}