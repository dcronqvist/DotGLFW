using System;
using System.Runtime.InteropServices;
using System.Text;

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

    var versionString = Glfw.GetVersionString();
    Console.WriteLine($"GLFW Version: {versionString}");

    Glfw.WindowHint(WindowHint.ClientAPI, ClientAPI.OpenGLAPI);
    Glfw.WindowHint(WindowHint.ContextVersionMajor, 3);
    Glfw.WindowHint(WindowHint.ContextVersionMinor, 3);
    Glfw.WindowHint(WindowHint.OpenGLProfile, OpenGLProfile.CoreProfile);
    Glfw.WindowHint(WindowHint.DoubleBuffer, true);
    Glfw.WindowHint(WindowHint.Decorated, true);
    Glfw.WindowHint(WindowHint.OpenGLForwardCompat, true);
    Glfw.WindowHint(WindowHint.Resizable, true);
    Glfw.WindowHint(WindowHint.Visible, false);

    var WIDTH = 800;
    var HEIGHT = 600;
    var TITLE = "DotGLFW Example";

    // Create window
    var window = Glfw.CreateWindow(WIDTH, HEIGHT, TITLE, null, null);
    var primary = Glfw.GetPrimaryMonitor();

    Glfw.GetMonitorWorkarea(primary, out var wx, out var wy, out var ww, out var wh);
    Glfw.SetWindowPos(window, wx + ww / 2 - WIDTH / 2, wy + wh / 2 - HEIGHT / 2);
    Glfw.ShowWindow(window);

    Glfw.MakeContextCurrent(window);

    // Enable VSYNC
    Glfw.SwapInterval(1);

    var videoMode = Glfw.GetVideoMode(primary);
    int refreshRate = videoMode.RefreshRate;
    double delta = 1.0 / refreshRate;

    glClearColor = Marshal.GetDelegateForFunctionPointer<glClearColorHandler>(
      Glfw.GetProcAddress("glClearColor"));
    glClear = Marshal.GetDelegateForFunctionPointer<glClearHandler>(
      Glfw.GetProcAddress("glClear"));

    Glfw.SetWindowIcon(window, [CreateIcon()]);

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

  private static Image CreateIcon()
  {
    var image = new Image
    {
      Width = 2,
      Height = 2,
      Pixels = [
        0, 0, 0, 255,
        255, 0, 0, 255,
        0, 255, 0, 255,
        0, 0, 255, 255
      ]
    };
    return image;
  }
}