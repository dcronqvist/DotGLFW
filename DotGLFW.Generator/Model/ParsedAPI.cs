using System.Collections.Generic;

namespace DotGLFW.Generator;

public class MacroCollection
{
  public string Name { get; set; }
  public List<Macro> Macros { get; set; } = [];
  public string PrefixToRemove { get; set; }
  public string SuffixToRemove { get; set; }
}

public class ParsedAPI
{
  public List<MacroCollection> MacroCollections { get; set; } = [];
  public List<Macro> Macros { get; set; } = [];
  public List<ITypeDef> TypeDefs { get; set; } = [];
  public List<Function> Functions { get; set; } = [];
}
