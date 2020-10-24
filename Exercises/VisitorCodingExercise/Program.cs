using System;
using System.Text;

namespace VisitorCodingExercise
{
    public abstract class ExpressionVisitor
    {
        public StringBuilder sb = new StringBuilder();
        public virtual void Visit(Value v) { }
        public virtual void Visit(AdditionExpression ae) { }
        public virtual void Visit(MultiplicationExpression me) { }

        public virtual void Visit(Expression e) { }
    }

    public abstract class Expression
    {
        public abstract void Accept(ExpressionVisitor ev);
    }

    public class Value : Expression
    {
        public readonly int TheValue;

        public Value(int value)
        {
            TheValue = value;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }

        // todo
    }

    public class AdditionExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public AdditionExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }

        // todo
    }

    public class MultiplicationExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public MultiplicationExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }

        // todo
    }

    public class ExpressionPrinter : ExpressionVisitor
    {
        
        public override void Visit(Value value)
        {
            sb.Append(value.TheValue);
        }

        public override void Visit(AdditionExpression ae)
        {
            sb.Append("(");
            ae.LHS.Accept(this);
            sb.Append("+");
            ae.RHS.Accept(this);
            sb.Append(")");

        }

        public override void Visit(MultiplicationExpression me)
        {
            me.LHS.Accept(this);
            sb.Append("*");
            me.RHS.Accept(this);
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var e = new MultiplicationExpression(
                new Value(12),
                new AdditionExpression(
                    new Value(3),
                    new MultiplicationExpression(
                        new Value(4),
                        new Value(9)
                        )
                    )
                );
            var ep = new ExpressionPrinter();
            ep.Visit(e);
            Console.WriteLine(ep.ToString());
        }
    }
}
