using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace DotGLFW.Generator;

public class ModelProvider : IModelProvider
{
  private readonly IGLFWProvider _glfwProvider;

  public ModelProvider(
    IGLFWProvider glfwProvider)
  {
    _glfwProvider = glfwProvider;
  }

  public ParsedAPI GetAPI()
  {
    var glfw3_8h = _glfwProvider.ReadGLFWRepoFile("docs/html/glfw3_8h.html");
    var doc = new HtmlDocument();
    doc.LoadHtml(glfw3_8h);

    var macroTable = doc.DocumentNode.Descendants("table")
      .Where(table =>
        table.HasClass("memberdecls") && table.Descendants("tr").First().InnerText.Contains("Macro"))
      .First();

    var excludedMacros = new[] { "GLAPIENTRY" };
    var macros = ParseMacrosInTable(macroTable)
      .Where(macro => !excludedMacros.Contains(macro.Name));

    var typeDefTable = doc.DocumentNode.Descendants("table")
      .Where(table =>
        table.HasClass("memberdecls") && table.Descendants("tr").First().InnerText.Contains("Typedefs"))
      .First();

    var excludedTypedefs = new[] { "GLFWglproc", "GLFWvkproc" };
    var typedefs = ParseTypeDefsInTable(typeDefTable)
      .Where(typeDef => !excludedTypedefs.Contains(typeDef.Name))
      .ToList();

    typedefs.Add(new DelegateTypeDef
    {
      Name = "PFN_vkGetInstanceProcAddr",
      ReturnType = new CppType { Name = "void*" },
      SimpleDocumentation = "The function pointer type for the instance-level Vulkan function vkGetInstanceProcAddr.",
      Parameters = [
        new ("instance", new CppType { Name = "VkInstance" }),
        new ("pName", new CppType { Name = "const char*" })
      ]
    });

    var functionsTable = doc.DocumentNode.Descendants("table")
      .Where(table =>
        table.HasClass("memberdecls") && table.Descendants("tr").First().InnerText.Contains("Functions"))
      .First();

    var functions = ParseFunctionsInTable(functionsTable);

    var parsedAPI = new ParsedAPI
    {
      MacroCollections = GetMacroCollections().ToList(),
      Macros = macros.ToList(),
      TypeDefs = typedefs,
      Functions = functions.ToList()
    };

    return parsedAPI;
  }

  private sealed class TableParser<T>
  {
    private readonly HtmlNode _table;
    private readonly IGLFWProvider _glfwProvider;
    private int _rowIndex = -1;
    private readonly Func<HtmlNode, HtmlNode, string, T> _itemParser;

    public TableParser(
      HtmlNode table,
      IGLFWProvider glfwProvider,
      Func<HtmlNode, HtmlNode, string, T> itemParser)
    {
      _table = table;
      _glfwProvider = glfwProvider;
      _itemParser = itemParser;
    }

    public IEnumerable<T> Parse()
    {
      var rowsInTable = _table.Descendants("tr").Count();
      while (_rowIndex + 1 < rowsInTable)
      {
        var item = ParseItem();
        if (!EqualityComparer<T>.Default.Equals(item, default))
        {
          yield return item;
        }
      }
    }

    private HtmlNode ConsumeRow()
    {
      return _table.Descendants("tr").Skip(++_rowIndex).First();
    }

    private HtmlNode CurrentRow()
    {
      return _table.Descendants("tr").Skip(_rowIndex).First();
    }

    private bool TryConsumeRow(Predicate<HtmlNode> predicate, out HtmlNode row)
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

    private bool IsRowHeading(HtmlNode row)
    {
      return row.GetAttributeValue("class", "").StartsWith("heading");
    }

    private bool RowHasAttributes(HtmlNode row)
    {
      return row.Attributes.Any();
    }

    private T ParseItem()
    {
      var row = ConsumeRow();
      if (IsRowSeparator(row) || IsRowHeading(row) || !RowHasAttributes(row))
        return default;

      var fullDocs = GetFullDocsForItem(row);

      string simpleDocs = null;
      if (TryConsumeRow(row => row.GetAttributeValue("class", "").StartsWith("memdesc"), out var simpleDocsRow))
        simpleDocs = ReadSimpleDocsFromRow(simpleDocsRow);

      return _itemParser(row, fullDocs, simpleDocs);
    }

    private string ReadSimpleDocsFromRow(HtmlNode row)
    {
      var cells = row.Descendants("td").ToArray();
      return cells.Skip(1).First().InnerText.Trim()
        .Remove("\n").Remove("&#160;").FixSpaces();
    }

    private HtmlNode GetFullDocsForItem(HtmlNode row)
    {
      var cellThatHasLinkToDocs = row.Descendants("td").Last();
      var linkInDocs = cellThatHasLinkToDocs.Descendants("a").FirstOrDefault();
      var linkToDocs = linkInDocs.GetAttributeValue("href", null);
      if (linkToDocs == null)
        return null;

      var idInLink = linkToDocs.Split('#').Last();
      var fileInLink = linkToDocs.Split('#').First();

      var docsHtml = _glfwProvider.ReadGLFWRepoFile($"docs/html/{fileInLink}");
      var doc = new HtmlDocument();
      doc.LoadHtml(docsHtml);

      var link = doc.DocumentNode.Descendants("a").First(a => a.GetAttributeValue("name", null) == idInLink);
      var h2Sibling = link.NextSibling.NextSibling;
      var linkInSibling = h2Sibling.Descendants("a").First();
      var linkInLink = linkInSibling.GetAttributeValue("href", null);

      if (linkInLink != $"#{idInLink}")
        throw new Exception("Link in sibling is not the same as the id in link");

      var memItemDiv = h2Sibling.NextSibling.NextSibling;
      var memDoc = memItemDiv.Descendants("div").Where(div => div.HasClass("memdoc")).First();

      return memDoc;
    }
  }

