using System.Runtime.InteropServices;

namespace DotGLFW;

[StructLayout(LayoutKind.Sequential)]
public class Monitor : IEquatable<Monitor>
{
    internal readonly IntPtr _handle;

    internal Monitor() { }
    private Monitor(IntPtr handle) => _handle = handle;
    public static readonly Monitor NULL = new Monitor(IntPtr.Zero);

    public IntPtr UserPointer
    {
        get => NativeGlfw.GetMonitorUserPointer(_handle);
        set => NativeGlfw.SetMonitorUserPointer(_handle, value);
    }

    public bool Equals(Monitor other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _handle.Equals(other._handle);
    }
}