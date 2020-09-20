using System;
using System.Text;

namespace CustomStringBuilder
{
    class Program
    {
        public class CodeBuilder
        {
            private StringBuilder builder = new StringBuilder();

            // create declarative methods for the relevant builder methods. E.g.
            public CodeBuilder Clear()
            {
                builder.Clear();
                return this;
            }

            public CodeBuilder(string val)
            {
                builder = new StringBuilder(val);
            }

            public CodeBuilder()
            {
                
            }

            public CodeBuilder Append(string val)
            {
                builder.Append(val);
                return this;
            }

            public CodeBuilder AppendLine(string val)
            {
                builder.AppendLine(val);
                return this;
            }

            // .....

            public override string ToString()
            {
                return builder.ToString();
            }
        }

        static void Main(string[] args)
        {
            CodeBuilder cb = new CodeBuilder();
            cb.AppendLine("class MyClass")
                .AppendLine("{")
                .AppendLine("}");

            Console.WriteLine(cb);
        }
    }
}
