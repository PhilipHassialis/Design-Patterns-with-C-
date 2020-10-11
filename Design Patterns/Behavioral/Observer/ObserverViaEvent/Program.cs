using System;

namespace ObserverViaEvent
{
    public class FallsIllEventArgs:EventArgs
    {
        public string DoctorAddress;
        public string PatientAddress;
    }

    public class Person
    {
        public string Address;
        public string DoctorAddress;

        public void CatchACold()
        {
            FallsIll?.Invoke(this, new FallsIllEventArgs() { PatientAddress = Address, DoctorAddress = DoctorAddress } );
        }

        public void Recuperate()
        {
            GotBetter?.Invoke(this, new EventArgs());
        }

        public event EventHandler<FallsIllEventArgs> FallsIll;
        public event EventHandler<EventArgs> GotBetter;
    }


    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person() { Address = "Lelas Karagianni 42", DoctorAddress = "Megistis 30A" };
            //p.FallsIll += (sender, args) =>
            //{
            //    Console.WriteLine("Fell ill");
            //};
            p.FallsIll += CallDoctor;
            p.CatchACold();
            //p.FallsIll -= CallDoctor;

            p.GotBetter += delegate (object sender, EventArgs args) { GetsWell(sender, args, "get ripped"); };
            p.Recuperate();

        }

        private static void CallDoctor(object sender, FallsIllEventArgs args)
        {

            Console.WriteLine($"Somebody call the doctor at {args.DoctorAddress} for patient at {args.PatientAddress}");
        }

        private static void GetsWell(object sender, EventArgs args, string getWellWish)
        {
            Console.WriteLine($"Got well, we wish him \"{getWellWish}\"");
        }
    }
}
