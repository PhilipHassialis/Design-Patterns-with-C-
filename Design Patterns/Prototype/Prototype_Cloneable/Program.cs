using System;

namespace Prototype_Cloneable
{
    public class Person : ICloneable
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public object Clone()
        {
            return new Person(Names, (Address)Address.Clone());
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)} {nameof(Address)}: {Address}";
        }
    }
    public class Address : ICloneable
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public object Clone()
        {
            return new Address(StreetName, HouseNumber);
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName} {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var John = new Person
            (
                names: new[] { "John", "Smith" },
                address: new Address
                (
                    streetName: "Megistis 30A",
                    houseNumber: 9
                )
            );

            var Jane = (Person)John.Clone();
            Jane.Address.HouseNumber = 10;

            Console.WriteLine(John);
            Console.WriteLine(Jane);
        }
    }

}
