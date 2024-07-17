using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DotGLFW.Generator;

public class Token
{
  public enum TokenType
  {
    Identifier,
    LiteralHexadecimal,
    LiteralDecimal,
    LiteralString,
    Asterisk,
    Equal,
    Semicolon,
    LeftParen,
    RightParen,
    Comma,
    Hashtag,
    Pipe,
    LineComment,
    BlockComment,
    LeftBracket,
    RightBracket,
    EndOfInput
  }

  public TokenType Type { get; }
  public string Value { get; }

  public Token(TokenType type, string value)
  {
    Type = type;
    Value = value;
  }
}

public interface ILexer
{
  IEnumerable<Token> Lex();
}

public class Lexer : ILexer
{
  private readonly string _input;
  private int _position;

  public Lexer(string input)
  {
    _input = input;
  }

  // Examples
  // #define GLFW_VERSION_MAJOR          3
  // #define GLFW_ANY_CURSOR             -1
  // #define GLFW_TEST                   0x20000000

  public IEnumerable<Token> Lex()
  {
    _position = 0;

    while (_position < _input.Length)
    {
      while (_position < _input.Length && char.IsWhiteSpace(Current()))
        _position++;

      if (_position >= _input.Length)
        break;

      var token = MakeToken();
      if (token != null)
        yield return token;
    }

    yield return new Token(Token.TokenType.EndOfInput, string.Empty);
  }

  private bool IsAlphaNumeric(char c) =>
    char.IsLetterOrDigit(c) || c == '_';

  private bool IsDigit(char c) =>
    char.IsDigit(c);

  private Token MakeLineComment()
  {
    var start = _position;
    while (_position < _input.Length && Current() != '\n')
      _position++;

    return new Token(Token.TokenType.LineComment, _input.Substring(start, _position - start));
  }

  private Token MakeBlockComment()
  {
    var start = _position;
    while (_position < _input.Length)
    {
      if (Current() == '*' && Peek(1, '/'))
      {
        _position += 2;
        return new Token(Token.TokenType.BlockComment, _input.Substring(start, _position - start));
      }

      _position++;
    }

    throw new Exception("Unterminated block comment");
  }

  private bool Match(char c)
  {
    if (_position >= _input.Length)
      return false;

    if (_input[_position] == c)
    {
      _position++;
      return true;
    }

    return false;
  }

  private bool Peek(int n, char c)
  {
    if (_position + n >= _input.Length)
      return false;

    return _input[_position + n] == c;
  }

  private char Current()
  {
    if (_position >= _input.Length)
      return ' ';

    return _input[_position];
  }

  private Token MakeToken()
  {
    if (Peek(0, '/') && Peek(1, '/'))
      return MakeLineComment();

    if (Peek(0, '/') && Peek(1, '*'))
      return MakeBlockComment();

    if (IsDigit(Current()) || Peek(0, '-'))
      return MakeNumber();

    if (IsAlphaNumeric(Current()))
      return MakeIdentifier();

    var simpleTokens = new Dictionary<char, Token.TokenType>
    {
      { '*', Token.TokenType.Asterisk },
      { '=', Token.TokenType.Equal },
      { ';', Token.TokenType.Semicolon },
      { '(', Token.TokenType.LeftParen },
      { ')', Token.TokenType.RightParen },
      { ',', Token.TokenType.Comma },
      { '#', Token.TokenType.Hashtag },
      { '|', Token.TokenType.Pipe },
      { '[', Token.TokenType.LeftBracket },
      { ']', Token.TokenType.RightBracket }
    };

    var current = Current();
    if (simpleTokens.TryGetValue(Current(), out var type))
    {
      Match(Current());
      return new Token(type, current.ToString());
    }

    throw new Exception($"Unexpected character: {Current()}");
  }

  private Token MakeNumber()
  {
    string value = string.Empty;
    if (Peek(0, '0') && Peek(1, 'x'))
    {
      Match('0');
      Match('x');

      while (_position < _input.Length && IsDigit(Current()))
      {
        value += Current();
        _position++;
      }

      return new Token(Token.TokenType.LiteralHexadecimal, "0x" + value);
    }
    else if (Peek(0, '-'))
    {
      Match('-');
      value += '-';

      while (_position < _input.Length && IsDigit(Current()))
      {
        value += Current();
        _position++;
      }

      return new Token(Token.TokenType.LiteralDecimal, value);
    }
    else
    {
      while (_position < _input.Length && IsDigit(Current()))
      {
        value += Current();
        _position++;
      }

      return new Token(Token.TokenType.LiteralDecimal, value);
    }
  }

  private Token MakeIdentifier()
  {
    var start = _position;
    while (_position < _input.Length && IsAlphaNumeric(Current()))
      _position++;

    return new Token(Token.TokenType.Identifier, _input.Substring(start, _position - start));
  }
}