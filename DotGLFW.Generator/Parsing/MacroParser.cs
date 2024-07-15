using System;
using System.Collections.Generic;
using System.Linq;

namespace DotGLFW.Generator;

public class MacroParser : BaseParser<Macro>
{
  public MacroParser(IEnumerable<Token> tokens) : base(tokens) { }

  public override Macro Parse()
  {
    Consume(Token.TokenType.Hashtag);
    Consume(Token.TokenType.Identifier, token => token.Value == "define");

    var name = Consume(Token.TokenType.Identifier).Value;

    if (TryConsume(Token.TokenType.EndOfInput, out _))
    {
      return new Macro
      {
        Name = name,
        Value = null
      };
    }

    var tokens = GetAllTokens();
    var position = GetPosition();
    var value = tokens.Skip(position + 1).SkipLast(1)
      .Where(token => token.Type != Token.TokenType.LineComment && token.Type != Token.TokenType.BlockComment)
      .Select(token => token.Value).Aggregate((a, b) => a + b);

    return new Macro
    {
      Name = name,
      Value = value == string.Empty ? null : value
    };
  }
}