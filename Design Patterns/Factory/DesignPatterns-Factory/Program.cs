using System;

namespace DesignPatterns_Factory
{
    public enum CoordinateSystem
    {
        Cartesian, Polar
    }

    public class PointFactory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    public class Point
    {
        private double x, y;

        // factory method design pattern



        public override string ToString()
        {
            return $"x:{x} y:{y}";
        }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }



        // public Point(double a, double b, CoordinateSystem system = CoordinateSystem.Cartesian)
        // {
        //     switch (system)
        //     {
        //         case CoordinateSystem.Cartesian:
        //             this.x = a;
        //             this.y = b;
        //             break;
        //         case CoordinateSystem.Polar:
        //             x = a * Math.Cos(b);
        //             y = a * Math.Sin(b);
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(system), system, null);
        //     }
        // }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Point p = PointFactory.NewPolarPoint(1, Math.PI / 2);
            Console.WriteLine(p);
        }
    }
}
