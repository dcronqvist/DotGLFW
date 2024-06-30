using System.Runtime.InteropServices;
using System.Security;

#pragma warning disable 1591 // Missing XML comment for publicly visible type or member, not needed for native mapper

namespace DotGLFW;

/// <summary>
/// The native GLFW library, a 1:1 mapping of the native library in C.
/// </summary>
[SuppressUnmanagedCodeSecurity]
public static class NativeGlfw
{
    internal const string LIBRARY = "glfw3";

    #region GLFW Delegate Types

    internal static NativeGlfwErrorCallback _currentNativeGlfwErrorCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwErrorCallback(int errorCode, IntPtr description);

    internal static NativeGlfwMonitorCallback _currentNativeGlfwMonitorCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwMonitorCallback(IntPtr monitor, int @event);

    internal static NativeGlfwWindowPosCallback _currentNativeGlfwWindowPosCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwWindowPosCallback(IntPtr window, int x, int y);

    internal static NativeGlfwWindowSizeCallback _currentNativeGlfwWindowSizeCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwWindowSizeCallback(IntPtr window, int width, int height);

    internal static NativeGlfwWindowCloseCallback _currentNativeGlfwWindowCloseCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwWindowCloseCallback(IntPtr window);

    internal static NativeGlfwWindowRefreshCallback _currentNativeGlfwWindowRefreshCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwWindowRefreshCallback(IntPtr window);

    internal static NativeGlfwWindowFocusCallback _currentNativeGlfwWindowFocusCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwWindowFocusCallback(IntPtr window, int focused);

    internal static NativeGlfwWindowIconifyCallback _currentNativeGlfwWindowIconifyCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwWindowIconifyCallback(IntPtr window, int iconified);

    internal static NativeGlfwWindowMaximizeCallback _currentNativeGlfwWindowMaximizeCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwWindowMaximizeCallback(IntPtr window, int maximized);

    internal static NativeGlfwFramebufferSizeCallback _currentNativeGlfwFramebufferSizeCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwFramebufferSizeCallback(IntPtr window, int width, int height);

    internal static NativeGlfwWindowContentScaleCallback _currentNativeGlfwWindowContentScaleCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwWindowContentScaleCallback(IntPtr window, float xscale, float yscale);

    internal static NativeGlfwKeyCallback _currentNativeGlfwKeyCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwKeyCallback(IntPtr window, int key, int scancode, int action, int mods);

    internal static NativeGlfwCharCallback _currentNativeGlfwCharCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwCharCallback(IntPtr window, uint codepoint);

    internal static NativeGlfwCharModsCallback _currentNativeGlfwCharModsCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwCharModsCallback(IntPtr window, uint codepoint, int mods);

    internal static NativeGlfwMouseButtonCallback _currentNativeGlfwMouseButtonCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwMouseButtonCallback(IntPtr window, int button, int action, int mods);

    internal static NativeGlfwCursorPosCallback _currentNativeGlfwCursorPosCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwCursorPosCallback(IntPtr window, double xpos, double ypos);

    internal static NativeGlfwCursorEnterCallback _currentNativeGlfwCursorEnterCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwCursorEnterCallback(IntPtr window, int entered);

    internal static NativeGlfwScrollCallback _currentNativeGlfwScrollCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwScrollCallback(IntPtr window, double xoffset, double yoffset);

    internal static NativeGlfwDropCallback _currentNativeGlfwDropCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwDropCallback(IntPtr window, int count, IntPtr paths);

    internal static NativeGlfwJoystickCallback _currentNativeGlfwJoystickCallback;
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NativeGlfwJoystickCallback(int jid, int @event);

    #endregion

    #region GLFW Constants

    public const int GLFW_TRUE = 1;
    public const int GLFW_FALSE = 0;

    public const int GLFW_HAT_CENTERED = 0;
    public const int GLFW_HAT_UP = 1;
    public const int GLFW_HAT_RIGHT = 2;
    public const int GLFW_HAT_DOWN = 4;
    public const int GLFW_HAT_LEFT = 8;
    public const int GLFW_HAT_RIGHT_UP = GLFW_HAT_RIGHT | GLFW_HAT_UP;
    public const int GLFW_HAT_RIGHT_DOWN = GLFW_HAT_RIGHT | GLFW_HAT_DOWN;
    public const int GLFW_HAT_LEFT_UP = GLFW_HAT_LEFT | GLFW_HAT_UP;
    public const int GLFW_HAT_LEFT_DOWN = GLFW_HAT_LEFT | GLFW_HAT_DOWN;

