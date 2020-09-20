using System;
using System.Collections;
using System.Collections.Generic;

namespace CompositeCodingExercise
{

    public interface IValueContainer:IEnumerable<int>
    {

    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {

    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<IValueContainer> conts = new List<IValueContainer>();
            conts.Add(new SingleValue() { Value = 5 });
            conts.Add(new SingleValue() { Value = 13 });

            conts.Add(new ManyValues() { 3, 12 });

            Console.WriteLine(conts.Sum());

        }
    }
}
