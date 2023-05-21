namespace DotGLFW;

/// <summary>
/// An enumerator for standard cursor shapes
/// </summary>
public enum CursorShape
{
    /// <summary>
    /// The regular arrow cursor shape.
    /// </summary>
    Arrow = NativeGlfw.GLFW_ARROW_CURSOR,

    /// <summary>
    /// The text input I-beam cursor shape.
    /// </summary>
    IBeam = NativeGlfw.GLFW_IBEAM_CURSOR,

    /// <summary>
    /// The crosshair shape.
    /// </summary>
    Crosshair = NativeGlfw.GLFW_CROSSHAIR_CURSOR,

    /// <summary>
    /// The hand shape.
    /// </summary>
    Hand = NativeGlfw.GLFW_HAND_CURSOR,

    /// <summary>
    /// The horizontal resize arrow shape.
    /// </summary>
    HResize = NativeGlfw.GLFW_HRESIZE_CURSOR,

    /// <summary>
    /// The vertical resize arrow shape.
    /// </summary>
    VResize = NativeGlfw.GLFW_VRESIZE_CURSOR
}