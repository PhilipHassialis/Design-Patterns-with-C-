using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace HandmadeInterpreter
{
    public interface IElement
    {
        int Value { get; }
    }

    public class InterpretedInteger : IElement
    {
        public InterpretedInteger(int value)
        {
            Value = value;
        }
        public int Value { get; }
    }

    public class BinaryOperation : IElement
    {
        public enum Type
        {
            Addition, Subtraction
        }

        public Type MyTpe;
        public IElement Left, Right;
        public int Value
        {
            get
            {
                switch (MyTpe)
                {
                    case Type.Addition:
                        return Left.Value + Right.Value;
                    case Type.Subtraction:
                        return Left.Value - Right.Value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

    }

    public class Token
    {
        public enum Type
        {
            Integer, Plus, Minus, LParen, RParen
        };

        public Type MyType;
        public string Text;

        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text;
        }

        public override string ToString()
        {
            return $"`{Text}`";
        }


    }

    class Program
    {
        static IElement Parse(IReadOnlyList<Token> tokens)
        {
            var result = new BinaryOperation();
            bool haveLHS = false;
            for (int i = 0; i <tokens.Count; i++ )
            {
                var token = tokens[i];
                switch (token.MyType)
                {
                    case Token.Type.Integer:
                        var integer = new InterpretedInteger(int.Parse(token.Text));
                        if (!haveLHS)
                        {
                            result.Left = integer;
                            haveLHS = true;
                        }
                        else result.Right = integer;
                        break;
                    case Token.Type.Plus:
                        result.MyTpe = BinaryOperation.Type.Addition;
                        break;
                    case Token.Type.Minus:
                        result.MyTpe = BinaryOperation.Type.Subtraction;
                        break;
                    case Token.Type.LParen:
                        int j = i;
                        for (;j<tokens.Count; ++j)
                            if (tokens[j].MyType == Token.Type.RParen) break;
                        var subexpression = tokens.Skip(i + 1).Take(j - i - 1).ToList();
                        var element = Parse(subexpression);
                        if (!haveLHS)
                        {
                            result.Left = element;
                            haveLHS = true;
                        }
                        else result.Right = element;
                        i = j;
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        static List<Token> Lex(string input)
        {
            var result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    case '(':
                        result.Add(new Token(Token.Type.LParen, "("));
                        break;
                    case ')':
                        result.Add(new Token(Token.Type.RParen, ")"));
                        break;
                    default:
                        var sb = new StringBuilder();
                        sb.Append(input[i]);
                        for (int j = i + 1; j < input.Length; j++)
                        {
                            if (char.IsDigit(input[j]))
                            {
                                sb.Append(input[j]);
                                i++;
                            }
                            else
                            {
                                result.Add(new Token(Token.Type.Integer, sb.ToString()));
                                break;
                            }
                        }
                        break;
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            string input = "(13+6)-(19+1)";
            var tokens = Lex(input);

            Console.WriteLine(string.Join("\t", tokens));
            var parsed = Parse(tokens);
            Console.WriteLine($"{input} yields {parsed.Value}");
        }
    }
}
