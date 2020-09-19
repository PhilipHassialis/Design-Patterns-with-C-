using System;
using System.Text;

namespace GenericValueAdapter
{
    class Program
    {
        public interface IInteger
        {
            int Value { get; }
        }


        public class Dimensions
        {
            public class Two : IInteger
            {
                public int Value => 2;
            }

            public class Three : IInteger
            {
                public int Value => 3;
            }
        }
        public class MyVector<TSelf, T,D> 
            where D:IInteger, new() 
            where TSelf: MyVector<TSelf, T, D>, new()
        {
            protected T[] data;
            public MyVector()
            {
                data = new T[new D().Value];
            
            }

            public MyVector(params T[] values)
            {
                var requiredSize = new D().Value;
                data = new T[requiredSize];
                var providedSize = values.Length;
                for (int i = 0; i < Math.Min(requiredSize, providedSize); i++)
                {
                    data[i] = values[i];
                }
            }

            public TSelf Create(params T[] values)
            {
                var result = new TSelf();
                var requiredSize = new D().Value;
                result.data = new T[requiredSize];
                var providedSize = values.Length;
                for (int i = 0; i < Math.Min(requiredSize, providedSize); i++)
                {
                    result.data[i] = values[i];
                }

                return result;
            }

            public T this[int index]
            {
                get => data[index];
                set => data[index] = value;
            }

            public override string ToString()
            {
                var sb = new StringBuilder("[");
                var dim = new D().Value;
                for (int i = 0; i < dim; i++)
                {
                    sb.Append(data[i]);
                    if (i < dim-1) sb.Append(",");
                }
                sb.Append("]");
                return sb.ToString();
            }
        }

        public class VectorOfInt<D> : MyVector<VectorOfInt<D>,int, D> where D : IInteger, new()
        {
            public VectorOfInt()
            {
            }

            public VectorOfInt(params int[] values) : base(values)
            {
            }

            public static VectorOfInt<D> operator+ (VectorOfInt<D> lhs, VectorOfInt<D> rhs)
            {
                var result = new VectorOfInt<D>();
                var dim = new D().Value;
                for (int i = 0; i < dim; i++)
                {
                    result[i] = lhs[i] + rhs[i];
                }
                return result;
            }
        }

        public class VectorOfFloat<TSelfF, D> : MyVector<TSelfF, float, D> 
            where D : IInteger, new() 
            where TSelfF : MyVector<TSelfF, float,D>, new()
        {
            public VectorOfFloat()
            {
            }

            public VectorOfFloat(params float[] values) : base(values)
            {
            }
        }

        public class Vector2i: VectorOfInt<Dimensions.Two>
        {
            public Vector2i()
            {

            }

            public Vector2i(params int[] values):base(values)
            { }
        }



        public class Vector3f: VectorOfFloat<Vector3f, Dimensions.Three>
        {
            public Vector3f() { }
            public Vector3f(params float[] values) : base(values) { }

            public override string ToString()
            {
                return $"{string.Join(",", data)}";
            }
        }

        static void Main(string[] args)
        {
            var v = new Vector2i(1,2);
            var vv = new Vector2i(3,7);
            var vvv = v + vv;

            Console.WriteLine(vvv);

            var u = new Vector3f(2.2f, 3f, 11.7f);

            Console.WriteLine(u);
        }
    }
}
