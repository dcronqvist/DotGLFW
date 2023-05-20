using System.Runtime.InteropServices;

namespace DotGLFW;

/// <summary>
///     Gamma ramp.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class GammaRamp
{
    IntPtr _red;
    IntPtr _green;
    IntPtr _blue;
    uint _size;

    /// <summary>
    /// An array of value describing the response of the red channel.
    /// </summary>
    public ushort[] Red
    {
        get
        {
            var result = new ushort[_size];
            for (int i = 0; i < _size; i++)
            {
                result[i] = (ushort)Marshal.ReadInt16(_red, i * sizeof(ushort));
            }
            return result;
        }
        set
        {
            if (_red != IntPtr.Zero)
                Marshal.FreeHGlobal(_red);

            _size = (uint)value.Length;
            _red = Marshal.AllocHGlobal(value.Length * sizeof(ushort));
            for (int i = 0; i < value.Length; i++)
            {
                Marshal.WriteInt16(_red, i * sizeof(ushort), (short)value[i]);
            }
        }
    }

    /// <summary>
    /// An array of value describing the response of the green channel.
    /// </summary>
    public ushort[] Green
    {
        get
        {
            var result = new ushort[_size];
            for (int i = 0; i < _size; i++)
            {
                result[i] = (ushort)Marshal.ReadInt16(_green, i * sizeof(ushort));
            }
            return result;
        }
        set
        {
            if (_green != IntPtr.Zero)
                Marshal.FreeHGlobal(_green);

            _size = (uint)value.Length;
            _green = Marshal.AllocHGlobal(value.Length * sizeof(ushort));
            for (int i = 0; i < value.Length; i++)
            {
                Marshal.WriteInt16(_green, i * sizeof(ushort), (short)value[i]);
            }
        }
    }

    /// <summary>
    /// An array of value describing the response of the blue channel.
    /// </summary>
    public ushort[] Blue
    {
        get
        {
            var result = new ushort[_size];
            for (int i = 0; i < _size; i++)
            {
                result[i] = (ushort)Marshal.ReadInt16(_blue, i * sizeof(ushort));
            }
            return result;
        }
        set
        {
            if (_blue != IntPtr.Zero)
                Marshal.FreeHGlobal(_blue);

            _size = (uint)value.Length;
            _blue = Marshal.AllocHGlobal(value.Length * sizeof(ushort));
            for (int i = 0; i < value.Length; i++)
            {
                Marshal.WriteInt16(_blue, i * sizeof(ushort), (short)value[i]);
            }
        }
    }
}