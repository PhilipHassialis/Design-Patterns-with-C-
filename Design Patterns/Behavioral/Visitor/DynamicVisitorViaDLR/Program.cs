using System;
using System.Text;

namespace DynamicVisitorViaDLR
{
    
    public abstract class Expression
    {
    }

    public class DoubleExpression : Expression
    {
        public double Value;
        public DoubleExpression(double value)
        {
            this.Value = value;
        }

    }

    public class AdditionExpression : Expression
    {
        public Expression Left, Right;

        public AdditionExpression(Expression left, Expression right)
        {
            this.Left = left;
            this.Right = right;
        }

    }

    public class ExpressionBuilder
    {
        public void Print(AdditionExpression ae, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic)ae.Left, sb);
            sb.Append("+");
            Print((dynamic)ae.Right, sb);
            sb.Append(")");

        }
        public void Print(DoubleExpression de, StringBuilder sb)
        {
            sb.Append(de.Value);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Expression e = new AdditionExpression(
                new DoubleExpression(7),
                new AdditionExpression(
                    new DoubleExpression(8),
                    new DoubleExpression(9)));
            var ep = new ExpressionBuilder();
            var sb = new StringBuilder();
            ep.Print((dynamic)e, sb);
            Console.WriteLine(sb.ToString());
        }
    }
}
