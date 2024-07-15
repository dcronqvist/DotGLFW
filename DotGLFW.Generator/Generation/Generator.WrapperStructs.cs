using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotGLFW.Generator;

public partial class Generator
{
  public void GenerateWrapperStructs(ParsedAPI api)
  {
    var opaqueHandleStructs = GetOpaqueHandleStructs(api);
    opaqueHandleStructs.ToList().ForEach(GenerateOpaqueHandleStruct);

    var normalStructs = GetNormalStructs(api);
    normalStructs.ToList().ForEach(structTypeDef => GenerateNormalStruct(structTypeDef, api));
  }

  private IEnumerable<StructTypeDef> GetOpaqueHandleStructs(ParsedAPI api)
  {
    return api.TypeDefs.Where(typeDef =>
      typeDef is StructTypeDef structTypeDef &&
      structTypeDef.Fields.Count == 0).Cast<StructTypeDef>();
  }

  private IEnumerable<StructTypeDef> GetNormalStructs(ParsedAPI api)
  {
    return api.TypeDefs.Where(typeDef =>
      typeDef is StructTypeDef structTypeDef &&
      structTypeDef.Fields.Count > 0).Cast<StructTypeDef>();
  }

  private void GenerateOpaqueHandleStruct(StructTypeDef structTypeDef)
  {
    var name = SanitizeIdentifierForCSharp(structTypeDef.Name);
    var nameWithoutGLFW = name.Replace("GLFW", "");
    var capitalizedName = nameWithoutGLFW.Capitalize();

    var content = new StringBuilder();
    WriteFileHeader(content);

    content.AppendLine("using System.Runtime.InteropServices;");
    content.AppendLine();
    content.AppendLine("namespace DotGLFW;");
    content.AppendLine();
    content.AppendLine($"/// <summary>");
    content.AppendLine($"/// An opaque handle to a GLFW {nameWithoutGLFW}.");
    content.AppendLine($"/// </summary>");
    content.AppendLine($"public partial class {capitalizedName}");
    content.AppendLine("{");
    content.AppendLine($"  internal readonly IntPtr _handle;");
    content.AppendLine();
    content.AppendLine($"  internal {capitalizedName}(IntPtr handle) => _handle = handle;");
    content.AppendLine();
    content.AppendLine($"  /// <summary>");
    content.AppendLine($"  /// A NULL {nameWithoutGLFW} handle. Often used for default values or error handling.");
    content.AppendLine($"  /// </summary>");
    content.AppendLine($"  public static readonly {capitalizedName} NULL = new {capitalizedName}(IntPtr.Zero);");
    content.AppendLine();
    content.AppendLine($"  /// <summary>");
    content.AppendLine($"  /// Performs equality check against another {nameWithoutGLFW} handle.");
    content.AppendLine($"  /// </summary>");
    content.AppendLine($"  public override bool Equals(object other)");
    content.AppendLine($"  {{");
    content.AppendLine($"    if (ReferenceEquals(null, other)) return false;");
    content.AppendLine($"    if (ReferenceEquals(this, other)) return true;");
    content.AppendLine();
    content.AppendLine($"    if (other is {capitalizedName} {nameWithoutGLFW})");
    content.AppendLine($"      return _handle.Equals({nameWithoutGLFW}._handle);");
    content.AppendLine();
    content.AppendLine($"    return false;");
    content.AppendLine($"  }}");
    content.AppendLine();
    content.AppendLine($"  /// <summary>");
    content.AppendLine($"  /// Simple hash code implementation that uses the underlying pointer.");
    content.AppendLine($"  /// </summary>");
    content.AppendLine($"  public override int GetHashCode() => _handle.GetHashCode();");
    content.AppendLine();
    content.AppendLine($"  /// <summary>");
    content.AppendLine($"  /// Returns the underlying pointer.");
    content.AppendLine($"  /// </summary>");
    content.AppendLine($"  public IntPtr GetHandle() => _handle;");
    content.AppendLine();
    content.AppendLine($"  /// <summary>");
    content.AppendLine($"  /// Performs equality check against another {nameWithoutGLFW} handle.");
    content.AppendLine($"  /// </summary>");
    content.AppendLine($"  public static bool operator ==({capitalizedName} left, {capitalizedName} right) => left.Equals(right);");
    content.AppendLine();
    content.AppendLine($"  /// <summary>");
    content.AppendLine($"  /// Performs inequality check against another {nameWithoutGLFW} handle.");
    content.AppendLine($"  /// </summary>");
    content.AppendLine($"  public static bool operator !=({capitalizedName} left, {capitalizedName} right) => !left.Equals(right);");
    content.AppendLine();
    content.AppendLine($"  /// <summary>");
    content.AppendLine($"  /// Implicit conversion to {name}* handle that is used by Native GLFW functions.");
    content.AppendLine($"  /// </summary>");
    content.AppendLine($"  public static unsafe implicit operator NativeGlfw.{name}*({capitalizedName} managed) => (NativeGlfw.{name}*)(managed?._handle ?? NULL._handle);");
    content.AppendLine("}");

    _sourceWriter.WriteToFile($"Struct.{capitalizedName}.gen.cs", content.ToString());
  }

  private void GenerateNormalStruct(StructTypeDef structTypeDef, ParsedAPI api)
  {
    var name = SanitizeIdentifierForCSharp(structTypeDef.Name);
    var nameWithoutGLFW = name.Replace("GLFW", "");
    var capitalizedName = nameWithoutGLFW.Capitalize();

    var content = new StringBuilder();
    WriteFileHeader(content);

    content.AppendLine("using System.Runtime.InteropServices;");
    content.AppendLine("using static DotGLFW.NativeGlfw;");
    content.AppendLine();
    content.AppendLine("namespace DotGLFW;");
    content.AppendLine();
    content.AppendLine($"/// <inheritdoc cref=\"NativeGlfw.{name}\"/>");
    content.AppendLine($"public partial class {capitalizedName}");
    content.AppendLine("{");
    foreach (var field in structTypeDef.Fields)
    {
      var normalFieldName = SanitizeIdentifierForCSharp(field.Name);
      var capitalizedField = normalFieldName.Capitalize();

      content.AppendLine($"  /// <inheritdoc cref=\"NativeGlfw.{name}.{field.Name}\"/>");
      content.AppendLine($"  public {ConvertToCSharpType(field.Type, api, preferArrayOverPointer: true)} {capitalizedField} {{ get; init; }}");
    }
    content.AppendLine("}");

    _sourceWriter.WriteToFile($"Struct.{capitalizedName}.gen.cs", content.ToString());
  }
}