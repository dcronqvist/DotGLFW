using System.Runtime.InteropServices;

namespace DotGLFW;

/// <summary>
/// An opaque handle to a GLFW monitor.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class Monitor
{
    internal readonly IntPtr _handle;

    internal Monitor() { }
    internal Monitor(IntPtr handle) => _handle = handle;

    /// <summary>
    /// A NULL monitor handle. Often used for default values or error handling.
    /// </summary>
    public static readonly Monitor NULL = new Monitor(IntPtr.Zero);

    /// <summary>
    /// Performs equality check against another monitor handle.
    /// </summary>
    public override bool Equals(object other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        if (other is Monitor monitor)
            return _handle.Equals(monitor._handle);

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
    /// Returns the underlying pointer.
    /// </summary>
    /// <returns></returns>
    public IntPtr GetHandle() => _handle;

    /// <summary>
    /// Performs equality check against another monitor handle.
    /// </summary>
    public static bool operator ==(Monitor left, Monitor right) => left.Equals(right);

    /// <summary>
    /// Performs inequality check against another monitor handle.
    /// </summary>
    public static bool operator !=(Monitor left, Monitor right) => !left.Equals(right);
}