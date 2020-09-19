using System;

namespace Coding.Exercise
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        int squareSide;

        public SquareToRectangleAdapter(Square square)
        {
            // todo
            squareSide = square.Side;
        }

        public int Width => squareSide;

        public int Height => squareSide;
        // todo
    }

    public class AdapterCodingExercise
    {
        public static void Main()
        {
            System.Console.WriteLine("hi");
        }
    }
}