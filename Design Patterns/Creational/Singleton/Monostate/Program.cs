using System;

namespace Monostate
{
    public class CEO
    {
        private static string name;
        private static int age;

        public int Age { get => age; set => age = value; }
        public string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return $"Name: {Name} Age: {Age}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ceo = new CEO() { Name = "Philip", Age = 45 };
            var ceo2 = new CEO();
            Console.WriteLine(ceo2);

        }
    }
}
