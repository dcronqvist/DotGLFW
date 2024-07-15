using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace DotGLFW.Generator;

public class TableParser
{
  private readonly IGLFWProvider _glfwProvider;
  private readonly HtmlNode _table;

  private int _rowIndex = -1;

  public TableParser(IGLFWProvider glfwProvider, HtmlNode itemTable)
  {
    _glfwProvider = glfwProvider;
    _table = itemTable;
  }

  public ParsedAPI Parse()
  {
    // Parsing
    var api = new ParsedAPI();
    var rowsInTable = _table.Descendants("tr").Count();
    while (_rowIndex + 1 < rowsInTable)
    {
      ParseItem(ref api);
    }

    return api;
  }

  // Parsing helpers
  private HtmlNode PreviousRow()
  {
    return _table.Descendants("tr").Skip(_rowIndex - 1).First();
  }

  private HtmlNode CurrentRow()
  {
    return _table.Descendants("tr").Skip(_rowIndex).First();
  }

  private HtmlNode ConsumeRow()
  {
    return _table.Descendants("tr").Skip(++_rowIndex).First();
  }

  private bool TryConsumeNextRow(Predicate<HtmlNode> predicate, out HtmlNode row)
  {
    if (_rowIndex + 1 >= _table.Descendants("tr").Count())
    {
      row = null;
      return false;
    }

    row = _table.Descendants("tr").Skip(_rowIndex + 1).First();
    if (predicate(row))
    {
      _rowIndex++;
      return true;
    }

    return false;
  }

  private bool IsRowSeparator(HtmlNode row)
  {
    return row.GetAttributeValue("class", "").StartsWith("separator");
  }

  private string ReadDocsFromRow(HtmlNode row)
  {
    var cells = row.Descendants("td").ToArray();
    return cells.Skip(1).First().InnerText.Trim().Remove("\n").Remove("&#160").FixSpaces();
  }

  // Parsers
  private void ParseItem(ref ParsedAPI api)
  {
    var row = ConsumeRow();
    if (row.HasClass("heading") || IsRowSeparator(row) || !row.Attributes.Any())
    {
      return;
    }

    var cells = row.Descendants("td").ToArray();
    var firstCell = cells.First();
    var cellType = firstCell.InnerText;

    if (cellType.StartsWith("#define"))
    {
      ParseMacro(ref api);
    }
    else if (cellType.StartsWith("typedef"))
    {

    }
    else
    {
      // Assume it's a function

    }
  }

  private void ParseMacro(ref ParsedAPI api)
  {
    var row = CurrentRow();
    var cells = row.Descendants("td").ToArray();
    var linkThatContainsName = cells.Skip(1).First().Descendants("a").First();
    var name = linkThatContainsName.InnerText;
    // var value = ParseMacroValue(linkThatContainsName.NextSibling)?.Trim();

    var macro = new Macro
    {
      Name = name,
      // Value = value,
      Type = new CppType { Name = "int" }
    };

    if (TryConsumeNextRow(row => !IsRowSeparator(row), out var docsRow))
    {
      macro.Documentation = ReadDocsFromRow(docsRow);
    }

    api.Macros.Add(macro);
  }

  // private void ParseTypeDef(ref ParsedAPI api)
  // {
  //   var row = CurrentRow();
  //   var typedefString = row.InnerText.Trim();
  //   if (typedefString.StartsWith("typedef struct"))
  //   {
  //     ParseStructTypeDef(ref api);
  //   }
  //   else
  //   {
  //     ParseDelegateTypeDef(ref api, typedefString);
  //   }
  // }

  // private void ParseDelegateTypeDef(ref ParsedAPI api, string typedefString)
  // {
  //   var withoutTypedef = typedefString.Substring("typedef ".Length).Trim();
  //   var returnType = withoutTypedef.Split("(*").First().Trim();
  //   var name = withoutTypedef.Split(" ").Where(x => x != "").Skip(1).First()[..^1].Trim();
  //   var parameters = withoutTypedef.Split(")").Skip(1).First().Trim().Substring(1);

  //   var delegateTypeDef = new DelegateTypeDef
  //   {
  //     Name = name,
  //     // ReturnType = returnType,
  //     // Parameters = ParseParameters(parameters)
  //   };

  //   if (TryConsumeNextRow(row => !IsRowSeparator(row), out var docsRow))
  //   {
  //     delegateTypeDef.Documentation = ReadDocsFromRow(docsRow);
  //   }

  //   api.TypeDefs.Add(delegateTypeDef);
  // }

  // private List<(string Name, string Type)> ParseParameters(string parametersString)
  // {
  //   if (parametersString == "void")
  //   {
  //     return [];
  //   }

  //   return parametersString.Split(", ").Select(param =>
  //   {
  //     var split = param.Split(" ");
  //     return (split[1], split[0]);
  //   }).ToList();
  // }

  // private void ParseStructTypeDef(ref ParsedAPI api)
  // {
  //   var row = CurrentRow();
  //   var name = row.Descendants("td").First().Descendants("a").First().InnerText;
  //   var href = row.Descendants("td").First().Descendants("a").First()
  //     .GetAttributeValue("href", "");

  //   bool isOpaque = href.StartsWith("group_");

  //   string docs = null;
  //   if (TryConsumeNextRow(row => !IsRowSeparator(row), out var docsRow))
  //   {
  //     docs = ReadDocsFromRow(docsRow);
  //   }

  //   if (isOpaque)
  //   {
  //     api.TypeDefs.Add(new OpaqueObjectTypeDef { Name = name, Documentation = docs });
  //   }
  //   else
  //   {
  //     string structHtml = _glfwProvider.ReadGLFWRepoFile($"docs/html/{href}");
  //     var structHtmlDoc = new HtmlDocument();
  //     structHtmlDoc.LoadHtml(structHtml);

  //     var structTypeDef = ParseStructTypeDefWithNameAndInFile(name, structHtmlDoc.DocumentNode);
  //     structTypeDef.Documentation = docs;
  //     api.TypeDefs.Add(structTypeDef);
  //   }
  // }

  // private StructTypeDef ParseStructTypeDefWithNameAndInFile(string name, HtmlNode structHtmlBody)
  // {
  //   var table = structHtmlBody.Descendants("table").Where(table => table.HasClass("memberdecls")).First();
  //   var rows = table.Descendants("tr").ToArray();

  //   int rowIndex = 0;
  //   var fields = new Dictionary<string, string>();

  //   while (rowIndex < rows.Length)
  //   {
  //     var row = rows[rowIndex];
  //     if (row.HasClass("heading") || IsRowSeparator(row) || !row.Attributes.Any())
  //     {
  //       rowIndex++;
  //       continue;
  //     }

  //     var fieldType = row.Descendants("td").First().InnerText.Trim().Remove("&#160;");
  //     var fieldName = row.Descendants("td").Skip(1).First().InnerText.Trim().Remove("&#160;");

  //     fields.Add(fieldName, fieldType);
  //     rowIndex++;
  //   }

  //   return new StructTypeDef
  //   {
  //     Name = name,
  //     Fields = fields,
  //     Documentation = "DOCS FOR STRUCT FIELDS ARE NOT SUPPORTED YET"
  //   };
  // }
}