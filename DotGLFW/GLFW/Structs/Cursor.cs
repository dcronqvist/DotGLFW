using System.Runtime.InteropServices;

namespace DotGLFW;

/// <summary>
/// An opaque handle to a GLFW monitor.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class Cursor
{
    internal readonly IntPtr _handle;

    internal Cursor() { }
    internal Cursor(IntPtr handle) => _handle = handle;

    /// <summary>
    /// A NULL cursor handle. Often used for default values or error handling.
    /// </summary>
    public static readonly Cursor NULL = new Cursor(IntPtr.Zero);

    /// <summary>
    /// Performs equality check against another cursor handle.
    /// </summary>
    public override bool Equals(object other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        if (other is Cursor cursor)
            return _handle.Equals(cursor._handle);

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
    /// Performs equality check against another cursor handle.
    /// </summary>
    public static bool operator ==(Cursor left, Cursor right) => left.Equals(right);

    /// <summary>
    /// Performs inequality check against another cursor handle.
    /// </summary>
    public static bool operator !=(Cursor left, Cursor right) => !left.Equals(right);
}