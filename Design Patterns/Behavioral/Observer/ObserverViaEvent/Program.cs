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

        public event EventHandler<FallsIllEventArgs> FallsIll;
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

        }

        private static void CallDoctor(object sender, FallsIllEventArgs args)
        {

            Console.WriteLine($"Somebody call the doctor at {args.DoctorAddress} for patient at {args.PatientAddress}");
        }
    }
}
