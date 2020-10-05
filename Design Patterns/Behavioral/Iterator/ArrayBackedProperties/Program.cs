using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArrayBackedProperties
{
    public class Creature:IEnumerable<int>
    {
        private int[] stats = new int[3];
        private const int strength = 0;

        public int Str { get { return stats[strength]; } set { stats[strength] = value; } }
        public int Agi { get => stats[1]; set => stats[1] = value; }
        public int Int { get => stats[2]; set => stats[2] = value; }
        public double AverageStat => stats.Average();

        public IEnumerator<int> GetEnumerator()
        {
            return stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        //{
        //    get { return (Str + Agi + Int) / 3.0; }
        //}

        public int this[int index]
        {
            get => stats[index];
            set => stats[index] = value;
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
