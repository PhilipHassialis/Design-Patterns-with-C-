using System;
using System.Collections.Generic;

namespace PropertyProxy
{
    public class Property<T> where T: new()
    {
        private T value;
        public T Value
        {
            get => value;
            set
            {
                if (Equals(this.value, value)) return;
                Console.WriteLine($"Assigning value to {value}");
                this.value = value;
            }
        }
        public Property():this(Activator.CreateInstance<T>())
        {

        }

        public Property(T value)
        {
            this.value = value;
        }

        public static implicit operator T(Property<T> property)
        {
            return property.value; // int = p_int
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value); // Property<int> p = 123;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        
        public bool Equals(Property<T> other)
        {
            if (other == null) return false;
            if (ReferenceEquals(other, this)) return true;
            return EqualityComparer<T>.Default.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Property<T>)obj);
        }

        public static bool operator ==(Property<T> left, Property<T> right) => Equals(left, right);
        public static bool operator !=(Property<T> left, Property<T> right) => !Equals(left, right);
    }

    public class Creature
    {
        //public Property<int> Agility { get; set; } this doesn't mutate but instead returns a new Property<int>

        private Property<int> agility = new Property<int>();
        public int Agility
        {
            get => agility.Value;
            set => agility.Value = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var c = new Creature();
            c.Agility = 10;
            c.Agility = 10;
        }
    }
}
