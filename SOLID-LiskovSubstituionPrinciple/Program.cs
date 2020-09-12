using System;

namespace SOLID_LiskovSubstituionPrinciple
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public Rectangle() { }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width} - {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }
        public override int Height
        {
            set { base.Height = base.Width = value; }
        }


    }

    class Program
    {
        static public int Area(Rectangle rc) => rc.Width * rc.Height;
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2, 9);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 6;
            Console.WriteLine($"{sq} has area {Area(sq)}");

        }
    }
}
