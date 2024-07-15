using System.Linq;
using System.Text;

namespace DotGLFW.Generator;

public partial class Generator
{
  private void GenerateNativeBinding(ParsedAPI api)
  {
    NativeGlfwGenerateMacros(api);
    NativeGlfwGenerateTypeDefs(api);
    NativeGlfwGenerateFunctions(api);
  }

  private void NativeGlfwGenerateMacros(ParsedAPI api)
  {
    var content = new StringBuilder();
    WriteFileHeader(content);

    content.AppendLine("#pragma warning disable 1591");
    content.AppendLine("namespace DotGLFW;");
    content.AppendLine();
    content.AppendLine("/// <summary>");
    content.AppendLine("/// Native bindings to the GLFW library.");
    content.AppendLine("/// </summary>");
    content.AppendLine($"public unsafe static partial class {NativeGLFWClassName}");
    content.AppendLine("{");

    foreach (var macro in api.Macros)
    {
      if (macro.TryGetValue(out var value))
      {
        if (macro.Documentation != null)
        {
          content.AppendLine($"  /// <summary>");
          content.AppendLine($"  /// {macro.Documentation}");
          content.AppendLine($"  /// </summary>");
        }
        content.AppendLine($"  public const {ConvertToCSharpType(macro.Type, api)} {macro.Name} = unchecked((int){value});");
      }
    }

    content.AppendLine("}");
    content.AppendLine("#pragma warning restore 1591");

    _sourceWriter.WriteToFile($"{NativeGLFWClassName}.Macros.gen.cs", content.ToString());
  }

  private void NativeGlfwGenerateTypeDefs(ParsedAPI api)
  {
    var content = new StringBuilder();
    WriteFileHeader(content);

    content.AppendLine("using System.Runtime.InteropServices;");
    content.AppendLine("using System.Security;");
    content.AppendLine("namespace DotGLFW;");
    content.AppendLine();
    content.AppendLine("[SuppressUnmanagedCodeSecurity]");
    content.AppendLine($"public unsafe static partial class {NativeGLFWClassName}");
    content.AppendLine("{");

    foreach (var typeDef in api.TypeDefs)
    {
      if (typeDef is DelegateTypeDef delegateTypeDef)
      {
        content.AppendLine($"  /// <summary>");
        var docs = ParseFunctionDocsIntoXMLDocs(delegateTypeDef.Documentation, api, (cref) =>
        {
          return $"{NativeGLFWClassName}.{cref}";
        })
        .FixNewlines().FixSpaces();
        if (!string.IsNullOrWhiteSpace(docs))
          content.AppendLine($"  /// {docs}");
        else if (delegateTypeDef.SimpleDocumentation != null)
          content.AppendLine($"  /// {delegateTypeDef.SimpleDocumentation}");
        content.AppendLine($"  /// </summary>");
        content.AppendLine("  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]");
        content.Append($"  public delegate {ConvertToCSharpType(delegateTypeDef.ReturnType, api)} {SanitizeIdentifierForCSharp(delegateTypeDef.Name)}(");
        for (var i = 0; i < delegateTypeDef.Parameters.Count; i++)
        {
          var (name, type) = delegateTypeDef.Parameters[i];
          content.Append($"{ConvertToCSharpType(type, api)} {SanitizeIdentifierForCSharp(name)}");
          if (i < delegateTypeDef.Parameters.Count - 1)
            content.Append(", ");
        }
        content.AppendLine(");");
        content.AppendLine();
      }
      else if (typeDef is StructTypeDef structTypeDef)
      {
        content.AppendLine($"  /// <summary>");
        var docs = ParseFunctionDocsIntoXMLDocs(structTypeDef.Documentation, api, (cref) =>
        {
          return $"{NativeGLFWClassName}.{cref}";
        })
        .FixNewlines().FixSpaces();
        content.AppendLine($"  /// {docs}");
        content.AppendLine($"  /// </summary>");
        content.AppendLine($"  [StructLayout(LayoutKind.Sequential)]");
        content.AppendLine($"  public unsafe struct {SanitizeIdentifierForCSharp(structTypeDef.Name)}");
        content.AppendLine("  {");
        foreach (var field in structTypeDef.Fields)
        {
          var fieldDocs = ParseFunctionDocsIntoXMLDocs(field.Documentation, api, (cref) =>
          {
            return $"{NativeGLFWClassName}.{cref}";
          }).FixNewlines().FixSpaces();
          if (!string.IsNullOrWhiteSpace(fieldDocs))
          {
            content.AppendLine($"    /// <summary>");
            content.AppendLine($"    /// {fieldDocs}");
            content.AppendLine($"    /// </summary>");
          }
          if (field.Type.IsArray && field.Type.ArraySize != -1)
          {
            content.AppendLine($"    public fixed {ConvertToCSharpType(field.Type, api, false)} {SanitizeIdentifierForCSharp(field.Name)}[{field.Type.ArraySize}];");
          }
          else
          {
            content.AppendLine($"    public {ConvertToCSharpType(field.Type, api, forceDelegatesToIntPtr: true)} {SanitizeIdentifierForCSharp(field.Name)};");
          }
        }
        content.AppendLine("  }");
      }
    }

    content.AppendLine("}");

    _sourceWriter.WriteToFile($"{NativeGLFWClassName}.TypeDefs.gen.cs", content.ToString());
  }

