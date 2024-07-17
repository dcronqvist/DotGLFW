using System;
using System.Collections.Generic;
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
    content.AppendLine("  private static DllLoader.GetProcAddressDelegate __getProcAddressForGLFWFunction;");
    content.AppendLine("  private static T LoadFunction<T>(string name) where T : Delegate");
    content.AppendLine("  {");
    content.AppendLine("    if (__getProcAddressForGLFWFunction == null)");
    content.AppendLine("      __getProcAddressForGLFWFunction = DllLoader.GetLoadFunctionPointerDelegate(LIBRARY_DLL);");
    content.AppendLine("    return Marshal.GetDelegateForFunctionPointer<T>(__getProcAddressForGLFWFunction(name));");
    content.AppendLine("  }");
    content.AppendLine();

    foreach (var function in api.Functions)
    {
      if (function.Name.EndsWith("Callback"))
      {
        GenerateCallbackFunction(api, function, content);
      }
      else
      {
        GenerateNormalFunction(api, function, content);
      }
    }

    content.AppendLine("}");

    _sourceWriter.WriteToFile($"{NativeGLFWClassName}.Functions.gen.cs", content.ToString());
  }

  private void EmitParameters(StringBuilder content, ParsedAPI api, Function function, bool includeType = true, bool forceDelegatesToIntPtr = false)
  {
    EmitParameters(content, api, function.Parameters.Select(p => (p.Name, p.Type)).ToList(), includeType, forceDelegatesToIntPtr);
  }

  private void EmitParameters(StringBuilder content, ParsedAPI api, List<(string Name, CppType Type)> parameters, bool includeType = true, bool forceDelegatesToIntPtr = false)
  {
    for (var i = 0; i < parameters.Count; i++)
    {
      var (name, type) = parameters[i];

      if (includeType)
        content.Append($"{ConvertToCSharpType(type, api, forceDelegatesToIntPtr: forceDelegatesToIntPtr)} {SanitizeIdentifierForCSharp(name)}");
      else
        content.Append($"{SanitizeIdentifierForCSharp(name)}");
      if (i < parameters.Count - 1)
        content.Append(", ");
    }
  }

  private void GenerateNormalFunction(ParsedAPI api, Function function, StringBuilder content)
  {
    var returnType = ConvertToCSharpType(function.ReturnType, api);

    // Create the delegate
    content.AppendLine("  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]");
    content.Append($"  private delegate {returnType} d_{SanitizeIdentifierForCSharp(function.Name)}(");
    EmitParameters(content, api, function);
    content.AppendLine(");");

    // Create instance of the delegate by loading it using the DllLoader
    content.AppendLine($"  private static d_{SanitizeIdentifierForCSharp(function.Name)} p_{SanitizeIdentifierForCSharp(function.Name)} = LoadFunction<d_{SanitizeIdentifierForCSharp(function.Name)}>(\"{function.Name}\");");

    // Add docs if present
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
    }

    // Create the public facing function for library consumers
    content.Append($"  public static {returnType} {SanitizeIdentifierForCSharp(function.Name)}(");
    EmitParameters(content, api, function, includeType: true);
    content.Append($") => p_{SanitizeIdentifierForCSharp(function.Name)}(");
    EmitParameters(content, api, function, includeType: false);
    content.AppendLine(");");
    content.AppendLine();
  }

  private void GenerateCallbackFunction(ParsedAPI api, Function function, StringBuilder content)
  {
    var delegateReturnType = ConvertToCSharpType(function.ReturnType, api, forceDelegatesToIntPtr: true);
    var managedReturnType = ConvertToCSharpType(function.ReturnType, api);

    // Create the delegate
    content.AppendLine("  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]");
    content.Append($"  private delegate {delegateReturnType} d_{SanitizeIdentifierForCSharp(function.Name)}(");
    EmitParameters(content, api, function, forceDelegatesToIntPtr: true);
    content.AppendLine(");");

    // Create instance of the delegate by loading it using the DllLoader
    content.AppendLine($"  private static d_{SanitizeIdentifierForCSharp(function.Name)} p_{SanitizeIdentifierForCSharp(function.Name)} = LoadFunction<d_{SanitizeIdentifierForCSharp(function.Name)}>(\"{function.Name}\");");

    // Add docs if present
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
      content.AppendLine("  /// <remarks>");
      content.AppendLine("  /// WARNING! Be aware that the supplied callback is NOT cached in any way to prevent it from being garbage collected. You must do that yourself to prevent potential crashes due to glfw calling a garbage collected delegate.");
      content.AppendLine("  /// </remarks>");
    }

    var callbackParameter = function.Parameters.First(x =>
    {
      var type = x.Type;
      return type.Name.Replace("*", "").EndsWith("fun");
    });
    var callbackParameterName = SanitizeIdentifierForCSharp(callbackParameter.Name);

    // Create the public facing function for library consumers
    content.Append($"  public static {managedReturnType} {SanitizeIdentifierForCSharp(function.Name)}(");
    EmitParameters(content, api, function, includeType: true);
    content.AppendLine(")");
    content.AppendLine("  {");

    // Marshal parameters into native types
    var marshalled = EmitMarshalledParametersAndReturnMap(api, content, function.Parameters);
    var callbackedMarshalled = marshalled[callbackParameterName];

    content.AppendLine($"    {managedReturnType} __{callbackedMarshalled}_managed;");
    content.AppendLine($"    System.IntPtr __{callbackedMarshalled}_retVal_native;");
    content.AppendLine("    {");
    content.Append($"      __{callbackedMarshalled}_retVal_native = p_{SanitizeIdentifierForCSharp(function.Name)}(");
    EmitParameters(content, api,
      function.Parameters.Select(p => (marshalled[SanitizeIdentifierForCSharp(p.Name)], p.Type)).ToList(),
      includeType: false);
    content.AppendLine(");");
    content.AppendLine("    }");
    content.AppendLine($"    System.GC.KeepAlive({callbackedMarshalled});");
    content.AppendLine($"    __{callbackedMarshalled}_managed = __{callbackedMarshalled}_retVal_native != default ? Marshal.PtrToStructure<{managedReturnType}>(__{callbackedMarshalled}_retVal_native) : null;");
    content.AppendLine($"    return __{callbackedMarshalled}_managed;");
    content.AppendLine("  }");

    // Create internal static variable to hold the current callback
    content.AppendLine($"  internal static {managedReturnType} _current{managedReturnType};");
  }

  private IReadOnlyDictionary<string, string> EmitMarshalledParametersAndReturnMap(
    ParsedAPI api,
    StringBuilder content,
    List<FunctionParameter> parameters)
  {
    var map = parameters.Select(p => (SanitizeIdentifierForCSharp(p.Name), EmitAndMarshalParameter(api, content, p)));
    return map.ToDictionary(x => x.Item1, x => x.Item2);
  }

  private string EmitAndMarshalParameter(
    ParsedAPI api,
    StringBuilder content,
    FunctionParameter parameter)
  {
    var convertedType = ConvertToCSharpType(parameter.Type, api, forceDelegatesToIntPtr: true);

    switch (convertedType)
    {
      case "IntPtr":
        if (parameter.Type.Name.Replace("*", "").EndsWith("fun"))
        {
          return EmitAndMarshalParameterAsDelegate(api, content, parameter);
        }
        else
        {
          content.Append($"{SanitizeIdentifierForCSharp(parameter.Name)}");
          return $"{SanitizeIdentifierForCSharp(parameter.Name)}";
        }
      default:
        return SanitizeIdentifierForCSharp(parameter.Name);
    }
  }

  private string EmitAndMarshalParameterAsDelegate(
    ParsedAPI api,
    StringBuilder content,
    FunctionParameter parameter)
  {
    var parameterName = SanitizeIdentifierForCSharp(parameter.Name);
    var parameterNativeName = $"__{parameterName}_native";

    content.AppendLine($"    System.IntPtr {parameterNativeName};");
    content.AppendLine($"    {parameterNativeName} = {parameterName} != null ? Marshal.GetFunctionPointerForDelegate({parameterName}) : default;");
    return parameterNativeName;
  }
}