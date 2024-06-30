﻿using System.Runtime.InteropServices;

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
        if (!Glfw.Init())
        {
            Console.Error.WriteLine("Failed to initialize GLFW");
            Environment.Exit(1);
        }

        Glfw.SetErrorCallback((errorCode, description) =>
        {
            Console.Error.WriteLine($"GLFW Error: {errorCode} - {description}");
        });

        Glfw.WindowHint(Hint.ContextVersionMajor, 3);
        Glfw.WindowHint(Hint.ContextVersionMinor, 3);
        Glfw.WindowHint(Hint.OpenGLProfile, OpenGLProfile.CoreProfile);
        Glfw.WindowHint(Hint.Samples, 4);

        Window window = Glfw.CreateWindow(640, 480, "FeelsDankMan", Monitor.NULL, Window.NULL);

        if (window == null)
        {
            Console.Error.WriteLine("Failed to create GLFW window");
            Glfw.Terminate();
            Environment.Exit(1);
        }

        Glfw.MakeContextCurrent(window);

        glClearColor = Marshal.GetDelegateForFunctionPointer<glClearColorHandler>(Glfw.GetProcAddress("glClearColor"));
        glClear = Marshal.GetDelegateForFunctionPointer<glClearHandler>(Glfw.GetProcAddress("glClear"));

        // This line crashes the program
        Glfw.GetGamepadState(Joystick.Joystick1, out GamepadState state);

        while (!Glfw.WindowShouldClose(window))
        {
            Glfw.PollEvents();
            glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            glClear(GL_COLOR_BUFFER_BIT);

            Glfw.SwapBuffers(window);
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