    public const int GLFW_KEY_UNKNOWN = -1;
    public const int GLFW_KEY_SPACE = 32;
    public const int GLFW_KEY_APOSTROPHE = 39; /* ' */
    public const int GLFW_KEY_COMMA = 44; /* , */
    public const int GLFW_KEY_MINUS = 45; /* - */
    public const int GLFW_KEY_PERIOD = 46; /* . */
    public const int GLFW_KEY_SLASH = 47; /* / */
    public const int GLFW_KEY_0 = 48;
    public const int GLFW_KEY_1 = 49;
    public const int GLFW_KEY_2 = 50;
    public const int GLFW_KEY_3 = 51;
    public const int GLFW_KEY_4 = 52;
    public const int GLFW_KEY_5 = 53;
    public const int GLFW_KEY_6 = 54;
    public const int GLFW_KEY_7 = 55;
    public const int GLFW_KEY_8 = 56;
    public const int GLFW_KEY_9 = 57;
    public const int GLFW_KEY_SEMICOLON = 59; /* ; */
    public const int GLFW_KEY_EQUAL = 61; /* = */
    public const int GLFW_KEY_A = 65;
    public const int GLFW_KEY_B = 66;
    public const int GLFW_KEY_C = 67;
    public const int GLFW_KEY_D = 68;
    public const int GLFW_KEY_E = 69;
    public const int GLFW_KEY_F = 70;
    public const int GLFW_KEY_G = 71;
    public const int GLFW_KEY_H = 72;
    public const int GLFW_KEY_I = 73;
    public const int GLFW_KEY_J = 74;
    public const int GLFW_KEY_K = 75;
    public const int GLFW_KEY_L = 76;
    public const int GLFW_KEY_M = 77;
    public const int GLFW_KEY_N = 78;
    public const int GLFW_KEY_O = 79;
    public const int GLFW_KEY_P = 80;
    public const int GLFW_KEY_Q = 81;
    public const int GLFW_KEY_R = 82;
    public const int GLFW_KEY_S = 83;
    public const int GLFW_KEY_T = 84;
    public const int GLFW_KEY_U = 85;
    public const int GLFW_KEY_V = 86;
    public const int GLFW_KEY_W = 87;
    public const int GLFW_KEY_X = 88;
    public const int GLFW_KEY_Y = 89;
    public const int GLFW_KEY_Z = 90;
    public const int GLFW_KEY_LEFT_BRACKET = 91; /* [ */
    public const int GLFW_KEY_BACKSLASH = 92; /* \ */
    public const int GLFW_KEY_RIGHT_BRACKET = 93; /* ] */
    public const int GLFW_KEY_GRAVE_ACCENT = 96; /* ` */
    public const int GLFW_KEY_WORLD_1 = 161; /* non-US #1 */
    public const int GLFW_KEY_WORLD_2 = 162; /* non-US #2 */
    public const int GLFW_KEY_ESCAPE = 256;
    public const int GLFW_KEY_ENTER = 257;
    public const int GLFW_KEY_TAB = 258;
    public const int GLFW_KEY_BACKSPACE = 259;
    public const int GLFW_KEY_INSERT = 260;
    public const int GLFW_KEY_DELETE = 261;
    public const int GLFW_KEY_RIGHT = 262;
    public const int GLFW_KEY_LEFT = 263;
    public const int GLFW_KEY_DOWN = 264;
    public const int GLFW_KEY_UP = 265;
    public const int GLFW_KEY_PAGE_UP = 266;
    public const int GLFW_KEY_PAGE_DOWN = 267;
    public const int GLFW_KEY_HOME = 268;
    public const int GLFW_KEY_END = 269;
    public const int GLFW_KEY_CAPS_LOCK = 280;
    public const int GLFW_KEY_SCROLL_LOCK = 281;
    public const int GLFW_KEY_NUM_LOCK = 282;
    public const int GLFW_KEY_PRINT_SCREEN = 283;
    public const int GLFW_KEY_PAUSE = 284;
    public const int GLFW_KEY_F1 = 290;
    public const int GLFW_KEY_F2 = 291;
    public const int GLFW_KEY_F3 = 292;
    public const int GLFW_KEY_F4 = 293;
    public const int GLFW_KEY_F5 = 294;
    public const int GLFW_KEY_F6 = 295;
    public const int GLFW_KEY_F7 = 296;
    public const int GLFW_KEY_F8 = 297;
    public const int GLFW_KEY_F9 = 298;
    public const int GLFW_KEY_F10 = 299;
    public const int GLFW_KEY_F11 = 300;
    public const int GLFW_KEY_F12 = 301;
    public const int GLFW_KEY_F13 = 302;
    public const int GLFW_KEY_F14 = 303;
    public const int GLFW_KEY_F15 = 304;
    public const int GLFW_KEY_F16 = 305;
    public const int GLFW_KEY_F17 = 306;
    public const int GLFW_KEY_F18 = 307;
    public const int GLFW_KEY_F19 = 308;
    public const int GLFW_KEY_F20 = 309;
    public const int GLFW_KEY_F21 = 310;
    public const int GLFW_KEY_F22 = 311;
    public const int GLFW_KEY_F23 = 312;
    public const int GLFW_KEY_F24 = 313;
    public const int GLFW_KEY_F25 = 314;
    public const int GLFW_KEY_KP_0 = 320;
    public const int GLFW_KEY_KP_1 = 321;
    public const int GLFW_KEY_KP_2 = 322;
    public const int GLFW_KEY_KP_3 = 323;
    public const int GLFW_KEY_KP_4 = 324;
    public const int GLFW_KEY_KP_5 = 325;
    public const int GLFW_KEY_KP_6 = 326;
    public const int GLFW_KEY_KP_7 = 327;
    public const int GLFW_KEY_KP_8 = 328;
    public const int GLFW_KEY_KP_9 = 329;
    public const int GLFW_KEY_KP_DECIMAL = 330;
    public const int GLFW_KEY_KP_DIVIDE = 331;
    public const int GLFW_KEY_KP_MULTIPLY = 332;
    public const int GLFW_KEY_KP_SUBTRACT = 333;
    public const int GLFW_KEY_KP_ADD = 334;
    public const int GLFW_KEY_KP_ENTER = 335;
    public const int GLFW_KEY_KP_EQUAL = 336;
    public const int GLFW_KEY_LEFT_SHIFT = 340;
    public const int GLFW_KEY_LEFT_CONTROL = 341;
    public const int GLFW_KEY_LEFT_ALT = 342;
    public const int GLFW_KEY_LEFT_SUPER = 343;
    public const int GLFW_KEY_RIGHT_SHIFT = 344;
    public const int GLFW_KEY_RIGHT_CONTROL = 345;
    public const int GLFW_KEY_RIGHT_ALT = 346;
    public const int GLFW_KEY_RIGHT_SUPER = 347;
    public const int GLFW_KEY_MENU = 348;
    public const int GLFW_KEY_LAST = GLFW_KEY_MENU;

