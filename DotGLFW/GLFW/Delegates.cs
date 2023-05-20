namespace DotGLFW;

public delegate void GlfwErrorCallback(ErrorCode error, string description);

public delegate void GlfwMonitorCallback(Monitor monitor, ConnectionState @event);

public delegate void GlfwWindowPosCallback(Window window, int x, int y);

public delegate void GlfwWindowSizeCallback(Window window, int width, int height);

public delegate void GlfwWindowCloseCallback(Window window);

public delegate void GlfwWindowRefreshCallback(Window window);

public delegate void GlfwWindowFocusCallback(Window window, bool focused);

public delegate void GlfwWindowIconifyCallback(Window window, bool iconified);

public delegate void GlfwWindowMaximizeCallback(Window window, bool maximized);

public delegate void GlfwFramebufferSizeCallback(Window window, int width, int height);

public delegate void GlfwWindowContentScaleCallback(Window window, float xScale, float yScale);

public delegate void GlfwKeyCallback(Window window, Keys key, int scancode, InputState action, ModifierKeys mods);

public delegate void GlfwCharCallback(Window window, uint codepoint);

public delegate void GlfwCharModsCallback(Window window, uint codepoint, ModifierKeys mods);

public delegate void GlfwMouseButtonCallback(Window window, MouseButton button, InputState action, ModifierKeys mods);

public delegate void GlfwCursorPosCallback(Window window, double x, double y);

public delegate void GlfwCursorEnterCallback(Window window, bool entered);

public delegate void GlfwScrollCallback(Window window, double xoffset, double yoffset);

public delegate void GlfwDropCallback(Window window, string[] paths);

public delegate void GlfwJoystickCallback(Joystick joystick, ConnectionState @event);