using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace DotGLFW.Generator;

public class TypeDefParser : BaseParser<ITypeDef>
{
  public TypeDefParser(IEnumerable<Token> tokens) : base(tokens) { }

  public override ITypeDef Parse()
  {
    Consume(Token.TokenType.Identifier, token => token.Value == "typedef");

    if (TryConsume(Token.TokenType.Identifier, token => token.Value == "struct", out var token))
    {
      return ParseStruct();
    }

    // Assume delegate
    var returnType = ParseType();

    Consume(Token.TokenType.LeftParen);
    Consume(Token.TokenType.Asterisk);

    var name = ParseIdentifier();

    Consume(Token.TokenType.RightParen);

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

    return new DelegateTypeDef
    {
      ReturnType = returnType,
      Name = name,
      Parameters = parameters
    };
  }

  private StructTypeDef ParseStruct()
  {
    var structName = ParseIdentifier();
    var name = ParseIdentifier();

    return new StructTypeDef
    {
      Name = structName
    };
  }

  public class StructFieldParser : BaseParser<StructTypeField>
  {
    public StructFieldParser(IEnumerable<Token> tokens) : base(tokens) { }

    public override StructTypeField Parse()
    {
      var type = ParseType();
      var name = ParseIdentifier();

      return new StructTypeField
      {
        Name = name,
        Type = type
      };
    }
  }
}