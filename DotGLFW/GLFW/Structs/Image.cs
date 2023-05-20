using System.Runtime.InteropServices;

namespace DotGLFW;

/// <summary>
/// Image data.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class Image
{
    private int _width;
    private int _height;
    private IntPtr _pixels;

    /// <summary>
    /// The width, in pixels, of this image.
    /// </summary>
    public int Width { get => _width; set => _width = value; }

    /// <summary>
    /// The height, in pixels, of this image.
    /// </summary>
    public int Height { get => _height; set => _height = value; }

    /// <summary>
    /// The pixel data of this image.
    /// </summary>
    public byte[] Pixels
    {
        get
        {
            byte[] pixels = new byte[_width * _height * 4];
            Marshal.Copy(_pixels, pixels, 0, pixels.Length);
            return pixels;
        }
        set
        {
            if (_pixels != IntPtr.Zero)
                Marshal.FreeHGlobal(_pixels);

            _pixels = Marshal.AllocHGlobal(value.Length);
            Marshal.Copy(value, 0, _pixels, value.Length);
        }
    }
}
