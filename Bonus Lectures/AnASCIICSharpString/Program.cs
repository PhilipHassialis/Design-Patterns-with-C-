using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AnASCIICSharpString
{
    // ascii string
    public class str:IEquatable<str>, IEquatable<string>
    {
        [NotNull] protected readonly byte[] buffer;

        public str()
        {
            buffer = new byte[] { };
        }

        public str(string s)
        {
            buffer = Encoding.ASCII.GetBytes(s);
        }

        protected str(byte[] buffer)
        {
            this.buffer = buffer;
        }

        public override string ToString()
        {
            return Encoding.ASCII.GetString(buffer);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static implicit operator str (string s)
        {
            return new str(s);
        }

        public bool Equals(str other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ((IStructuralEquatable)buffer)
                .Equals(other.buffer
                , StructuralComparisons.StructuralEqualityComparer);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((str)obj);
        }

        public bool Equals([AllowNull] string other)
        {
            return ToString() == other;
        }

        public static bool operator == (str left, str right)
        {
            return Equals(left, right);
        }

        public static bool operator != (str left, str right)
        {
            return !Equals(left, right);
        }

        public static str operator + (str first, str second)
        {
            var bytes = new byte[first.buffer.Length + second.buffer.Length];
            first.buffer.CopyTo(bytes, 0);
            second.buffer.CopyTo(bytes, first.buffer.Length);
            return new str(bytes);
        }

        public char this[int index]
        {
            get => (char)buffer[index];
            set => buffer[index] = (byte)value;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