    public const int GLFW_MOD_SHIFT = 0x0001;
    public const int GLFW_MOD_CONTROL = 0x0002;
    public const int GLFW_MOD_ALT = 0x0004;
    public const int GLFW_MOD_SUPER = 0x0008;
    public const int GLFW_MOD_CAPS_LOCK = 0x0010;
    public const int GLFW_MOD_NUM_LOCK = 0x0020;

    public const int GLFW_MOUSE_BUTTON_1 = 0;
    public const int GLFW_MOUSE_BUTTON_2 = 1;
    public const int GLFW_MOUSE_BUTTON_3 = 2;
    public const int GLFW_MOUSE_BUTTON_4 = 3;
    public const int GLFW_MOUSE_BUTTON_5 = 4;
    public const int GLFW_MOUSE_BUTTON_6 = 5;
    public const int GLFW_MOUSE_BUTTON_7 = 6;
    public const int GLFW_MOUSE_BUTTON_8 = 7;
    public const int GLFW_MOUSE_BUTTON_LAST = GLFW_MOUSE_BUTTON_8;
    public const int GLFW_MOUSE_BUTTON_LEFT = GLFW_MOUSE_BUTTON_1;
    public const int GLFW_MOUSE_BUTTON_RIGHT = GLFW_MOUSE_BUTTON_2;
    public const int GLFW_MOUSE_BUTTON_MIDDLE = GLFW_MOUSE_BUTTON_3;

    public const int GLFW_JOYSTICK_1 = 0;
    public const int GLFW_JOYSTICK_2 = 1;
    public const int GLFW_JOYSTICK_3 = 2;
    public const int GLFW_JOYSTICK_4 = 3;
    public const int GLFW_JOYSTICK_5 = 4;
    public const int GLFW_JOYSTICK_6 = 5;
    public const int GLFW_JOYSTICK_7 = 6;
    public const int GLFW_JOYSTICK_8 = 7;
    public const int GLFW_JOYSTICK_9 = 8;
    public const int GLFW_JOYSTICK_10 = 9;
    public const int GLFW_JOYSTICK_11 = 10;
    public const int GLFW_JOYSTICK_12 = 11;
    public const int GLFW_JOYSTICK_13 = 12;
    public const int GLFW_JOYSTICK_14 = 13;
    public const int GLFW_JOYSTICK_15 = 14;
    public const int GLFW_JOYSTICK_16 = 15;
    public const int GLFW_JOYSTICK_LAST = GLFW_JOYSTICK_16;