  private void NativeGlfwGenerateFunctions(ParsedAPI api)
  {
    var content = new StringBuilder();
    WriteFileHeader(content);

    content.AppendLine("using System.Runtime.InteropServices;");
    content.AppendLine("using System.Runtime.CompilerServices;");
    content.AppendLine("namespace DotGLFW;");
    content.AppendLine();
    content.AppendLine($"public unsafe static partial class {NativeGLFWClassName}");
    content.AppendLine("{");
    content.AppendLine("  private const string LIBRARY_DLL = \"glfw3\";");
    content.AppendLine();

    foreach (var function in api.Functions)
    {
      var returnType = ConvertToCSharpType(function.ReturnType, api);
      if (function.Documentation != null)
      {
        content.AppendLine($"  /// <summary>");
        var functionDocs = ParseFunctionDocsIntoXMLDocs(function.Documentation, api, (cref) =>
        {
          return $"{NativeGLFWClassName}.{cref}";
        })
        .FixNewlines().FixSpaces();
        if (!string.IsNullOrWhiteSpace(functionDocs))
          content.AppendLine($"  /// {functionDocs}");
        else if (function.SimpleDocumentation != null)
          content.AppendLine($"  /// {function.SimpleDocumentation}");
        content.AppendLine($"  /// </summary>");

        if (function.Name.EndsWith("Callback"))
        {
          content.AppendLine("  /// <remarks>");
          content.AppendLine("  /// WARNING! Be aware that the supplied callback is NOT cached in any way to prevent it from being garbage collected. You must do that yourself to prevent potential crashes due to glfw calling a garbage collected delegate.");
          content.AppendLine("  /// </remarks>");
        }
      }
      var libraryImportString = $"[LibraryImport(LIBRARY_DLL, EntryPoint = \"{function.Name}\")]";
      content.AppendLine($"  {libraryImportString}");
      content.AppendLine($"  [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]");
      content.Append($"  public static partial {returnType} {SanitizeIdentifierForCSharp(function.Name)}(");
      for (var i = 0; i < function.Parameters.Count; i++)
      {
        var (name, type) = function.Parameters[i];
        content.Append($"{ConvertToCSharpType(type, api)} {SanitizeIdentifierForCSharp(name)}");
        if (i < function.Parameters.Count - 1)
          content.Append(", ");
      }
      content.AppendLine(");");
      if (function.Name.EndsWith("Callback"))
      {
        content.AppendLine($"  internal static {returnType} _current{returnType};");
      }
      content.AppendLine();
    }

    content.AppendLine("}");

    _sourceWriter.WriteToFile($"{NativeGLFWClassName}.Functions.cs", content.ToString());
  }
}