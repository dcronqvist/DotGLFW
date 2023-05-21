namespace DotGLFW;

/// <summary>
/// Delegate for handling GLFW error callbacks.
/// </summary>
/// <param name="error">The error code.</param>
/// <param name="description">The error description.</param>
public delegate void GlfwErrorCallback(ErrorCode error, string description);

/// <summary>
/// Delegate for handling GLFW monitor callbacks.
/// </summary>
/// <param name="monitor">The monitor that was connected or disconnected.</param>
/// <param name="event">The connection state.</param>
public delegate void GlfwMonitorCallback(Monitor monitor, ConnectionState @event);

/// <summary>
/// Delegate for handling GLFW window position callbacks.
/// </summary>
/// <param name="window">The window that was moved.</param>
/// <param name="x">The new x-coordinate of the window.</param>
/// <param name="y">The new y-coordinate of the window.</param>
public delegate void GlfwWindowPosCallback(Window window, int x, int y);

/// <summary>
/// Delegate for handling GLFW window size callbacks.
/// </summary>
/// <param name="window">The window that was resized.</param>
/// <param name="width">The new width of the window.</param>
/// <param name="height">The new height of the window.</param>
public delegate void GlfwWindowSizeCallback(Window window, int width, int height);

/// <summary>
/// Delegate for handling GLFW window close callbacks.
/// </summary>
/// <param name="window">The window that was closed.</param>
public delegate void GlfwWindowCloseCallback(Window window);

/// <summary>
/// Delegate for handling GLFW window refresh callbacks.
/// </summary>
/// <param name="window">The window that was refreshed.</param>
public delegate void GlfwWindowRefreshCallback(Window window);

/// <summary>
/// Delegate for handling GLFW window focus callbacks.
/// </summary>
/// <param name="window">The window that was focused or defocused.</param>
/// <param name="focused">Whether the window was focused or defocused.</param>
public delegate void GlfwWindowFocusCallback(Window window, bool focused);

/// <summary>
/// Delegate for handling GLFW window iconify callbacks.
/// </summary>
/// <param name="window">The window that was iconified or restored.</param>
/// <param name="iconified">Whether the window was iconified or restored.</param>
public delegate void GlfwWindowIconifyCallback(Window window, bool iconified);

/// <summary>
/// Delegate for handling GLFW window maximize callbacks.
/// </summary>
/// <param name="window">The window that was maximized or restored.</param>
/// <param name="maximized">Whether the window was maximized or restored.</param>
public delegate void GlfwWindowMaximizeCallback(Window window, bool maximized);

/// <summary>
/// Delegate for handling GLFW window framebuffer size callbacks.
/// </summary>
/// <param name="window">The window whose framebuffer was resized.</param>
/// <param name="width">The new width of the framebuffer.</param>
/// <param name="height">The new height of the framebuffer.</param>
public delegate void GlfwFramebufferSizeCallback(Window window, int width, int height);

/// <summary>
/// Delegate for handling GLFW window content scale callbacks.
/// </summary>
/// <param name="window">The window whose content scale was changed.</param>
/// <param name="xScale">The new x-axis content scale of the window.</param>
/// <param name="yScale">The new y-axis content scale of the window.</param>
public delegate void GlfwWindowContentScaleCallback(Window window, float xScale, float yScale);

/// <summary>
/// Delegate for handling GLFW key callbacks.
/// </summary>
/// <param name="window">The window that received the event.</param>
/// <param name="key">The keyboard key that was pressed or released.</param>
/// <param name="scancode">The system-specific scancode of the key.</param>
/// <param name="action">The key action.</param>
/// <param name="mods">Bit field describing which modifier keys were held down.</param>
public delegate void GlfwKeyCallback(Window window, Keys key, int scancode, InputState action, ModifierKeys mods);

/// <summary>
/// Delegate for handling GLFW character callbacks.
/// </summary>
/// <param name="window">The window that received the event.</param>
/// <param name="codepoint">The Unicode code point of the character.</param>
public delegate void GlfwCharCallback(Window window, uint codepoint);

/// <summary>
/// Delegate for handling GLFW character with modifiers callbacks.
/// </summary>
/// <param name="window">The window that received the event.</param>
/// <param name="codepoint">The Unicode code point of the character.</param>
/// <param name="mods">Bit field describing which modifier keys were held down.</param>
public delegate void GlfwCharModsCallback(Window window, uint codepoint, ModifierKeys mods);

/// <summary>
/// Delegate for handling GLFW mouse button callbacks.
/// </summary>
/// <param name="window">The window that received the event.</param>
/// <param name="button">The mouse button that was pressed or released.</param>
/// <param name="action">The button action.</param>
/// <param name="mods">Bit field describing which modifier keys were held down.</param>
public delegate void GlfwMouseButtonCallback(Window window, MouseButton button, InputState action, ModifierKeys mods);

/// <summary>
/// Delegate for handling GLFW cursor position callbacks.
/// </summary>
/// <param name="window">The window that received the event.</param>
/// <param name="x">The new x-coordinate of the cursor.</param>
/// <param name="y">The new y-coordinate of the cursor.</param>
public delegate void GlfwCursorPosCallback(Window window, double x, double y);

/// <summary>
/// Delegate for handling GLFW cursor enter/leave callbacks.
/// </summary>
/// <param name="window">The window that received the event.</param>
/// <param name="entered">Whether the cursor entered or left the window.</param>
public delegate void GlfwCursorEnterCallback(Window window, bool entered);

/// <summary>
/// Delegate for handling GLFW scroll callbacks.
/// </summary>
/// <param name="window">The window that received the event.</param>
/// <param name="xoffset">The scroll offset along the x-axis.</param>
/// <param name="yoffset">The scroll offset along the y-axis.</param>
public delegate void GlfwScrollCallback(Window window, double xoffset, double yoffset);

/// <summary>
/// Delegate for handling GLFW file drop callbacks.
/// </summary>
/// <param name="window">The window that received the event.</param>
/// <param name="paths">The paths of the files that were dropped.</param>
public delegate void GlfwDropCallback(Window window, string[] paths);

/// <summary>
/// Delegate for handling GLFW joystick callbacks.
/// </summary>
/// <param name="joystick">The joystick that was connected or disconnected.</param>
/// <param name="event">The connection state of the joystick.</param>
public delegate void GlfwJoystickCallback(Joystick joystick, ConnectionState @event);