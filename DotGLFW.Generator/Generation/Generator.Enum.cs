using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotGLFW.Generator;

public partial class Generator
{
  public void GenerateEnums(ParsedAPI api)
  {
    var enums = api.MacroCollections;
    enums.ToList().ForEach(GenerateEnum);
  }

  private void GenerateEnum(MacroCollection macroCollection)
  {
    var content = new StringBuilder();
    WriteFileHeader(content);

    content.AppendLine("using static DotGLFW.NativeGlfw;");
    content.AppendLine("namespace DotGLFW;");
    content.AppendLine();
    content.AppendLine("/// <summary>");
    content.AppendLine($"/// Wrapping enum for {macroCollection.Name}.");
    content.AppendLine("/// </summary>");
    content.AppendLine($"public enum {macroCollection.Name}");
    content.AppendLine("{");

    foreach (var macro in macroCollection.Macros)
    {
      var csharpName = ConvertEnumName(macro.Name, macroCollection.PrefixToRemove, macroCollection.SuffixToRemove);
      if (macro.TryGetValue(out var value))
      {
        content.AppendLine($"  /// <inheritdoc cref=\"NativeGlfw.{macro.Name}\" />");
        content.AppendLine($"  {csharpName} = NativeGlfw.{macro.Name},");
      }
    }

    content.AppendLine("}");

    _sourceWriter.WriteToFile($"Enum.{macroCollection.Name}.gen.cs", content.ToString());
  }

  private string ConvertEnumName(string name, string prefixToRemove, string suffixToRemove)
  {
    string withoutPrefixSuffix = name;
    if (!string.IsNullOrEmpty(prefixToRemove))
    {
      withoutPrefixSuffix = withoutPrefixSuffix.Replace(prefixToRemove, "");
    }
    if (!string.IsNullOrEmpty(suffixToRemove))
    {
      withoutPrefixSuffix = withoutPrefixSuffix.Replace(suffixToRemove, "");
    }

    var split = withoutPrefixSuffix.Split('_');
    var result = new StringBuilder();
    foreach (var part in split)
    {
      result.Append(part.ToLower().Capitalize());
    }
    var final = result.ToString();
    if (char.IsDigit(final[0]))
    {
      final = "D" + final;
    }
    return final;
  }
}