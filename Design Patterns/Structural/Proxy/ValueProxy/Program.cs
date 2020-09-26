using System;
using System.Diagnostics;

namespace ValueProxy
{
    
    struct Price
    {
        private int value;
        public Price(int value)
        {
            this.value = value;
        }

    }

    [DebuggerDisplay("{value * 100.0f}%")]
    public struct Percentage
    {
        private readonly float value;
        internal Percentage(float value)
        {
            this.value = value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        public static float operator *(float f, Percentage p)
        {
            return f * p.value;
        }
        public static float operator *(int f, Percentage p)
        {
            return f * p.value;
        }

        public static Percentage operator+(Percentage left, Percentage right)
        {
            return new Percentage(left.value + right.value);
        }

        public override string ToString()
        {
            return $"{value*100}%";
        }

        public static bool operator ==(Percentage left, Percentage right) => left.value == right.value;
        public static bool operator !=(Percentage left, Percentage right) => left.value != right.value;

    }

    public static class PercentageExtensions
    {
        public static Percentage Percent(this int value)
        {
            return new Percentage(value / 100.0f);
        }

        public static Percentage Percent(this float value)
        {
            return new Percentage(value / 100.0f);
        }

    }


    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine(182*(47.Percent()+1.Percent()));
        }
    }
}