    public const int GLFW_GAMEPAD_BUTTON_A = 0;
    public const int GLFW_GAMEPAD_BUTTON_B = 1;
    public const int GLFW_GAMEPAD_BUTTON_X = 2;
    public const int GLFW_GAMEPAD_BUTTON_Y = 3;
    public const int GLFW_GAMEPAD_BUTTON_LEFT_BUMPER = 4;
    public const int GLFW_GAMEPAD_BUTTON_RIGHT_BUMPER = 5;
    public const int GLFW_GAMEPAD_BUTTON_BACK = 6;
    public const int GLFW_GAMEPAD_BUTTON_START = 7;
    public const int GLFW_GAMEPAD_BUTTON_GUIDE = 8;
    public const int GLFW_GAMEPAD_BUTTON_LEFT_THUMB = 9;
    public const int GLFW_GAMEPAD_BUTTON_RIGHT_THUMB = 10;
    public const int GLFW_GAMEPAD_BUTTON_DPAD_UP = 11;
    public const int GLFW_GAMEPAD_BUTTON_DPAD_RIGHT = 12;
    public const int GLFW_GAMEPAD_BUTTON_DPAD_DOWN = 13;
    public const int GLFW_GAMEPAD_BUTTON_DPAD_LEFT = 14;
    public const int GLFW_GAMEPAD_BUTTON_LAST = GLFW_GAMEPAD_BUTTON_DPAD_LEFT;
    public const int GLFW_GAMEPAD_BUTTON_CROSS = GLFW_GAMEPAD_BUTTON_A;
    public const int GLFW_GAMEPAD_BUTTON_CIRCLE = GLFW_GAMEPAD_BUTTON_B;
    public const int GLFW_GAMEPAD_BUTTON_SQUARE = GLFW_GAMEPAD_BUTTON_X;
    public const int GLFW_GAMEPAD_BUTTON_TRIANGLE = GLFW_GAMEPAD_BUTTON_Y;
    public const int GLFW_GAMEPAD_AXIS_LEFT_X = 0;
    public const int GLFW_GAMEPAD_AXIS_LEFT_Y = 1;
    public const int GLFW_GAMEPAD_AXIS_RIGHT_X = 2;
    public const int GLFW_GAMEPAD_AXIS_RIGHT_Y = 3;
    public const int GLFW_GAMEPAD_AXIS_LEFT_TRIGGER = 4;
    public const int GLFW_GAMEPAD_AXIS_RIGHT_TRIGGER = 5;
    public const int GLFW_GAMEPAD_AXIS_LAST = GLFW_GAMEPAD_AXIS_RIGHT_TRIGGER;

    public const int GLFW_NO_ERROR = 0;
    public const int GLFW_NOT_INITIALIZED = 0x00010001;
    public const int GLFW_NO_CURRENT_CONTEXT = 0x00010002;
    public const int GLFW_INVALID_ENUM = 0x00010003;
    public const int GLFW_INVALID_VALUE = 0x00010004;
    public const int GLFW_OUT_OF_MEMORY = 0x00010005;
    public const int GLFW_API_UNAVAILABLE = 0x00010006;
    public const int GLFW_VERSION_UNAVAILABLE = 0x00010007;
    public const int GLFW_PLATFORM_ERROR = 0x00010008;
    public const int GLFW_FORMAT_UNAVAILABLE = 0x00010009;
    public const int GLFW_NO_WINDOW_CONTEXT = 0x0001000A;

    public const int GLFW_FOCUSED = 0x00020001;
    public const int GLFW_ICONIFIED = 0x00020002;
    public const int GLFW_RESIZABLE = 0x00020003;
    public const int GLFW_VISIBLE = 0x00020004;
    public const int GLFW_DECORATED = 0x00020005;
    public const int GLFW_AUTO_ICONIFY = 0x00020006;
    public const int GLFW_FLOATING = 0x00020007;
    public const int GLFW_MAXIMIZED = 0x00020008;
    public const int GLFW_CENTER_CURSOR = 0x00020009;
    public const int GLFW_TRANSPARENT_FRAMEBUFFER = 0x0002000A;
    public const int GLFW_HOVERED = 0x0002000B;
    public const int GLFW_FOCUS_ON_SHOW = 0x0002000C;

    public const int GLFW_RED_BITS = 0x00021001;
    public const int GLFW_GREEN_BITS = 0x00021002;
    public const int GLFW_BLUE_BITS = 0x00021003;
    public const int GLFW_ALPHA_BITS = 0x00021004;
    public const int GLFW_DEPTH_BITS = 0x00021005;
    public const int GLFW_STENCIL_BITS = 0x00021006;
    public const int GLFW_ACCUM_RED_BITS = 0x00021007;
    public const int GLFW_ACCUM_GREEN_BITS = 0x00021008;
    public const int GLFW_ACCUM_BLUE_BITS = 0x00021009;
    public const int GLFW_ACCUM_ALPHA_BITS = 0x0002100A;
    public const int GLFW_AUX_BUFFERS = 0x0002100B;
    public const int GLFW_STEREO = 0x0002100C;
    public const int GLFW_SAMPLES = 0x0002100D;
    public const int GLFW_SRGB_CAPABLE = 0x0002100E;
    public const int GLFW_REFRESH_RATE = 0x0002100F;
    public const int GLFW_DOUBLEBUFFER = 0x00021010;

