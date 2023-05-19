namespace DotGLFW;

/// <summary>
/// Base exception class for GLFW related errors.
/// </summary>
public class GLFWException : System.Exception
{
    #region Methods

    /// <summary>
    ///     Generic error messages if only an error code is supplied as an argument to the constructor.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <returns>Error message.</returns>
    public static string GetErrorMessage(ErrorCode code)
    {
        return code.ToString();
    }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="GLFWException" /> class.
    /// </summary>
    /// <param name="error">The error code to create a generic message from.</param>
    public GLFWException(ErrorCode error) : base(GetErrorMessage(error)) { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="GLFWException" /> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public GLFWException(string message) : base(message) { }

    #endregion
}
