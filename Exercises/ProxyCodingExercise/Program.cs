using System;

namespace ProxyCodingExercise
{
    public class Person
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson
    {
        private readonly Person person;
        public ResponsiblePerson(Person person)
        {
            this.person = person;
        }

        public int Age
        {
            get { return person.Age; }
            set { person.Age = value; }
        }

        public string Drink()
        {
            return Age<=18?"too young":person.Drink();
        }

        public string Drive()
        {
            return Age <= 16 ? "too young" : person.Drive();
        }

        public string DrinkAndDrive() => "dead";

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
