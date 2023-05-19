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
        // Set some common hints for the OpenGL profile creation
        Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
        Glfw.WindowHint(Hint.ContextVersionMajor, 3);
        Glfw.WindowHint(Hint.ContextVersionMinor, 3);
        Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
        Glfw.WindowHint(Hint.Doublebuffer, true);
        Glfw.WindowHint(Hint.Decorated, true);
        Glfw.WindowHint(Hint.OpenglForwardCompatible, true);

        // Create window
        var window = Glfw.CreateWindow(WIDTH, HEIGHT, TITLE, Monitor.None, Window.None);
        Glfw.MakeContextCurrent(window);

        // Effectively enables VSYNC by setting to 1.
        Glfw.SwapInterval(1);

        // Find center position based on window and monitor sizes
        var screenSize = Glfw.PrimaryMonitor.WorkArea;
        var x = (screenSize.Width - WIDTH) / 2;
        var y = (screenSize.Height - HEIGHT) / 2;
        Glfw.SetWindowPosition(window, x, y);

        glClearColor = Marshal.GetDelegateForFunctionPointer<glClearColorHandler>(Glfw.GetProcAddress("glClearColor"));
        glClear = Marshal.GetDelegateForFunctionPointer<glClearHandler>(Glfw.GetProcAddress("glClear"));

        double hueShiftScale = 50.0;

        while (!Glfw.WindowShouldClose(window))
        {
            // Poll for OS events and swap front/back buffers
            Glfw.PollEvents();
            Glfw.SwapBuffers(window);

            SetColor(Glfw.Time * hueShiftScale);

            // Clear the buffer to the set color
            glClear(GL_COLOR_BUFFER_BIT);
        }
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