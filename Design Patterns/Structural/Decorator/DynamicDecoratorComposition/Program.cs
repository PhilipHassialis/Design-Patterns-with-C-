using System;

namespace DynamicDecoratorComposition
{
    class Program
    {
        public interface IShape
        {
            string AsString();
        }

        public class Circle: IShape
        {
            private float radius;

            public Circle(float radius)
            {
                this.radius = radius;
            }

            public string AsString() =>  $"A circle with {radius}";
            

            public void Resize(float factor) { radius *= factor; }
        }


        public class Square: IShape
        {
            private float side;

            public Square(float side)
            {
                this.side = side;
            }

            public string AsString() => $"A square with side {side}";
        }

        public class ColoredShape : IShape
        {
            private IShape shape;
            private string color;

            public ColoredShape(IShape shape, string color)
            {
                this.shape = shape;
                this.color = color;
            }

            public string AsString() => $"A colored shape {shape.AsString()} with color {color}";
        }

        public class TransparentShape: IShape
        {
            private float transparency;
            private IShape shape;

            public TransparentShape(IShape shape, float transparency )
            {
                this.transparency = transparency;
                this.shape = shape;
            }

            public string AsString() => $"{shape.AsString()} has transparency {transparency * 100.0}%";
        }

        static void Main(string[] args)
        {
            var sq = new Square(1.2f);
            var c = new Circle(3f);

            var redSquare = new ColoredShape(sq, "red");
            Console.WriteLine(redSquare.AsString());

            var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
            Console.WriteLine(redHalfTransparentSquare.AsString());
        }
    }
}
