using System;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace DotGLFW.Generator;

public class Program
{
  private static void Main(string[] args)
  {
    string glfwPath = args[0]; // Path to the GLFW source code
    string generatedPath = args[1]; // Path to the generated source code
    string licensePath = args[2]; // Path to the license file
    string docUrl = args[3]; // URL to the GLFW documentation

    var glfwProvider = new GLFWProvider(glfwPath);
    var sourceWriter = new SourceWriter(generatedPath);
    var license = File.ReadAllText(licensePath);
    var generator = new Generator(new ModelProvider(glfwProvider), sourceWriter, docUrl, license);
    generator.Generate();
  }
}
