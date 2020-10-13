using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ObservableCollections
{
    public class Market : INotifyPropertyChanged
    {
        private float volatility;

        public float Volatility
        {
            get => volatility; set
            {
                if (value.Equals(volatility)) return;
                volatility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Market2
    {
        private List<float> prices = new List<float>();

        public void AddPrice(float price)
        {
            prices.Add(price);
            PriceAdded?.Invoke(this, price);
        }

        public event EventHandler<float> PriceAdded;
    }

    public class Market3 // observable
    {
        public BindingList<float> Prices = new BindingList<float>();

        public void AddPrice(float price)
        {
            Prices.Add(price);
        }
    }

    class Program // observer
    {
        static void Main(string[] args)
        {
            //var market = new Market();
            //market.PropertyChanged += (o, e) =>
            //{
            //    if (e.PropertyName.Equals("Volatility"))
            //    {
            //        Console.WriteLine("Volatility changed");
            //    }
            //};
            //market.Volatility = 500;

            //var m2 = new Market2();
            //m2.PriceAdded += (sender, f) =>
            //{
            //    Console.WriteLine($"Price added {f}");
            //};
            //m2.AddPrice(100);

            var m3 = new Market3();
            m3.Prices.ListChanged += (sender, eventArgs) =>
            {
                if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                {
                    float price = ((BindingList<float>)sender)[eventArgs.NewIndex];
                    Console.WriteLine($"Binding list got a price of {price}");
                }
            };
            m3.AddPrice(134);

        }
    }
}
