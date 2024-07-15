using System.Collections.Generic;
using HtmlAgilityPack;

namespace DotGLFW.Generator;

public class StructTypeDef : ITypeDef
{
    public string Name { get; set; }
    public HtmlNode Documentation { get; set; }
    public string SimpleDocumentation { get; set; }
    public List<StructTypeField> Fields { get; set; } = [];
}