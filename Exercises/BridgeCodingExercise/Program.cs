using System;

namespace BridgeCodingExercise
{
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }
    
    public class VectorRenderer:IRenderer
    {
        public string WhatToRenderAs => "lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs => "pixels";
    }


    public abstract class Shape
    {
        private IRenderer renderer;
        public string Name { get; set; }
        public Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public override string ToString()
        {
            return $"Drawing {Name} as {renderer.WhatToRenderAs}";
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer):base(renderer) => Name = "Triangle";
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer) => Name = "Square";
    }

    public class VectorSquare : Square
    {
        public VectorSquare() : base(new VectorRenderer())
        {
        }
    }

    public class RasterSquare : Square
    {
        public RasterSquare() : base(new RasterRenderer())
        {
        }
    }

    // imagine VectorTriangle and RasterTriangle are here too


    class Program
    {
        static void Main(string[] args)
        {
            var vs = new VectorSquare();
            var rs = new RasterSquare();
            Console.WriteLine(vs);
            Console.WriteLine(rs);
        }
    }
}
