using System;
using System.Linq;

namespace DotGLFW.Generator;

public class CppType
{
  public bool IsPointer => Name.EndsWith('*');
  public string Name { get; set; }
  public bool IsArray { get; set; }
  public int ArraySize { get; set; } = -1;

  public override string ToString()
  {
    return $"{Name}{(IsArray ? "[]" : "")}";
  }
}