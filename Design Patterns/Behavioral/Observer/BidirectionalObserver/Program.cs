using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BidirectionalObserver
{
    public class Product : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value.Equals(name)) return; name = value; OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Product :{Name}";
        }
    }

    public class Window : INotifyPropertyChanged
    {
        private string productName;
        public string ProductName
        {
            get => productName; set
            {
                if (value.Equals(productName)) return; productName = value; OnPropertyChanged();

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Window :{ProductName}";
        }
    }


    public sealed class BidirectionalBinding: IDisposable
    {
        private bool disposed;

        // first second
        // firstProp secondProp

        public BidirectionalBinding(
            INotifyPropertyChanged first, 
            Expression<Func<object>> firstProperty, // x => x.Foo  
            INotifyPropertyChanged second,
            Expression<Func<object>> secondProperty)
        {
            // xxxProperty is MemberExpression
            // Member ^ PropertyInfo

            if (firstProperty.Body is MemberExpression firstExpr &&
                secondProperty.Body is MemberExpression secondExpr)
            {
                if (firstExpr.Member is PropertyInfo firstProp &&
                    secondExpr.Member is PropertyInfo secondProp)
                {
                    first.PropertyChanged += (sender, args) =>
                    {
                        if (!disposed)
                            secondProp.SetValue(second, firstProp.GetValue(first));
                    };
                    second.PropertyChanged += (sender, args) => {
                        if (!disposed)
                            firstProp.SetValue(first, secondProp.GetValue(second));
                    };
                }
            }
        }

        public void Dispose()
        {
            disposed = true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var product = new Product() { Name = "Book" };
            var window = new Window() { ProductName = "Book" };

            //p.PropertyChanged += (sender, args) =>
            //{
            //    if (args.PropertyName=="Name")
            //    {
            //        Console.WriteLine("Name was changed in product");
            //        window.ProductName = p.Name;
            //    }
            //};

            //window.PropertyChanged += (sender, args) =>
            //{
            //    if (args.PropertyName == "ProductName")
            //    {
            //        Console.WriteLine("ProductName was changed in window");
            //        p.Name = window.ProductName;
            //    }
            //};

            using var binding = new BidirectionalBinding(
                product,
                () => product.Name,
                window,
                () => window.ProductName);

            product.Name = "Smart Book";
            Console.WriteLine(product);
            Console.WriteLine(window);

            window.ProductName = "Really Smart Book";
            Console.WriteLine(product);
            Console.WriteLine(window);

        }
    }
}
