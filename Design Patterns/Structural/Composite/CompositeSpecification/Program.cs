using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace CompositeSpecification
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, ExtraLarge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size)
                    yield return p;
            }
        }
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                    yield return p;
            }
        }

        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var p in products)
            {
                if (p.Size == size && p.Color == color)
                    yield return p;
            }
        }
    }

    public abstract class Specification<T>
    {
        public abstract bool IsSatisfied(T t);
        public static Specification<T> operator &(Specification<T> first, Specification<T> second)
        {
            return new AndSpecification<T>(first, second);
        }
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, Specification<T> spec);
    }

    public class ColorSpecification : Specification<Product>
    {
        Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }
        public override bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }

        
    }

    public class SizeSpecification : Specification<Product>
    {
        Size size;
        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public override bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public abstract class CompositeSpecification<T>: Specification<T>
    {
        protected readonly Specification<T>[] items;
        public CompositeSpecification(params Specification<T>[] items)
        {
            this.items = items;
        }
    }

    // combinator
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(params Specification<T>[] items) : base(items)
        {
        }

        public override bool IsSatisfied(T t)
        {
            return items.All(i => i.IsSatisfied(t));
        }
    }

    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public OrSpecification(params Specification<T>[] items) : base(items)
        {
        }

        public override bool IsSatisfied(T t)
        {
            return items.Any(i => i.IsSatisfied(t));
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, Specification<Product> spec)
        {
            foreach (var i in items)
            {
                if (spec.IsSatisfied(i))
                    yield return i;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Green products (old): ");
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                WriteLine($"{p.Name} is Green");
            }

            WriteLine();
            var bf = new BetterFilter();
            WriteLine("Green products (new):");
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                WriteLine($"{p.Name} is Green");
            }
            WriteLine();
            WriteLine("Large blue items");
            foreach (var p in bf.Filter(products,
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Blue),
                    new SizeSpecification(Size.Large)
                )))
            {
                WriteLine($"{p.Name} is Large and Blue");

            }
        }
    }
}
