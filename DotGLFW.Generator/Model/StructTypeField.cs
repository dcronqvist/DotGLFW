using HtmlAgilityPack;

namespace DotGLFW.Generator;

public class StructTypeField
{
    public string Name { get; set; }
    public CppType Type { get; set; }
    public HtmlNode Documentation { get; set; }
    public string SimpleDocumentation { get; set; }
}
