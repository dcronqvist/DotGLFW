using System.Runtime.InteropServices;

namespace DotGLFW.Example;

public class Program
{
    private const int GL_COLOR_BUFFER_BIT = 0x00004000;
    private delegate void glClearColorHandler(float r, float g, float b, float a);
    private delegate void glClearHandler(int mask);
    private static glClearColorHandler glClearColor;
    private static glClearHandler glClear;

    static void Main(string[] args)
    {
        Glfw.Init();

        // Set some common hints for the OpenGL profile creation
        Glfw.WindowHint(Hint.ClientAPI, ClientAPI.OpenGLAPI);
        Glfw.WindowHint(Hint.ContextVersionMajor, 3);
        Glfw.WindowHint(Hint.ContextVersionMinor, 3);
        Glfw.WindowHint(Hint.OpenGLProfile, OpenGLProfile.CoreProfile);
        Glfw.WindowHint(Hint.DoubleBuffer, true);
        Glfw.WindowHint(Hint.Decorated, true);
        Glfw.WindowHint(Hint.OpenGLForwardCompat, true);
        Glfw.WindowHint(Hint.Resizable, false);

        var WIDTH = 800;
        var HEIGHT = 600;
        var TITLE = "DotGLFW Example";

        // Create window
        var window = Glfw.CreateWindow(WIDTH, HEIGHT, TITLE, Monitor.NULL, Window.NULL);
        Glfw.MakeContextCurrent(window);

        // Enable VSYNC
        Glfw.SwapInterval(1);

        var primaryMonitor = Glfw.GetPrimaryMonitor();
        Glfw.GetMonitorWorkarea(primaryMonitor, out var x, out var y, out var width, out var height);
        VideoMode primaryVideoMode = Glfw.GetVideoMode(primaryMonitor);

        int refreshRate = primaryVideoMode.RefreshRate;
        double delta = 1.0 / refreshRate;

        // Find center position based on window and monitor sizes
        Glfw.SetWindowPos(window, width / 2 - WIDTH / 2, height / 2 - HEIGHT / 2);

        glClearColor = Marshal.GetDelegateForFunctionPointer<glClearColorHandler>(Glfw.GetProcAddress("glClearColor"));
        glClear = Marshal.GetDelegateForFunctionPointer<glClearHandler>(Glfw.GetProcAddress("glClear"));

        while (!Glfw.WindowShouldClose(window))
        {
            Glfw.PollEvents();
            Glfw.SwapBuffers(window);

            double currentTime = Glfw.GetTime();
            SetHueShiftedColor(currentTime * delta * 200);

            // Clear the buffer to the set color
            glClear(GL_COLOR_BUFFER_BIT);
        }
    }

    private static void SetHueShiftedColor(double time)
    {
        // Set the clear color to a shifted hue
        float r = (float)(Math.Sin(time) / 2 + 0.5);
        float g = (float)(Math.Sin(time + 2) / 2 + 0.5);
        float b = (float)(Math.Sin(time + 4) / 2 + 0.5);
        float a = 1.0f;
        glClearColor(r, g, b, a);
    }
}