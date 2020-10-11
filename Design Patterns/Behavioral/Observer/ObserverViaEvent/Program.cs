using System;

namespace ObserverViaEvent
{
    public class FallsIllEventArgs:EventArgs
    {
        public string Address;
    }

    public class Person
    {
        public void CatchACold()
        {
            FallsIll?.Invoke(this, 
                new FallsIllEventArgs() { Address = "Lelas Karagianni 42" } );
        }

        public event EventHandler<FallsIllEventArgs> FallsIll;
    }


    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person();
            //p.FallsIll += (sender, args) =>
            //{
            //    Console.WriteLine("Fell ill");
            //};
            p.FallsIll += CallDoctor;
            p.CatchACold();
            //p.FallsIll -= CallDoctor;

        }

        private static void CallDoctor(object sender, FallsIllEventArgs args)
        {
            Console.WriteLine($"Somebody call the doctor at {args.Address}");
        }
    }
}
