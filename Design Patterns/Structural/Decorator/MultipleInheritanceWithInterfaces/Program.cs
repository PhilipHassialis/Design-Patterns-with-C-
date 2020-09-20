using System;

namespace MultipleInheritanceWithInterfaces
{
    public interface IBird
    {
        int Weight { get; set; }

        void Fly();
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }
        public void Fly()
        {
            Console.WriteLine($"Bird flying with weight {Weight}");
        }
    }

    public interface ILizard
    {
        int Weight { get; set; }

        void Crawl();
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }
        public void Crawl()
        {
            Console.WriteLine($"Lizard crawling with weight {Weight}");
        }
    }

    public class Dragon : IBird, ILizard
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();
        private int weight;

        public void Crawl() { lizard.Crawl(); }
        public void Fly() { bird.Fly(); }
        public int Weight { 
            get => weight;
            set { bird.Weight = lizard.Weight = weight = value;  }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dragon dr = new Dragon();
            dr.Weight = 300;
            dr.Fly();
            dr.Crawl();
        }
    }
}
