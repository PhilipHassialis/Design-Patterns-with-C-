using System;
using System.Collections.Generic;

namespace IteratorCodingExercise
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;
        private readonly Node<T> root;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }

        public IEnumerable<T> PreOrder
        {
            get
            {
                static IEnumerable<Node<T>> Traverse(Node<T> current)
                {
                    yield return current;
                    if (current.Left != null)
                    {
                        foreach (var left in Traverse(current.Left))
                        {
                            yield return left;
                        }
                    }
                   
                    if (current.Right != null)
                    {
                        foreach (var right in Traverse(current.Right))
                        {
                            yield return right;
                        }
                    }

                }

                foreach (var node in Traverse(this))
                {
                    yield return node.Value;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3, new Node<int>(4, new Node<int>(6), new Node<int>(7)), new Node<int>(5)));
            foreach (var nodeValue in root.PreOrder)
            {
                Console.WriteLine(nodeValue);
            }
        }
    }
}
