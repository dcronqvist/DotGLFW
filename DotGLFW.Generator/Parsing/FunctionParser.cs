using System;
using System.Collections.Generic;
using System.Linq;

namespace DotGLFW.Generator;

public class FunctionParser : BaseParser<Function>
{
  public FunctionParser(IEnumerable<Token> tokens) : base(tokens) { }

  public override Function Parse()
  {
    var returnType = ParseType();
    var name = ParseIdentifier();

    Consume(Token.TokenType.LeftParen);

    var parameters = new List<(string Name, CppType Type)>();
    while (GetPosition() < GetAllTokens().Count() - 1 && !TryConsume(Token.TokenType.RightParen, out _))
    {
      var parameterType = ParseType();

      if (TryConsume(Token.TokenType.RightParen, out _))
      {
        parameters.Add((string.Empty, parameterType));
        break;
      }

      var parameterName = ParseIdentifier();
      parameters.Add((parameterName, parameterType));

      if (TryConsume(Token.TokenType.Comma, out _))
        continue;
    }

    if (parameters.Count == 1 && parameters[0].Type.Name == "void")
      parameters.Clear();

    return new Function
    {
      ReturnType = returnType,
      Name = name,
      Parameters = parameters
    };
  }
}