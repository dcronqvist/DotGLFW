using System.Runtime.InteropServices;

namespace DotGLFW;

[StructLayout(LayoutKind.Sequential)]
public class GamepadState
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
    private byte[] _buttons;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    private float[] _axes;

    public InputState GetButtonState(GamepadButton button) => (InputState)_buttons[(int)button];

    public float GetAxis(GamepadAxis axis) => _axes[(int)axis];
}