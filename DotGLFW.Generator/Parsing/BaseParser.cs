using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace DotGLFW.Generator;

public abstract class BaseParser<T>
{
  private readonly Token[] _tokens;
  private int _position = -1;

  protected BaseParser(IEnumerable<Token> tokens)
  {
    _tokens = tokens.ToArray();
  }

  public abstract T Parse();

  protected IEnumerable<Token> GetAllTokens()
  {
    return _tokens;
  }

  protected int GetPosition()
  {
    return _position;
  }

  protected void Regress(int count = 1)
  {
    _position -= count;
  }

  protected Token Consume(Token.TokenType type)
  {
    if (_position + 1 >= _tokens.Length)
      throw new System.Exception("Unexpected end of input");

    var token = _tokens[_position + 1];
    if (token.Type != type)
      throw new System.Exception($"Expected token of type {type}, but got {token.Type}");

    _position++;
    return token;
  }

  protected Token Consume(Token.TokenType type, Predicate<Token> predicate)
  {
    if (_position + 1 >= _tokens.Length)
      throw new System.Exception("Unexpected end of input");

    var token = _tokens[_position + 1];
    if (token.Type != type || !predicate(token))
      throw new System.Exception($"Expected token of type {type}, but got {token.Type}");

    _position++;
    return token;
  }

  protected bool TryConsume(Token.TokenType type, out Token token)
  {
    if (_position + 1 >= _tokens.Length)
    {
      token = null;
      return false;
    }

    var nextToken = _tokens[_position + 1];
    if (nextToken.Type != type)
    {
      token = null;
      return false;
    }

    _position++;
    token = nextToken;
    return true;
  }

  protected bool TryConsume(Token.TokenType type, Predicate<Token> predicate, out Token token)
  {
    if (_position + 1 >= _tokens.Length)
    {
      token = null;
      return false;
    }

    var nextToken = _tokens[_position + 1];
    if (nextToken.Type != type || !predicate(nextToken))
    {
      token = null;
      return false;
    }

    _position++;
    token = nextToken;
    return true;
  }

  protected CppType ParseType()
  {
    string[] typePrefixes = [
      "unsigned",
      "const"
    ];

    var foundPrefixes = new List<string>();
    while (TryConsume(Token.TokenType.Identifier, token => typePrefixes.Contains(token.Value), out var token))
    {
      foundPrefixes.Add(token.Value);
    }

    var finalType = Consume(Token.TokenType.Identifier);
    int asterisks = 0;
    while (TryConsume(Token.TokenType.Asterisk, out _))
    {
      asterisks++;
    }
    bool isPointer = asterisks > 0;
    bool isArray = false;
    int arraySize = -1;

    // Check if it is array (which is after identifier)
    if (TryConsume(Token.TokenType.Identifier, out _))
    {
      if (TryConsume(Token.TokenType.LeftBracket, out _))
      {
        if (TryConsume(Token.TokenType.LiteralDecimal, out var size))
        {
          arraySize = int.Parse(size.Value);
        }
        else if (TryConsume(Token.TokenType.LiteralHexadecimal, out var hexSize))
        {
          arraySize = int.Parse(hexSize.Value.Substring(2), System.Globalization.NumberStyles.HexNumber);
        }

        Consume(Token.TokenType.RightBracket);
        isArray = true;
        if (arraySize != -1)
        {
          Regress();
        }
        Regress();
        Regress();
      }
      Regress();
    }

    string prefixString = string.Join(" ", foundPrefixes) + (foundPrefixes.Count > 0 ? " " : string.Empty);
    string fullType = $"{prefixString}{finalType.Value}{(isPointer ? "*".Repeat(asterisks) : string.Empty)}";

    return new CppType
    {
      Name = fullType,
      IsArray = isArray,
      ArraySize = arraySize
    };
  }

  protected string ParseIdentifier()
  {
    string identifier = Consume(Token.TokenType.Identifier).Value;
    TryConsume(Token.TokenType.LeftBracket, out _);
    TryConsume(Token.TokenType.LiteralDecimal, out _);
    TryConsume(Token.TokenType.LiteralHexadecimal, out _);
    TryConsume(Token.TokenType.RightBracket, out _);
    return identifier;
  }
}