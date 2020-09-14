using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Prototype_Cloneable
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using var ms = new MemoryStream();
            var s = new XmlSerializer(typeof(T));
            s.Serialize(ms, self);
            ms.Position = 0;
            return (T)s.Deserialize(ms);

        }
    }

    // public interface IPrototype<T>
    // {
    //     T DeepCopy();
    // }


    // [Serializable]
    public class Person //: IPrototype<Person>
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }

        public Person()
        {
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)} {nameof(Address)}: {Address}";
        }

        // public Person DeepCopy()
        // {
        //     return new Person(Names, Address.DeepCopy());
        // }
    }


    // [Serializable]
    public class Address // : IPrototype<Address>
    {
        public string StreetName;
        public int HouseNumber;

        public Address()
        {
        }

        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }


        // public Address DeepCopy()
        // {
        //     return new Address(StreetName, HouseNumber);
        // }

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

            // var Jane = new Person(John);
            // var Jane = John.DeepCopy();
            var Jane = John.DeepCopyXml();
            Jane.Address.HouseNumber = 10;
            Jane.Names[0] = "Jane";

            Console.WriteLine(John);
            Console.WriteLine(Jane);
        }
    }

}
