using System;
using System.Numerics;

namespace StrategyCodingExercise
{

    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo
        public double CalculateDiscriminant(double a, double b, double c)
        {
            return b * b - 4 * a * c;
        }
    }

    public class RealDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo (return NaN on negative discriminant!)
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0) return double.NaN; else return discriminant;
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy strategy;

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            //var discriminant = strategy.CalculateDiscriminant(a, b, c);
            //if (discriminant == double.NaN)
            //    return new Tuple<Complex, Complex>(new Complex(double.NaN, double.NaN), new Complex(double.NaN, double.NaN));
            //else
            //{
            //    Complex solution1 = new Complex();
            //    Complex solution2 = new Complex();

            //    double squareRootPart=0;
            //    if (discriminant < 0)
            //    {
            //        squareRootPart = Math.Sqrt(-1 * discriminant);
            //        solution1 = new Complex((-1 * b) / (2 * a), -1 * squareRootPart / (2 * a));
            //        solution2 = new Complex((-1 * b) / (2 * a), squareRootPart / (2 * a));
            //    }
            //    else
            //    {
            //        squareRootPart = Math.Sqrt(discriminant);
            //        solution1 = new Complex(((-1 * b) - squareRootPart) / (2 * a), 0);
            //        solution2 = new Complex(((-1 * b) + squareRootPart) / (2 * a), 0);

            //    }

            //    return new Tuple<Complex, Complex>(solution1, solution2);
            //}

            var disc = new Complex(strategy.CalculateDiscriminant(a, b, c), 0);
            var rootDisc = Complex.Sqrt(disc);
            return Tuple.Create(
              (-b + rootDisc) / (2 * a),
              (-b - rootDisc) / (2 * a)
            );
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
