using System.Runtime.InteropServices;

namespace DotGLFW;

/// <summary>
/// Video mode type.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class VideoMode
{
    private int _width;
    private int _height;
    private int _redBits;
    private int _greenBits;
    private int _blueBits;
    private int _refreshRate;

    /// <summary>
    /// The width, in screen coordinates, of the video mode.
    /// </summary>
    public int Width => _width;

    /// <summary>
    /// The height, in screen coordinates, of the video mode.
    /// </summary>
    public int Height => _height;

    /// <summary>
    /// The bit depth of the red channel of the video mode.
    /// </summary>
    public int RedBits => _redBits;

    /// <summary>
    /// The bit depth of the green channel of the video mode.
    /// </summary>
    public int GreenBits => _greenBits;

    /// <summary>
    /// The bit depth of the blue channel of the video mode.
    /// </summary>
    public int BlueBits => _blueBits;

    /// <summary>
    /// The refresh rate, in Hz, of the video mode.
    /// </summary>
    public int RefreshRate => _refreshRate;
}