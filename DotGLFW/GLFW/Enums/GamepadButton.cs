namespace DotGLFW;

public enum GamepadButton
{
    A = NativeGlfw.GLFW_GAMEPAD_BUTTON_A,
    B = NativeGlfw.GLFW_GAMEPAD_BUTTON_B,
    X = NativeGlfw.GLFW_GAMEPAD_BUTTON_X,
    Y = NativeGlfw.GLFW_GAMEPAD_BUTTON_Y,
    LeftBumper = NativeGlfw.GLFW_GAMEPAD_BUTTON_LEFT_BUMPER,
    RightBumper = NativeGlfw.GLFW_GAMEPAD_BUTTON_RIGHT_BUMPER,
    Back = NativeGlfw.GLFW_GAMEPAD_BUTTON_BACK,
    Start = NativeGlfw.GLFW_GAMEPAD_BUTTON_START,
    Guide = NativeGlfw.GLFW_GAMEPAD_BUTTON_GUIDE,
    LeftThumb = NativeGlfw.GLFW_GAMEPAD_BUTTON_LEFT_THUMB,
    RightThumb = NativeGlfw.GLFW_GAMEPAD_BUTTON_RIGHT_THUMB,
    DpadUp = NativeGlfw.GLFW_GAMEPAD_BUTTON_DPAD_UP,
    DpadRight = NativeGlfw.GLFW_GAMEPAD_BUTTON_DPAD_RIGHT,
    DpadDown = NativeGlfw.GLFW_GAMEPAD_BUTTON_DPAD_DOWN,
    DpadLeft = NativeGlfw.GLFW_GAMEPAD_BUTTON_DPAD_LEFT,

    Cross = A,
    Circle = B,
    Square = X,
    Triangle = Y,
}

public enum GamepadAxis
{
    LeftX = NativeGlfw.GLFW_GAMEPAD_AXIS_LEFT_X,
    LeftY = NativeGlfw.GLFW_GAMEPAD_AXIS_LEFT_Y,
    RightX = NativeGlfw.GLFW_GAMEPAD_AXIS_RIGHT_X,
    RightY = NativeGlfw.GLFW_GAMEPAD_AXIS_RIGHT_Y,
    LeftTrigger = NativeGlfw.GLFW_GAMEPAD_AXIS_LEFT_TRIGGER,
    RightTrigger = NativeGlfw.GLFW_GAMEPAD_AXIS_RIGHT_TRIGGER,
}