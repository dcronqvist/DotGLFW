namespace DotGLFW.Generator;

public class Macro
{
  public string Name { get; set; }
  public string Documentation { get; set; }
  public CppType Type { get; set; }
  public string Value { get; set; }

  public bool TryGetValue(out string value)
  {
    value = Value;
    return Value != null;
  }
}
