namespace DotGLFW;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

/// <summary>
/// An enumerator for all joystick hat positions
/// </summary>
public enum JoystickHat
{
    Centered = NativeGlfw.GLFW_HAT_CENTERED,
    Up = NativeGlfw.GLFW_HAT_UP,
    Right = NativeGlfw.GLFW_HAT_RIGHT,
    Down = NativeGlfw.GLFW_HAT_DOWN,
    Left = NativeGlfw.GLFW_HAT_LEFT,
    RightUp = NativeGlfw.GLFW_HAT_RIGHT_UP,
    RightDown = NativeGlfw.GLFW_HAT_RIGHT_DOWN,
    LeftUp = NativeGlfw.GLFW_HAT_LEFT_UP,
    LeftDown = NativeGlfw.GLFW_HAT_LEFT_DOWN
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member