namespace DotGLFW;

#pragma warning disable 1591

public enum Keys
{
    Unknown = NativeGlfw.GLFW_KEY_UNKNOWN,
    Space = NativeGlfw.GLFW_KEY_SPACE,
    Apostrophe = NativeGlfw.GLFW_KEY_APOSTROPHE, // '
    Comma = NativeGlfw.GLFW_KEY_COMMA, // ,
    Minus = NativeGlfw.GLFW_KEY_MINUS, // -
    Period = NativeGlfw.GLFW_KEY_PERIOD, // .
    Slash = NativeGlfw.GLFW_KEY_SLASH, // /
    D0 = NativeGlfw.GLFW_KEY_0,
    D1 = NativeGlfw.GLFW_KEY_1,
    D2 = NativeGlfw.GLFW_KEY_2,
    D3 = NativeGlfw.GLFW_KEY_3,
    D4 = NativeGlfw.GLFW_KEY_4,
    D5 = NativeGlfw.GLFW_KEY_5,
    D6 = NativeGlfw.GLFW_KEY_6,
    D7 = NativeGlfw.GLFW_KEY_7,
    D8 = NativeGlfw.GLFW_KEY_8,
    D9 = NativeGlfw.GLFW_KEY_9,
    SemiColon = NativeGlfw.GLFW_KEY_SEMICOLON, // ;
    Equal = NativeGlfw.GLFW_KEY_EQUAL, // =
    A = NativeGlfw.GLFW_KEY_A,
    B = NativeGlfw.GLFW_KEY_B,
    C = NativeGlfw.GLFW_KEY_C,
    D = NativeGlfw.GLFW_KEY_D,
    E = NativeGlfw.GLFW_KEY_E,
    F = NativeGlfw.GLFW_KEY_F,
    G = NativeGlfw.GLFW_KEY_G,
    H = NativeGlfw.GLFW_KEY_H,
    I = NativeGlfw.GLFW_KEY_I,
    J = NativeGlfw.GLFW_KEY_J,
    K = NativeGlfw.GLFW_KEY_K,
    L = NativeGlfw.GLFW_KEY_L,
    M = NativeGlfw.GLFW_KEY_M,
    N = NativeGlfw.GLFW_KEY_N,
    O = NativeGlfw.GLFW_KEY_O,
    P = NativeGlfw.GLFW_KEY_P,
    Q = NativeGlfw.GLFW_KEY_Q,
    R = NativeGlfw.GLFW_KEY_R,
    S = NativeGlfw.GLFW_KEY_S,
    T = NativeGlfw.GLFW_KEY_T,
    U = NativeGlfw.GLFW_KEY_U,
    V = NativeGlfw.GLFW_KEY_V,
    W = NativeGlfw.GLFW_KEY_W,
    X = NativeGlfw.GLFW_KEY_X,
    Y = NativeGlfw.GLFW_KEY_Y,
    Z = NativeGlfw.GLFW_KEY_Z,
    LeftBracket = NativeGlfw.GLFW_KEY_LEFT_BRACKET, // [
    BackSlash = NativeGlfw.GLFW_KEY_BACKSLASH, // \
    RightBracket = NativeGlfw.GLFW_KEY_RIGHT_BRACKET, // ]
    GraveAccent = NativeGlfw.GLFW_KEY_GRAVE_ACCENT, // `
    World1 = NativeGlfw.GLFW_KEY_WORLD_1, // non-US #1
    World2 = NativeGlfw.GLFW_KEY_WORLD_2, // non-US #2
    Escape = NativeGlfw.GLFW_KEY_ESCAPE,
    Enter = NativeGlfw.GLFW_KEY_ENTER,
    Tab = NativeGlfw.GLFW_KEY_TAB,
    Backspace = NativeGlfw.GLFW_KEY_BACKSPACE,
    Insert = NativeGlfw.GLFW_KEY_INSERT,
    Delete = NativeGlfw.GLFW_KEY_DELETE,
    Right = NativeGlfw.GLFW_KEY_RIGHT,
    Left = NativeGlfw.GLFW_KEY_LEFT,
    Down = NativeGlfw.GLFW_KEY_DOWN,
    Up = NativeGlfw.GLFW_KEY_UP,
    PageUp = NativeGlfw.GLFW_KEY_PAGE_UP,
    PageDown = NativeGlfw.GLFW_KEY_PAGE_DOWN,
    Home = NativeGlfw.GLFW_KEY_HOME,
    End = NativeGlfw.GLFW_KEY_END,
    CapsLock = NativeGlfw.GLFW_KEY_CAPS_LOCK,
    ScrollLock = NativeGlfw.GLFW_KEY_SCROLL_LOCK,
    NumLock = NativeGlfw.GLFW_KEY_NUM_LOCK,
    PrintScreen = NativeGlfw.GLFW_KEY_PRINT_SCREEN,
    Pause = NativeGlfw.GLFW_KEY_PAUSE,
    F1 = NativeGlfw.GLFW_KEY_F1,
    F2 = NativeGlfw.GLFW_KEY_F2,
    F3 = NativeGlfw.GLFW_KEY_F3,
    F4 = NativeGlfw.GLFW_KEY_F4,
    F5 = NativeGlfw.GLFW_KEY_F5,
    F6 = NativeGlfw.GLFW_KEY_F6,
    F7 = NativeGlfw.GLFW_KEY_F7,
    F8 = NativeGlfw.GLFW_KEY_F8,
    F9 = NativeGlfw.GLFW_KEY_F9,
    F10 = NativeGlfw.GLFW_KEY_F10,
    F11 = NativeGlfw.GLFW_KEY_F11,
    F12 = NativeGlfw.GLFW_KEY_F12,
    F13 = NativeGlfw.GLFW_KEY_F13,
    F14 = NativeGlfw.GLFW_KEY_F14,
    F15 = NativeGlfw.GLFW_KEY_F15,
    F16 = NativeGlfw.GLFW_KEY_F16,
    F17 = NativeGlfw.GLFW_KEY_F17,
    F18 = NativeGlfw.GLFW_KEY_F18,
    F19 = NativeGlfw.GLFW_KEY_F19,
    F20 = NativeGlfw.GLFW_KEY_F20,
    F21 = NativeGlfw.GLFW_KEY_F21,
    F22 = NativeGlfw.GLFW_KEY_F22,
    F23 = NativeGlfw.GLFW_KEY_F23,
    F24 = NativeGlfw.GLFW_KEY_F24,
    F25 = NativeGlfw.GLFW_KEY_F25,
    Kp0 = NativeGlfw.GLFW_KEY_KP_0,
    Kp1 = NativeGlfw.GLFW_KEY_KP_1,
    Kp2 = NativeGlfw.GLFW_KEY_KP_2,
    Kp3 = NativeGlfw.GLFW_KEY_KP_3,
    Kp4 = NativeGlfw.GLFW_KEY_KP_4,
    Kp5 = NativeGlfw.GLFW_KEY_KP_5,
    Kp6 = NativeGlfw.GLFW_KEY_KP_6,
    Kp7 = NativeGlfw.GLFW_KEY_KP_7,
    Kp8 = NativeGlfw.GLFW_KEY_KP_8,
    Kp9 = NativeGlfw.GLFW_KEY_KP_9,
    KpDecimal = NativeGlfw.GLFW_KEY_KP_DECIMAL,
    KpDivide = NativeGlfw.GLFW_KEY_KP_DIVIDE,
    KpMultiply = NativeGlfw.GLFW_KEY_KP_MULTIPLY,
    KpSubtract = NativeGlfw.GLFW_KEY_KP_SUBTRACT,
    KpAdd = NativeGlfw.GLFW_KEY_KP_ADD,
    KpEnter = NativeGlfw.GLFW_KEY_KP_ENTER,
    KpEqual = NativeGlfw.GLFW_KEY_KP_EQUAL,
    LeftShift = NativeGlfw.GLFW_KEY_LEFT_SHIFT,
    LeftControl = NativeGlfw.GLFW_KEY_LEFT_CONTROL,
    LeftAlt = NativeGlfw.GLFW_KEY_LEFT_ALT,
    LeftSuper = NativeGlfw.GLFW_KEY_LEFT_SUPER,
    RightShift = NativeGlfw.GLFW_KEY_RIGHT_SHIFT,
    RightControl = NativeGlfw.GLFW_KEY_RIGHT_CONTROL,
    RightAlt = NativeGlfw.GLFW_KEY_RIGHT_ALT,
    RightSuper = NativeGlfw.GLFW_KEY_RIGHT_SUPER,
    Menu = NativeGlfw.GLFW_KEY_MENU,
}