  private IEnumerable<Macro> ParseMacrosInTable(HtmlNode table)
  {
    var macroParser = new TableParser<Macro>(table, _glfwProvider, ParseMacro);
    return macroParser.Parse();
  }

  private Macro ParseMacro(HtmlNode row, HtmlNode fullDocs, string simpleDocs)
  {
    var rowInnerText = row.InnerText.Replace("&#160;", " ").Replace('\n', ' ').Trim();

    var lexer = new Lexer(rowInnerText);
    var tokens = lexer.Lex().ToArray();
    var parser = new MacroParser(tokens);
    var parsedMacro = parser.Parse();
    parsedMacro.Documentation = simpleDocs;
    parsedMacro.Type = new CppType { Name = "int" };

    return parsedMacro;
  }

  private IEnumerable<MacroCollection> GetMacroCollections()
  {
    (string file, string name, string prefixToRemove, string suffixToRemove)[] collections = [
      ("buttons", "MouseButton", "GLFW_MOUSE_", ""),
      ("errors", "ErrorCode", "GLFW_", ""),
      ("gamepad__axes", "GamepadAxis", "GLFW_GAMEPAD_AXIS_", ""),
      ("gamepad__buttons", "GamepadButton", "GLFW_GAMEPAD_BUTTON_", ""),
      ("hat__state", "JoystickHat", "GLFW_HAT_", ""),
      ("joysticks", "Joystick", "GLFW_JOYSTICK_", ""),
      ("Keys", "Key", "GLFW_KEY_", ""),
      ("mods", "ModifierKey", "GLFW_MOD_", ""),
      ("shapes", "CursorShape", "GLFW_", "_CURSOR")
    ];

    foreach (var (collection, name, prefixToRemove, suffixToRemove) in collections)
    {

      var html = _glfwProvider.ReadGLFWRepoFile($"docs/html/group__{collection}.html");
      var doc = new HtmlDocument();
      doc.LoadHtml(html);

      var table = doc.DocumentNode.Descendants("table")
        .Where(table =>
          table.HasClass("memberdecls") && table.Descendants("tr").First().InnerText.Contains("Macros"))
        .First();

      var macros = ParseMacrosInTable(table).ToList();
      yield return new MacroCollection
      {
        Name = name,
        Macros = macros,
        PrefixToRemove = prefixToRemove,
        SuffixToRemove = suffixToRemove
      };
    }
  }

  private IEnumerable<ITypeDef> ParseTypeDefsInTable(HtmlNode table)
  {
    var typeDefParser = new TableParser<ITypeDef>(table, _glfwProvider, ParseTypeDef);
    return typeDefParser.Parse();
  }

  private ITypeDef ParseTypeDef(HtmlNode row, HtmlNode fullDocs, string simpleDocs)
  {
    var rowInnerText = row.InnerText.Replace("&#160;", " ").Replace('\n', ' ').Trim();
    var lexer = new Lexer(rowInnerText);
    var tokens = lexer.Lex().ToArray();
    var parser = new TypeDefParser(tokens);
    var parsedTypeDef = parser.Parse();
    parsedTypeDef.Documentation = fullDocs;
    parsedTypeDef.SimpleDocumentation = simpleDocs;

    if (parsedTypeDef is StructTypeDef std)
    {
      var cells = row.Descendants("td").ToArray();
      var firstCell = cells.First();
      var linkInCell = firstCell.Descendants("a").FirstOrDefault();
      var link = linkInCell.GetAttributeValue("href", null);

      if (link.StartsWith("struct"))
      {
        std.Fields = ParseFieldsInStructFile(link);
      }
    }

    return parsedTypeDef;
  }

  private List<StructTypeField> ParseFieldsInStructFile(string structFile)
  {
    var structHtml = _glfwProvider.ReadGLFWRepoFile($"docs/html/{structFile}");
    var doc = new HtmlDocument();
    doc.LoadHtml(structHtml);

    var fieldTable = doc.DocumentNode.Descendants("table")
      .Where(table =>
        table.HasClass("memberdecls") && table.Descendants("tr").First().InnerText.Contains("Data Fields"))
      .First();

    var fieldParser = new TableParser<StructTypeField>(fieldTable, _glfwProvider, ParseField);
    return fieldParser.Parse().ToList();
  }

  private StructTypeField ParseField(HtmlNode row, HtmlNode fullDocs, string simpleDocs)
  {
    var innerText = row.InnerText.Replace("&#160;", " ").Replace('\n', ' ').Trim();
    var lexer = new Lexer(innerText);
    var tokens = lexer.Lex().ToArray();
    var parser = new TypeDefParser.StructFieldParser(tokens);
    var parsedField = parser.Parse();
    parsedField.SimpleDocumentation = simpleDocs;
    parsedField.Documentation = fullDocs;
    return parsedField;
  }

  private IEnumerable<Function> ParseFunctionsInTable(HtmlNode table)
  {
    var functionParser = new TableParser<Function>(table, _glfwProvider, ParseFunction);
    return functionParser.Parse();
  }

  private Function ParseFunction(HtmlNode row, HtmlNode fullDocs, string simpleDocs)
  {
    var rowInnerText = row.InnerText.Replace("&#160;", " ").Replace('\n', ' ').Trim();
    var lexer = new Lexer(rowInnerText);
    var tokens = lexer.Lex().ToArray();
    var parser = new FunctionParser(tokens);
    var parsedFunction = parser.Parse();
    parsedFunction.Documentation = fullDocs;
    parsedFunction.SimpleDocumentation = simpleDocs;
    return parsedFunction;
  }
}