    public const int GLFW_CLIENT_API = 0x00022001;
    public const int GLFW_CONTEXT_VERSION_MAJOR = 0x00022002;
    public const int GLFW_CONTEXT_VERSION_MINOR = 0x00022003;
    public const int GLFW_CONTEXT_REVISION = 0x00022004;
    public const int GLFW_CONTEXT_ROBUSTNESS = 0x00022005;
    public const int GLFW_OPENGL_FORWARD_COMPAT = 0x00022006;
    public const int GLFW_OPENGL_DEBUG_CONTEXT = 0x00022007;
    public const int GLFW_OPENGL_PROFILE = 0x00022008;
    public const int GLFW_CONTEXT_RELEASE_BEHAVIOR = 0x00022009;
    public const int GLFW_CONTEXT_NO_ERROR = 0x0002200A;
    public const int GLFW_CONTEXT_CREATION_API = 0x0002200B;
    public const int GLFW_SCALE_TO_MONITOR = 0x0002200C;

    public const int GLFW_COCOA_RETINA_FRAMEBUFFER = 0x00023001;
    public const int GLFW_COCOA_FRAME_NAME = 0x00023002;
    public const int GLFW_COCOA_GRAPHICS_SWITCHING = 0x00023003;

    public const int GLFW_X11_CLASS_NAME = 0x00024001;
    public const int GLFW_X11_INSTANCE_NAME = 0x00024002;

    public const int GLFW_NO_API = 0;
    public const int GLFW_OPENGL_API = 0x00030001;
    public const int GLFW_OPENGL_ES_API = 0x00030002;
    public const int GLFW_NO_ROBUSTNESS = 0;
    public const int GLFW_NO_RESET_NOTIFICATION = 0x00031001;
    public const int GLFW_LOSE_CONTEXT_ON_RESET = 0x00031002;
    public const int GLFW_OPENGL_ANY_PROFILE = 0;
    public const int GLFW_OPENGL_CORE_PROFILE = 0x00032001;
    public const int GLFW_OPENGL_COMPAT_PROFILE = 0x00032002;
    public const int GLFW_CURSOR = 0x00033001;
    public const int GLFW_STICKY_KEYS = 0x00033002;
    public const int GLFW_STICKY_MOUSE_BUTTONS = 0x00033003;
    public const int GLFW_LOCK_KEY_MODS = 0x00033004;
    public const int GLFW_RAW_MOUSE_MOTION = 0x00033005;
    public const int GLFW_CURSOR_NORMAL = 0x00034001;
    public const int GLFW_CURSOR_HIDDEN = 0x00034002;
    public const int GLFW_CURSOR_DISABLED = 0x00034003;
    public const int GLFW_ANY_RELEASE_BEHAVIOR = 0;
    public const int GLFW_RELEASE_BEHAVIOR_FLUSH = 0x00035001;
    public const int GLFW_RELEASE_BEHAVIOR_NONE = 0x00035002;
    public const int GLFW_NATIVE_CONTEXT_API = 0x00036001;
    public const int GLFW_EGL_CONTEXT_API = 0x00036002;
    public const int GLFW_OSMESA_CONTEXT_API = 0x00036003;
    public const int GLFW_ARROW_CURSOR = 0x00036001;
    public const int GLFW_IBEAM_CURSOR = 0x00036002;
    public const int GLFW_CROSSHAIR_CURSOR = 0x00036003;
    public const int GLFW_HAND_CURSOR = 0x00036004;
    public const int GLFW_HRESIZE_CURSOR = 0x00036005;
    public const int GLFW_VRESIZE_CURSOR = 0x00036006;
    public const int GLFW_CONNECTED = 0x00040001;
    public const int GLFW_DISCONNECTED = 0x00040002;
    public const int GLFW_JOYSTICK_HAT_BUTTONS = 0x00050001;
    public const int GLFW_COCOA_CHDIR_RESOURCES = 0x00051001;
    public const int GLFW_COCOA_MENUBAR = 0x00051002;
    public const int GLFW_DONT_CARE = -1;

    public const int GLFW_RELEASE = 0;
    public const int GLFW_PRESS = 1;
    public const int GLFW_REPEAT = 2;

    #endregion

    #region GLFW Functions

