using System.Runtime.InteropServices;

namespace DotGLFW;

/// <summary>
/// Wrapper for a GLFW gamepad state.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class GamepadState
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
    private byte[] _buttons;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    private float[] _axes;

    /// <summary>
    /// Get the state of a gamepad button.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>The state of the button.</returns>
    public InputState GetButtonState(GamepadButton button) => (InputState)_buttons[(int)button];

    /// <summary>
    /// Get the state of a gamepad axis.
    /// </summary>
    /// <param name="axis">The axis to check.</param>
    /// <returns>The state of the axis.</returns>
    public float GetAxis(GamepadAxis axis) => _axes[(int)axis];
}