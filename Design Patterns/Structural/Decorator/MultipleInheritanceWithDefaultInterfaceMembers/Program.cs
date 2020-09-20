using System;

namespace MultipleInheritanceWithDefaultInterfaceMembers
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird:ICreature
    {
        void Fly()
        {
            if (Age >= 10)
            {
                Console.WriteLine("I am flying");
            }
        }
    }

    public interface ILizard:ICreature
    {
        void Crawl()
        {
            if (Age < 10) Console.WriteLine("I am crawling");
        }
    }

    public class Organism { }

    public class Dragon:Organism, IBird, ILizard
    {
        public int Age { get; set; }
    }

    // inheritance
    // SmartDragon(Dragon)

    // extension methods
    // C#8 default interface methods



    class Program
    {
        static void Main(string[] args)
        {
            Dragon d = new Dragon() { Age = 5 };

            if (d is IBird bird) bird.Fly();
            if (d is ILizard lizard) lizard.Crawl();
            
        }
    }
}
