using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricShapes
{
    public class GraphicObject
    {
        public string Color;
        public virtual string Name { get; set; } = "Group";
        private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
        public List<GraphicObject> Children => children.Value;

        private void Print(StringBuilder sb, int depth)
        {
            sb.Append(new string('*', depth))
                .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
                .AppendLine(Name);
            foreach (var child in Children)
            {
                child.Print(sb, depth + 1);
            }

        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            Print(sb, 0);
            return sb.ToString();
        }
    }

    public class Circle:GraphicObject
    {
        public override string Name => "Circle";
    }

    public class Square:GraphicObject
    {
        public override string Name => "Square";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var drawing = new GraphicObject() { Name = "Philip drawing" };
            drawing.Children.Add(new Square() { Color = "Red" });
            drawing.Children.Add(new Circle() { Color = "Blue" });

            var group = new GraphicObject();
            group.Children.Add(new Square() { Color = "Purple" });
            group.Children.Add(new Square() { Color = "Green" });
            group.Children.Add(new Circle() { Color = "Cyan " });

            drawing.Children.Add(group);

            Console.WriteLine(drawing);

            Console.WriteLine("Hello World!");
        }
    }
}
