namespace DotGLFW;

/// <summary>
/// Input modes for a cursor
/// </summary>
public enum CursorMode
{
    /// <summary>
    ///     Makes the cursor visible and behaving normally.
    /// </summary>
    Normal = NativeGlfw.GLFW_CURSOR_NORMAL,

    /// <summary>
    ///     Makes the cursor invisible when it is over the client area of the window but does not restrict the cursor from leaving.
    /// </summary>
    Hidden = NativeGlfw.GLFW_CURSOR_HIDDEN,

    /// <summary>
    ///     Hides and grabs the cursor, providing virtual and unlimited cursor movement. This is useful for implementing for example 3D camera controls.
    /// </summary>
    Disabled = NativeGlfw.GLFW_CURSOR_DISABLED,

    /// <summary>
    ///    Captures the cursor, makes it visible and confines it to the content area of the window.
    /// </summary>
    Captured = NativeGlfw.GLFW_CURSOR_CAPTURED
}