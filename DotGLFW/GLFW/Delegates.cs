namespace DotGLFW;

/// <inheritdoc cref="NativeGlfw.GLFWerrorfun" />
public delegate void GlfwErrorCallback(ErrorCode errorCode, string description);

/// <inheritdoc cref="NativeGlfw.GLFWmonitorfun" />
public delegate void GlfwMonitorCallback(Monitor monitor, ConnectionState @event);

/// <inheritdoc cref="NativeGlfw.GLFWwindowposfun" />
public delegate void GlfwWindowPosCallback(Window window, int x, int y);

/// <inheritdoc cref="NativeGlfw.GLFWwindowsizefun" />
public delegate void GlfwWindowSizeCallback(Window window, int width, int height);

/// <inheritdoc cref="NativeGlfw.GLFWwindowclosefun" />
public delegate void GlfwWindowCloseCallback(Window window);

/// <inheritdoc cref="NativeGlfw.GLFWwindowrefreshfun" />
public delegate void GlfwWindowRefreshCallback(Window window);

/// <inheritdoc cref="NativeGlfw.GLFWwindowfocusfun" />
public delegate void GlfwWindowFocusCallback(Window window, bool focused);

/// <inheritdoc cref="NativeGlfw.GLFWwindowiconifyfun" />
public delegate void GlfwWindowIconifyCallback(Window window, bool iconified);

/// <inheritdoc cref="NativeGlfw.GLFWwindowmaximizefun" />
public delegate void GlfwWindowMaximizeCallback(Window window, bool maximized);

/// <inheritdoc cref="NativeGlfw.GLFWframebuffersizefun" />
public delegate void GlfwFramebufferSizeCallback(Window window, int width, int height);

/// <inheritdoc cref="NativeGlfw.GLFWwindowcontentscalefun" />
public delegate void GlfwWindowContentScaleCallback(Window window, float xScale, float yScale);

/// <inheritdoc cref="NativeGlfw.GLFWkeyfun" />
public delegate void GlfwKeyCallback(Window window, Key key, int scancode, InputState action, ModifierKey mods);

/// <inheritdoc cref="NativeGlfw.GLFWcharfun" />
public delegate void GlfwCharCallback(Window window, uint codepoint);

/// <inheritdoc cref="NativeGlfw.GLFWcharmodsfun" />
public delegate void GlfwCharModsCallback(Window window, uint codepoint, ModifierKey mods);

/// <inheritdoc cref="NativeGlfw.GLFWmousebuttonfun" />
public delegate void GlfwMouseButtonCallback(Window window, MouseButton button, InputState action, ModifierKey mods);

/// <inheritdoc cref="NativeGlfw.GLFWcursorposfun" />
public delegate void GlfwCursorPosCallback(Window window, double x, double y);

/// <inheritdoc cref="NativeGlfw.GLFWcursorenterfun" />
public delegate void GlfwCursorEnterCallback(Window window, bool entered);

/// <inheritdoc cref="NativeGlfw.GLFWscrollfun" />
public delegate void GlfwScrollCallback(Window window, double xoffset, double yoffset);

/// <inheritdoc cref="NativeGlfw.GLFWdropfun" />
public delegate void GlfwDropCallback(Window window, string[] paths);

/// <inheritdoc cref="NativeGlfw.GLFWjoystickfun" />
public delegate void GlfwJoystickCallback(Joystick joystick, ConnectionState @event);