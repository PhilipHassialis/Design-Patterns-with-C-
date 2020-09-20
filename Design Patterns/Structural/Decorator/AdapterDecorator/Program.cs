using System;
using System.Text;

namespace AdapterDecorator
{
    class Program
    {
        public class MyStringBuilder
        {
            StringBuilder builder = new StringBuilder();

            public MyStringBuilder() { }
            public MyStringBuilder(string s) { builder = new StringBuilder(s); }
            public MyStringBuilder Append(string s) { builder.Append(s); return this; }
            public MyStringBuilder AppendLine(string s) { builder.AppendLine(s); return this; }
            public MyStringBuilder Clear() { builder.Clear(); return this; }
            public static implicit operator MyStringBuilder(string s) { var msb = new MyStringBuilder(); msb.Append(s); return msb; }
            public static MyStringBuilder operator +(MyStringBuilder msb, string s) { msb.Append(s); return msb; }
            public override string ToString()
            {
                return builder.ToString();
            }
        }

        static void Main(string[] args)
        {
            MyStringBuilder s = "Hello ";
            s += "world!";

            Console.WriteLine(s);
        }
    }
}
