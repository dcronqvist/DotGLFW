using System.Runtime.InteropServices;

namespace DotGLFW;

[StructLayout(LayoutKind.Sequential)]
public class Cursor : IEquatable<Monitor>
{
    internal readonly IntPtr _handle;

    internal Cursor() { }
    internal Cursor(IntPtr handle) => _handle = handle;
    public static readonly Cursor NULL = new Cursor(IntPtr.Zero);

    public bool Equals(Monitor other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _handle.Equals(other._handle);
    }
}