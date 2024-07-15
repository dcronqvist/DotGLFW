using System.IO;

namespace DotGLFW.Generator;

public class SourceWriter : ISourceWriter
{
  private readonly string _outputDirectory;

  public SourceWriter(string outputDirectory)
  {
    _outputDirectory = outputDirectory;

    if (Directory.Exists(_outputDirectory))
    {
      Directory.Delete(_outputDirectory, true);
    }
  }

  public void WriteToFile(string filePath, string content)
  {
    if (!Directory.Exists(_outputDirectory))
    {
      Directory.CreateDirectory(_outputDirectory);
    }

    if (File.Exists(Path.Combine(_outputDirectory, filePath)))
    {
      File.Delete(Path.Combine(_outputDirectory, filePath));
    }

    using var writer = new StreamWriter(Path.Combine(_outputDirectory, filePath));
    writer.Write(content);
  }
}