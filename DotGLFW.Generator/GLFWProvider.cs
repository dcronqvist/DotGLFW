using System.IO;

namespace DotGLFW.Generator;

public class GLFWProvider : IGLFWProvider
{
  private readonly string _glfwRepoPath;

  public GLFWProvider(string glfwRepoPath)
  {
    _glfwRepoPath = glfwRepoPath;
  }

  public string ReadGLFWRepoFile(string filePath)
  {
    using var reader = new StreamReader(Path.Combine(_glfwRepoPath, filePath));
    return reader.ReadToEnd();
  }
}