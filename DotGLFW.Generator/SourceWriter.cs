using System;
using System.IO;

namespace DotGLFW.Generator;

public class SourceWriter : ISourceWriter
{
  private readonly string _outputDirectory;

  public SourceWriter(string outputDirectory)
  {
    _outputDirectory = outputDirectory;
  }

  public void WriteToFile(string filePath, string content)
  {
    if (!Directory.Exists(_outputDirectory))
    {
      Directory.CreateDirectory(_outputDirectory);
      Console.WriteLine($"Created directory: {_outputDirectory}");
    }

    if (File.Exists(Path.Combine(_outputDirectory, filePath)))
    {
      File.Delete(Path.Combine(_outputDirectory, filePath));
      Console.WriteLine($"Deleted existing file: {filePath}");
    }

    using var writer = new StreamWriter(Path.Combine(_outputDirectory, filePath), false);
    writer.Write(content);
    Console.WriteLine($"Wrote file: {filePath}");
  }
}