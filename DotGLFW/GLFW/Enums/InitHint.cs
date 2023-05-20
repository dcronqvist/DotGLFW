namespace DotGLFW;

/// <summary>
///     Initialization hints are set before <see cref="Glfw.Init" /> and affect how the library behaves until termination.
///     Read more here <see href="https://www.glfw.org/docs/latest/intro_guide.html#init_hints" />.
/// </summary>
public enum InitHint
{
    /// <summary>
    ///    Specifies whether to also expose joystick hats as buttons, for compatibility with earlier versions of GLFW that did not have glfwGetJoystickHats. Possible values are true and false.
    ///    Default value: true.
    /// </summary>
    JoystickHatButtons = NativeGlfw.GLFW_JOYSTICK_HAT_BUTTONS,

    /// <summary>
    ///     Specifies whether to set the current directory to the application to the Contents/Resources subdirectory of the application's bundle, if present. Possible values are true and false.
    ///     Default value: true.
    /// </summary>
    CocoaChdirResources = NativeGlfw.GLFW_COCOA_CHDIR_RESOURCES,

    /// <summary>
    ///     Specifies whether to create a basic menu bar, either from a nib or manually, when the first window is created, which is when AppKit is initialized. Possible values are true and false.
    ///     Default value: true.
    /// </summary>
    CocoaMenubar = NativeGlfw.GLFW_COCOA_MENUBAR,
}