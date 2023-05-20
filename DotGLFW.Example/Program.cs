using System.Runtime.InteropServices;

namespace DotGLFW.Example;

public class Program
{
    private const string TITLE = "Example Window";
    private const int WIDTH = 800;
    private const int HEIGHT = 600;

    private const int GL_COLOR_BUFFER_BIT = 0x00004000;
    private delegate void glClearColorHandler(float r, float g, float b, float a);
    private delegate void glClearHandler(int mask);

    private static glClearColorHandler glClearColor;
    private static glClearHandler glClear;

    public static void Main(string[] args)
    {
        Glfw.SetErrorCallback((ErrorCode errorCode, string description) =>
        {
            Console.WriteLine($"GLFW Error: {errorCode.ToString()} - {description}");
        });

        var result = Glfw.Init();
        Console.WriteLine($"GLFW Init: {result}");

        Glfw.GetVersion(out int major, out int minor, out int rev);
        Console.WriteLine($"GLFW Version: {major}.{minor}.{rev}");

        var monitors = Glfw.GetMonitors();
        Console.WriteLine($"Monitor Count: {monitors.Length}");

        foreach (var monitor in monitors)
        {
            var name = Glfw.GetMonitorName(monitor);
            Glfw.GetMonitorWorkarea(monitor, out int x, out int y, out int width, out int height);
            var videoMode = Glfw.GetVideoMode(monitor);
            Console.WriteLine($"Monitor Name: {name}");
            Console.WriteLine($"Monitor Workarea: {x}, {y}, {width}, {height}");
            Console.WriteLine($"Monitor VideoMode: {videoMode.Width}, {videoMode.Height}, {videoMode.RedBits}, {videoMode.GreenBits}, {videoMode.BlueBits}, {videoMode.RefreshRate}");
        }

        Glfw.Terminate();
    }

    private static void SetColor(double time)
    {
        // Set the clear color based on the current time
        // Hue shifting over time

        var hue = (time % 360) / 360.0f;

        var r = (float)Math.Sin(hue * 2 * Math.PI);
        var g = (float)Math.Sin((hue + 1.0 / 3.0) * 2 * Math.PI);
        var b = (float)Math.Sin((hue + 2.0 / 3.0) * 2 * Math.PI);

        glClearColor(r, g, b, 1.0f);
    }
}