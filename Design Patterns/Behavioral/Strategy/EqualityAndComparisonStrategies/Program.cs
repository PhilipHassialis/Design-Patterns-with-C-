using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EqualityAndComparisonStrategies
{
    public class Person : IComparable, IComparable<Person>
    {
        public int Id;
        public string Name;
        public int Age;

        public Person(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(this, obj)) return 0;
            if (ReferenceEquals(null, obj)) return 1;
            return obj is Person other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Person)}");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return obj is Person other ? Id.Equals(other.Id): throw new ArgumentException($"Object must be of type {nameof(Person)}");
        }

       

        public int CompareTo([AllowNull] Person other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }

        public static bool operator ==(Person left, Person right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !(left == right);
        }

        public static bool operator <(Person left, Person right)
        {
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        }

        public static bool operator <=(Person left, Person right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator >(Person left, Person right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(Person left, Person right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }

        private sealed class NameRelationalComparer: IComparer<Person>
        {
            public int Compare(Person x, Person y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            }
        }

        public static IComparer<Person> NameComparer { get; } = new NameRelationalComparer();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person>();

            people.Sort(); // default

            //people.Sort((x, y) => x.Name.CompareTo(y.Name));

            people.Sort(Person.NameComparer);

        }
    }
}
