using System;

namespace DecoratorCodingExercise
{
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon // no need for interfaces
    {
        private int age;
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();

        public int Age
        {
            get => age;
            set { bird.Age = lizard.Age = age = value; }
        }

        public string Fly()
        {
            return bird.Fly();
        }

        public string Crawl()
        {
            return lizard.Crawl();

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dragon d = new Dragon() { Age = 12 };
            Console.WriteLine(d.Fly());
            Console.WriteLine(d.Crawl());

        }
    }
}
