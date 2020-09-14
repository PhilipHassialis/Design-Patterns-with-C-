using System;
using System.Collections.Generic;
using static System.Console;

namespace AbstractFactory
{
    public interface IHotDrink
    {
        void Consume();
    }
    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            WriteLine("Consuming Tea");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            WriteLine("Consuming Coffee");
        }
    }
    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine("Preparing Tea");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine("Preparing Cofeee");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        // the following violates OpenClosed Principle
        // ====================================================

        // public enum AvailableDrink
        // {
        //     Tea, Coffee
        // }
        // private Dictionary<AvailableDrink, IHotDrinkFactory> Factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();

        // public HotDrinkMachine()
        // {
        //     foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //     {
        //         var factory = (IHotDrinkFactory)Activator.CreateInstance(
        //             Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
        //         );
        //         Factories.Add(drink, factory);
        //     }
        // }

        // public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        // {
        //     return Factories[drink].Prepare(amount);
        // }

        private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach (var machineType in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(machineType) && !machineType.IsInterface)
                {
                    factories.Add(Tuple.Create(
                        machineType.Name.Replace("Factory", String.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(machineType)
                    ));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            WriteLine("Available Drinks:");
            for (int i = 0; i < factories.Count; i++)
            {
                Tuple<string, IHotDrinkFactory> tuple = (Tuple<string, IHotDrinkFactory>)factories[i];
                WriteLine($"{i}:{tuple.Item1}");
            }
            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null && int.TryParse(s, out int i) && i >= 0 && i < factories.Count)
                {
                    Write("Specify amount: ");
                    s = Console.ReadLine();
                    if (s != null && int.TryParse(s, out int amount) && amount > 0)
                    {
                        return factories[i].Item2.Prepare(amount);
                    }
                }
                WriteLine("Incorrect input, try again");
            }
        }
    }






    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            // var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 2);
            var drink = machine.MakeDrink();
            drink.Consume();
        }
    }
}
