namespace DotGLFW;

/// <summary>
///     Initialization hints are set before <see cref="Glfw.Init" /> and affect how the library behaves until termination.
///     Read more here <see href="https://www.glfw.org/docs/latest/intro_guide.html#init_hints" />.
/// </summary>
public static class InitHint
{
    /// <summary>
    ///    Specifies whether to also expose joystick hats as buttons, for compatibility with earlier versions of GLFW that did not have glfwGetJoystickHats. Possible values are true and false.
    ///    Default value: true.
    /// </summary>
    public static readonly HintType<bool> JoystickHatButtons = new HintType<bool>(NativeGlfw.GLFW_JOYSTICK_HAT_BUTTONS);

    /// <summary>
    ///     Specifies whether to set the current directory to the application to the Contents/Resources subdirectory of the application's bundle, if present. Possible values are true and false.
    ///     Default value: true.
    /// </summary>
    public static readonly HintType<bool> CocoaChdirResources = new HintType<bool>(NativeGlfw.GLFW_COCOA_CHDIR_RESOURCES);

    /// <summary>
    ///     Specifies whether to create a basic menu bar, either from a nib or manually, when the first window is created, which is when AppKit is initialized. Possible values are true and false.
    ///     Default value: true.
    /// </summary>
    public static readonly HintType<bool> CocoaMenubar = new HintType<bool>(NativeGlfw.GLFW_COCOA_MENUBAR);

    /// <summary>
    ///     Specifies whether to prefer the VK_KHR_xcb_surface extension for creating Vulkan surfaces, or whether to use the VK_KHR_xlib_surface extension. Possible values are true and false.
    /// </summary>
    public static readonly HintType<bool> X11XCBVulkanSurface = new HintType<bool>(NativeGlfw.GLFW_X11_XCB_VULKAN_SURFACE);

    /// <summary>
    ///     Allows control of platform selection. Defaults to <see cref="Platform.AnyPlatform" />.
    /// </summary>
    public static readonly HintType<Platform> Platform = new HintType<Platform>(NativeGlfw.GLFW_PLATFORM);

    /// <summary>
    ///    Specifies the platform type (rendering backend) to request when using OpenGL ES and EGL via ANGLE. Defaults to <see cref="AnglePlatform.None" />.
    /// </summary>
    public static readonly HintType<AnglePlatform> AnglePlatform = new HintType<AnglePlatform>(NativeGlfw.GLFW_ANGLE_PLATFORM_TYPE);
}