using System.Collections.Generic;
using HtmlAgilityPack;

namespace DotGLFW.Generator;

public class Function
{
  public string Name { get; set; }
  public HtmlNode Documentation { get; set; }
  public string SimpleDocumentation { get; set; }
  public CppType ReturnType { get; set; }
  public List<(string Name, CppType Type)> Parameters { get; set; } = [];
}

public class FunctionParameter
{
  public string Name { get; set; }
  public CppType Type { get; set; }
  public HtmlNode Documentation { get; set; }
  public string SimpleDocumentation { get; set; }

  public bool IsOut { get; set; }
}