using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;

namespace SingletonImplementation
{
    public interface IDatabase
    {
        int GetPopulation(string name);

    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;

        private SingletonDatabase()
        {
            Console.WriteLine("Initializing db");
            capitals = File.ReadAllLines("capitals.txt")
                .Batch(2)
                .ToDictionary(list => list.ElementAt(0).Trim(), list => int.Parse(list.ElementAt(1)));

        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        private static readonly Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            Console.WriteLine($"Tokyo has population of : {db.GetPopulation("Tokyo")}");
        }
    }
}