public enum InputState
{
    Release = NativeGlfw.GLFW_RELEASE,
    Press = NativeGlfw.GLFW_PRESS,
    Repeat = NativeGlfw.GLFW_REPEAT,
}

public enum MouseButton
{
    Button1 = NativeGlfw.GLFW_MOUSE_BUTTON_1,
    Button2 = NativeGlfw.GLFW_MOUSE_BUTTON_2,
    Button3 = NativeGlfw.GLFW_MOUSE_BUTTON_3,
    Button4 = NativeGlfw.GLFW_MOUSE_BUTTON_4,
    Button5 = NativeGlfw.GLFW_MOUSE_BUTTON_5,
    Button6 = NativeGlfw.GLFW_MOUSE_BUTTON_6,
    Button7 = NativeGlfw.GLFW_MOUSE_BUTTON_7,
    Button8 = NativeGlfw.GLFW_MOUSE_BUTTON_8,
    Left = Button1,
    Right = Button2,
    Middle = Button3,
}

[Flags]
public enum ModifierKeys
{
    Shift = NativeGlfw.GLFW_MOD_SHIFT,
    Control = NativeGlfw.GLFW_MOD_CONTROL,
    Alt = NativeGlfw.GLFW_MOD_ALT,
    Super = NativeGlfw.GLFW_MOD_SUPER,
    CapsLock = NativeGlfw.GLFW_MOD_CAPS_LOCK,
    NumLock = NativeGlfw.GLFW_MOD_NUM_LOCK,
}

#pragma warning restore 1591