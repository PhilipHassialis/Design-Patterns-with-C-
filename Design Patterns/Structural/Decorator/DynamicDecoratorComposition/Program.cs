using System;

namespace DynamicDecoratorComposition
{
    class Program
    {
        public abstract class Shape
        {
            public abstract string AsString();
        }

        public class Circle : Shape
        {
            private float radius;

            public Circle() : this(0)
            {}

            public Circle(float radius)
            {
                this.radius = radius;
            }

            public override string AsString() =>  $"A circle with {radius}";
            

            public void Resize(float factor) { radius *= factor; }
        }


        public class Square: Shape
        {
            private float side;

            public Square() : this(0)
            {
            }

            public Square(float side)
            {
                this.side = side;
            }

            public override string AsString() => $"A square with side {side}";
        }

        public class ColoredShape : Shape
        {
            private Shape shape;
            private string color;

            public ColoredShape(Shape shape, string color)
            {
                this.shape = shape;
                this.color = color;
            }

            public override string AsString() => $"A colored shape {shape.AsString()} with color {color}";
        }

        public class TransparentShape: Shape
        {
            private float transparency;
            private Shape shape;

            public TransparentShape(Shape shape, float transparency )
            {
                this.transparency = transparency;
                this.shape = shape;
            }

            public override string  AsString() => $"{shape.AsString()} has transparency {transparency * 100.0}%";
        }

        public class ColoredShape<T> : Shape where T : Shape, new()
        {
            private string color;
            private T shape = new T();

            public ColoredShape(string color)
            {
                this.color = color;
            }

            public ColoredShape():this("black")
            {

            }

            public override string AsString() => $"A colored shape {shape.AsString()} with color {color}";
        }

        public class TransparentShape<T> : Shape where T : Shape, new()
        {
            private float transparency;
            private T shape = new T();

            public TransparentShape(float transparency)
            {
                this.transparency = transparency;
            }

            public TransparentShape() : this(0)
            {

            }

            public override string AsString() => $"A transparent shape {shape.AsString()} with transparency {transparency*100}%";
        }

        static void Main(string[] args)
        {
            //var sq = new Square(1.2f);
            //var c = new Circle(3f);

            //var redSquare = new ColoredShape(sq, "red");
            //Console.WriteLine(redSquare.AsString());

            //var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
            //Console.WriteLine(redHalfTransparentSquare.AsString());

            var redSquare = new ColoredShape<Square>();
            Console.WriteLine(redSquare.AsString());

            var circle = new TransparentShape<ColoredShape<Circle>>(0.4f);
            Console.WriteLine(circle.AsString());


        }
    }
}
