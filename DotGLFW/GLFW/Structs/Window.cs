using System.Runtime.InteropServices;

namespace DotGLFW;

/// <summary>
/// An opaque handle to a GLFW window.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class Window
{
    internal readonly IntPtr _handle;

    internal Window() { }
    internal Window(IntPtr handle) => _handle = handle;

    /// <summary>
    /// A NULL window handle. Often used for default values or error handling.
    /// </summary>
    public static readonly Window NULL = new Window(IntPtr.Zero);

    /// <summary>
    /// Performs equality check against another window handle.
    /// </summary>
    public override bool Equals(object other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        if (other is Window window)
            return _handle.Equals(window._handle);

        return false;
    }

    /// <summary>
    /// Simple hash code implementation that uses the underlying pointer.
    /// </summary>
    public override int GetHashCode()
    {
        return _handle.GetHashCode();
    }

    /// <summary>
    /// Performs equality check against another window handle.
    /// </summary>
    public static bool operator ==(Window left, Window right) => left.Equals(right);

    /// <summary>
    /// Performs inequality check against another window handle.
    /// </summary>
    public static bool operator !=(Window left, Window right) => !left.Equals(right);
}