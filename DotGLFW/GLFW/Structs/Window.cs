using System.Runtime.InteropServices;

namespace DotGLFW;

[StructLayout(LayoutKind.Sequential)]
public class Window : IEquatable<Monitor>
{
    internal readonly IntPtr _handle;

    internal Window() { }
    internal Window(IntPtr handle) => _handle = handle;
    public static readonly Window NULL = new Window(IntPtr.Zero);

    public bool Equals(Monitor other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _handle.Equals(other._handle);
    }
}