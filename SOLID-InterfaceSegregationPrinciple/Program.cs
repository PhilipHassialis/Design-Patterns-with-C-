using System;

namespace SOLID_InterfaceSegregationPrinciple
{
    public class Document { }

    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }

        public void Fax(Document d)
        {
            //
        }

    }

    public class OldFashionedPrinter : IMachine
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException(nameof(Scan));
        }

        public void Fax(Document d)
        {
            throw new NotImplementedException(nameof(Fax));
        }
    }


    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IFax
    {
        void Fax(Document d);
    }

    public class PhotoCopier : IScanner, IPrinter
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMultiFunctionMachine : IScanner, IPrinter // ....
    {
    }

    public class MultiFunctionMachine : IMultiFunctionMachine
    {
        IPrinter printer;
        IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
            // decorator pattern
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
