using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterCodingExercise
{
    public class ExpressionProcessor
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        public class Token
        {
            public enum TokenTypeEnum { Integer, Addition, Subtraction, Other };
            public string Text;
            public TokenTypeEnum TokenType;

            public Token(string text, TokenTypeEnum tokenType)
            {
                Text = text;
                TokenType = tokenType;
            }
            public override string ToString()
            {
                return $" {Text} ";
            }
        }

        public List<Token> Lex(string expression)
        {
            var result = new List<Token>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (char.IsDigit(expression[i]))
                {
                    var sb = new StringBuilder(expression[i].ToString());
                    for (var j = i + 1; j < expression.Length; j++)
                    {
                        if (char.IsDigit(expression[j]))
                        {

                            sb.Append(expression[j].ToString());
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    result.Add(new Token(sb.ToString(), Token.TokenTypeEnum.Integer));
                }
                else
                {
                    result.Add(new Token(expression[i].ToString(), expression[i] == '+' ? Token.TokenTypeEnum.Addition : Token.TokenTypeEnum.Subtraction));
                }
            }
            return result;
        }



        public int Calculate(string expression)
        {
            for (int i = 0; i < expression.Length; i++)
            {
                if (char.IsLetter(expression[i]))
                {
                    if (i < expression.Length - 1 && !char.IsLetter(expression[i + 1])) return 0;
                    if (!Variables.ContainsKey(expression[i])) return 0;
                    var a = expression[i].ToString();
                    var b = Variables[expression[i]];
                    expression = expression.Replace(a, b.ToString());
                }
            }

            var parsed = Lex(expression);
            int currentOperand = 0, nextOperand = 0;
            bool haveCurrentOperand = false;
            Token.TokenTypeEnum currentAction = Token.TokenTypeEnum.Other;
            foreach (var t in parsed)
            {
                switch (t.TokenType)
                {
                    case Token.TokenTypeEnum.Integer:
                        if (!haveCurrentOperand)
                        {
                            currentOperand = int.Parse(t.Text);
                            haveCurrentOperand = true;
                        }
                        else
                        {
                            nextOperand = int.Parse(t.Text);

                            switch (currentAction)
                            {
                                case Token.TokenTypeEnum.Addition: currentOperand += nextOperand; currentAction = Token.TokenTypeEnum.Other; break;
                                case Token.TokenTypeEnum.Subtraction: currentOperand -= nextOperand; currentAction = Token.TokenTypeEnum.Other; break;
                                default: break;
                            }

                        }
                        break;
                    case Token.TokenTypeEnum.Addition:
                        currentAction = Token.TokenTypeEnum.Addition;
                        break;
                    case Token.TokenTypeEnum.Subtraction:
                        currentAction = Token.TokenTypeEnum.Subtraction;
                        break;
                }
            }

            return currentOperand;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            string expression = "1+2+3+ab";
            var ep = new ExpressionProcessor();
            ep.Variables.Add('x', 12);
            Console.WriteLine(ep.Calculate(expression));
            //var p = ep.Lex(expression);
            //foreach (var t in p) Console.WriteLine(t);

        }
    }
}