    [DllImport(LIBRARY, EntryPoint = "glfwInit", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Init();

    [DllImport(LIBRARY, EntryPoint = "glfwTerminate", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Terminate();

    [DllImport(LIBRARY, EntryPoint = "glfwInitHint", CallingConvention = CallingConvention.Cdecl)]
    public static extern void InitHint(int hint, int value);

    [DllImport(LIBRARY, EntryPoint = "glfwGetVersion", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetVersion(out int major, out int minor, out int rev);

    [DllImport(LIBRARY, EntryPoint = "glfwGetVersionString", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetVersionString();

    [DllImport(LIBRARY, EntryPoint = "glfwGetError", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetError(out IntPtr description);

    [DllImport(LIBRARY, EntryPoint = "glfwSetErrorCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwErrorCallback SetErrorCallback(NativeGlfwErrorCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitors", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetMonitors(out int count);

    [DllImport(LIBRARY, EntryPoint = "glfwGetPrimaryMonitor", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetPrimaryMonitor();

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorPos", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetMonitorPos(IntPtr monitor, out int xpos, out int ypos);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorWorkarea", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetMonitorWorkarea(IntPtr monitor, out int xpos, out int ypos, out int width, out int height);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorPhysicalSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetMonitorPhysicalSize(IntPtr monitor, out int widthMM, out int heightMM);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorContentScale", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetMonitorContentScale(IntPtr monitor, out float xscale, out float yscale);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorName", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetMonitorName(IntPtr monitor);

    [DllImport(LIBRARY, EntryPoint = "glfwSetMonitorUserPointer", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetMonitorUserPointer(IntPtr monitor, IntPtr pointer);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMonitorUserPointer", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetMonitorUserPointer(IntPtr monitor);

    [DllImport(LIBRARY, EntryPoint = "glfwSetMonitorCallback", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(NativeGlfwMonitorCallback))]
    public static extern NativeGlfwMonitorCallback SetMonitorCallback(NativeGlfwMonitorCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwGetVideoModes", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetVideoModes(IntPtr monitor, out int count);

    [DllImport(LIBRARY, EntryPoint = "glfwGetVideoMode", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetVideoMode(IntPtr monitor);

    [DllImport(LIBRARY, EntryPoint = "glfwSetGamma", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetGamma(IntPtr monitor, float gamma);

    [DllImport(LIBRARY, EntryPoint = "glfwGetGammaRamp", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetGammaRamp(IntPtr monitor);

    [DllImport(LIBRARY, EntryPoint = "glfwSetGammaRamp", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetGammaRamp(IntPtr monitor, IntPtr ramp);

    [DllImport(LIBRARY, EntryPoint = "glfwDefaultWindowHints", CallingConvention = CallingConvention.Cdecl)]
    public static extern void DefaultWindowHints();

    [DllImport(LIBRARY, EntryPoint = "glfwWindowHint", CallingConvention = CallingConvention.Cdecl)]
    public static extern void WindowHint(int hint, int value);

    [DllImport(LIBRARY, EntryPoint = "glfwWindowHintString", CallingConvention = CallingConvention.Cdecl)]
    public static extern void WindowHintString(int hint, string value);

    [DllImport(LIBRARY, EntryPoint = "glfwCreateWindow", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr CreateWindow(int width, int height, string title, IntPtr monitor, IntPtr share);

    [DllImport(LIBRARY, EntryPoint = "glfwDestroyWindow", CallingConvention = CallingConvention.Cdecl)]
    public static extern void DestroyWindow(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwWindowShouldClose", CallingConvention = CallingConvention.Cdecl)]
    public static extern int WindowShouldClose(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowShouldClose", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowShouldClose(IntPtr window, int value);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowTitle", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowTitle(IntPtr window, string title);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowIcon", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowIcon(IntPtr window, int count, IntPtr images);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowPos", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetWindowPos(IntPtr window, out int xpos, out int ypos);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowPos", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowPos(IntPtr window, int xpos, int ypos);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetWindowSize(IntPtr window, out int width, out int height);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowSizeLimits", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowSizeLimits(IntPtr window, int minwidth, int minheight, int maxwidth, int maxheight);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowAspectRatio", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowAspectRatio(IntPtr window, int numer, int denom);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowSize(IntPtr window, int width, int height);

    [DllImport(LIBRARY, EntryPoint = "glfwGetFramebufferSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetFramebufferSize(IntPtr window, out int width, out int height);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowFrameSize", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetWindowFrameSize(IntPtr window, out int left, out int top, out int right, out int bottom);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowContentScale", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetWindowContentScale(IntPtr window, out float xscale, out float yscale);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowOpacity", CallingConvention = CallingConvention.Cdecl)]
    public static extern float GetWindowOpacity(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowOpacity", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowOpacity(IntPtr window, float opacity);

    [DllImport(LIBRARY, EntryPoint = "glfwIconifyWindow", CallingConvention = CallingConvention.Cdecl)]
    public static extern void IconifyWindow(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwRestoreWindow", CallingConvention = CallingConvention.Cdecl)]
    public static extern void RestoreWindow(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwMaximizeWindow", CallingConvention = CallingConvention.Cdecl)]
    public static extern void MaximizeWindow(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwShowWindow", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ShowWindow(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwHideWindow", CallingConvention = CallingConvention.Cdecl)]
    public static extern void HideWindow(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwFocusWindow", CallingConvention = CallingConvention.Cdecl)]
    public static extern void FocusWindow(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwRequestWindowAttention", CallingConvention = CallingConvention.Cdecl)]
    public static extern void RequestWindowAttention(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowMonitor", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetWindowMonitor(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowMonitor", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowMonitor(IntPtr window, IntPtr monitor, int xpos, int ypos, int width, int height, int refreshRate);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowAttrib", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetWindowAttrib(IntPtr window, int attrib);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowAttrib", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowAttrib(IntPtr window, int attrib, int value);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowUserPointer", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetWindowUserPointer(IntPtr window, IntPtr pointer);

    [DllImport(LIBRARY, EntryPoint = "glfwGetWindowUserPointer", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetWindowUserPointer(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowPosCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwWindowPosCallback SetWindowPosCallback(IntPtr window, NativeGlfwWindowPosCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowSizeCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwWindowSizeCallback SetWindowSizeCallback(IntPtr window, NativeGlfwWindowSizeCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowCloseCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwWindowCloseCallback SetWindowCloseCallback(IntPtr window, NativeGlfwWindowCloseCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowRefreshCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwWindowRefreshCallback SetWindowRefreshCallback(IntPtr window, NativeGlfwWindowRefreshCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowFocusCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwWindowFocusCallback SetWindowFocusCallback(IntPtr window, NativeGlfwWindowFocusCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowIconifyCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwWindowIconifyCallback SetWindowIconifyCallback(IntPtr window, NativeGlfwWindowIconifyCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowMaximizeCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwWindowMaximizeCallback SetWindowMaximizeCallback(IntPtr window, NativeGlfwWindowMaximizeCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetFramebufferSizeCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwFramebufferSizeCallback SetFramebufferSizeCallback(IntPtr window, NativeGlfwFramebufferSizeCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetWindowContentScaleCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwWindowContentScaleCallback SetWindowContentScaleCallback(IntPtr window, NativeGlfwWindowContentScaleCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwPollEvents", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PollEvents();

    [DllImport(LIBRARY, EntryPoint = "glfwWaitEvents", CallingConvention = CallingConvention.Cdecl)]
    public static extern void WaitEvents();

    [DllImport(LIBRARY, EntryPoint = "glfwWaitEventsTimeout", CallingConvention = CallingConvention.Cdecl)]
    public static extern void WaitEventsTimeout(double timeout);

    [DllImport(LIBRARY, EntryPoint = "glfwPostEmptyEvent", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PostEmptyEvent();

    [DllImport(LIBRARY, EntryPoint = "glfwGetInputMode", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetInputMode(IntPtr window, int mode);

    [DllImport(LIBRARY, EntryPoint = "glfwSetInputMode", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetInputMode(IntPtr window, int mode, int value);

    [DllImport(LIBRARY, EntryPoint = "glfwRawMouseMotionSupported", CallingConvention = CallingConvention.Cdecl)]
    public static extern int RawMouseMotionSupported();

    [DllImport(LIBRARY, EntryPoint = "glfwGetKeyName", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetKeyName(int key, int scancode);

    [DllImport(LIBRARY, EntryPoint = "glfwGetKeyScancode", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetKeyScancode(int key);

    [DllImport(LIBRARY, EntryPoint = "glfwGetKey", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetKey(IntPtr window, int key);

    [DllImport(LIBRARY, EntryPoint = "glfwGetMouseButton", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetMouseButton(IntPtr window, int button);

    [DllImport(LIBRARY, EntryPoint = "glfwGetCursorPos", CallingConvention = CallingConvention.Cdecl)]
    public static extern void GetCursorPos(IntPtr window, out double xpos, out double ypos);

    [DllImport(LIBRARY, EntryPoint = "glfwSetCursorPos", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetCursorPos(IntPtr window, double xpos, double ypos);

    [DllImport(LIBRARY, EntryPoint = "glfwCreateCursor", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr CreateCursor(IntPtr image, int xhot, int yhot);

    [DllImport(LIBRARY, EntryPoint = "glfwCreateStandardCursor", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr CreateStandardCursor(int shape);

    [DllImport(LIBRARY, EntryPoint = "glfwDestroyCursor", CallingConvention = CallingConvention.Cdecl)]
    public static extern void DestroyCursor(IntPtr cursor);

    [DllImport(LIBRARY, EntryPoint = "glfwSetCursor", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetCursor(IntPtr window, IntPtr cursor);

    [DllImport(LIBRARY, EntryPoint = "glfwSetKeyCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwKeyCallback SetKeyCallback(IntPtr window, NativeGlfwKeyCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetCharCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwCharCallback SetCharCallback(IntPtr window, NativeGlfwCharCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetCharModsCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwCharModsCallback SetCharModsCallback(IntPtr window, NativeGlfwCharModsCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetMouseButtonCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwMouseButtonCallback SetMouseButtonCallback(IntPtr window, NativeGlfwMouseButtonCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetCursorPosCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwCursorPosCallback SetCursorPosCallback(IntPtr window, NativeGlfwCursorPosCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetCursorEnterCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwCursorEnterCallback SetCursorEnterCallback(IntPtr window, NativeGlfwCursorEnterCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetScrollCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwScrollCallback SetScrollCallback(IntPtr window, NativeGlfwScrollCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwSetDropCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwDropCallback SetDropCallback(IntPtr window, NativeGlfwDropCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwJoystickPresent", CallingConvention = CallingConvention.Cdecl)]
    public static extern int JoystickPresent(int jid);

    [DllImport(LIBRARY, EntryPoint = "glfwGetJoystickAxes", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetJoystickAxes(int jid, out int count);

    [DllImport(LIBRARY, EntryPoint = "glfwGetJoystickButtons", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetJoystickButtons(int jid, out int count);

    [DllImport(LIBRARY, EntryPoint = "glfwGetJoystickHats", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetJoystickHats(int jid, out int count);

    [DllImport(LIBRARY, EntryPoint = "glfwGetJoystickName", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetJoystickName(int jid);

    [DllImport(LIBRARY, EntryPoint = "glfwGetJoystickGUID", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetJoystickGUID(int jid);

    [DllImport(LIBRARY, EntryPoint = "glfwSetJoystickUserPointer", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetJoystickUserPointer(int jid, IntPtr pointer);

    [DllImport(LIBRARY, EntryPoint = "glfwGetJoystickUserPointer", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetJoystickUserPointer(int jid);

    [DllImport(LIBRARY, EntryPoint = "glfwJoystickIsGamepad", CallingConvention = CallingConvention.Cdecl)]
    public static extern int JoystickIsGamepad(int jid);

    [DllImport(LIBRARY, EntryPoint = "glfwSetJoystickCallback", CallingConvention = CallingConvention.Cdecl)]
    public static extern NativeGlfwJoystickCallback SetJoystickCallback(NativeGlfwJoystickCallback callback);

    [DllImport(LIBRARY, EntryPoint = "glfwUpdateGamepadMappings", CallingConvention = CallingConvention.Cdecl)]
    public static extern int UpdateGamepadMappings(string mapping);

    [DllImport(LIBRARY, EntryPoint = "glfwGetGamepadName", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetGamepadName(int jid);

    [DllImport(LIBRARY, EntryPoint = "glfwGetGamepadState", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetGamepadState(int jid, IntPtr state);

    [DllImport(LIBRARY, EntryPoint = "glfwSetClipboardString", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetClipboardString(IntPtr window, IntPtr @string);

    [DllImport(LIBRARY, EntryPoint = "glfwGetClipboardString", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetClipboardString(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwGetTime", CallingConvention = CallingConvention.Cdecl)]
    public static extern double GetTime();

    [DllImport(LIBRARY, EntryPoint = "glfwSetTime", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SetTime(double time);

    [DllImport(LIBRARY, EntryPoint = "glfwGetTimerValue", CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong GetTimerValue();

    [DllImport(LIBRARY, EntryPoint = "glfwGetTimerFrequency", CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong GetTimerFrequency();

    [DllImport(LIBRARY, EntryPoint = "glfwMakeContextCurrent", CallingConvention = CallingConvention.Cdecl)]
    public static extern void MakeContextCurrent(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwGetCurrentContext", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetCurrentContext();

    [DllImport(LIBRARY, EntryPoint = "glfwSwapBuffers", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SwapBuffers(IntPtr window);

    [DllImport(LIBRARY, EntryPoint = "glfwSwapInterval", CallingConvention = CallingConvention.Cdecl)]
    public static extern void SwapInterval(int interval);

    [DllImport(LIBRARY, EntryPoint = "glfwExtensionSupported", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ExtensionSupported(string extension);

    [DllImport(LIBRARY, EntryPoint = "glfwGetProcAddress", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetProcAddress(string procname);

    [DllImport(LIBRARY, EntryPoint = "glfwVulkanSupported", CallingConvention = CallingConvention.Cdecl)]
    public static extern int VulkanSupported();

    [DllImport(LIBRARY, EntryPoint = "glfwGetRequiredInstanceExtensions", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetRequiredInstanceExtensions(out uint count);

    [DllImport(LIBRARY, EntryPoint = "glfwGetInstanceProcAddress", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetInstanceProcAddress(IntPtr instance, string procname);

    [DllImport(LIBRARY, EntryPoint = "glfwGetPhysicalDevicePresentationSupport", CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetPhysicalDevicePresentationSupport(IntPtr instance, IntPtr device, uint queuefamily);

    [DllImport(LIBRARY, EntryPoint = "glfwCreateWindowSurface", CallingConvention = CallingConvention.Cdecl)]
    public static extern int CreateWindowSurface(IntPtr instance, IntPtr window, IntPtr allocator, out IntPtr surface);

    #endregion
}

#pragma warning restore 1591 // Missing XML comment for publicly visible type or member