using System.Collections.Generic;
using HtmlAgilityPack;

namespace DotGLFW.Generator;

public class DelegateTypeDef : ITypeDef
{
    public string Name { get; set; }
    public HtmlNode Documentation { get; set; }
    public string SimpleDocumentation { get; set; }
    public CppType ReturnType { get; set; }
    public List<(string Name, CppType Type)> Parameters { get; set; } = [];
}
