using System;
using System.Linq;

namespace DotGLFW.Generator;

public static partial class Extensions
{
  public static string Remove(this string str, string toRemove)
  {
    return str.Replace(toRemove, "");
  }

  public static string FixSpaces(this string str)
  {
    if (!str.Contains(" "))
    {
      return str;
    }

    if (!str.Split(" ").Where(x => x != "").Any())
    {
      return str;
    }

    return str.Split(" ").Where(x => x != "").Aggregate((x, y) => $"{x} {y}");
  }

  public static string FixNewlines(this string str)
  {
    if (!str.Contains("\n"))
    {
      return str;
    }

    if (!str.Split("\n").Where(x => x != "").Any())
    {
      return str;
    }

    return str.Split("\n").Where(x => x != "").Aggregate((x, y) => $"{x} {y}");
  }

  public static int Count(this string str, char c)
  {
    return str.Count(x => x == c);
  }

  public static string Repeat(this string str, int count)
  {
    return string.Concat(Enumerable.Repeat(str, count));
  }

  public static string Capitalize(this string str)
  {
    if (str.Length <= 1)
      return str.ToUpper();

    return char.ToUpper(str[0]) + str.Substring(1);
  }
}
