namespace DotGLFW.Generator;

public interface ISourceWriter
{
  void WriteToFile(string filePath, string content);
}