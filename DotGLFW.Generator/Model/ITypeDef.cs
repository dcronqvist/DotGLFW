using HtmlAgilityPack;

namespace DotGLFW.Generator;

public interface ITypeDef
{
  string Name { get; }
  HtmlNode Documentation { get; set; }
  string SimpleDocumentation { get; set; }
}
