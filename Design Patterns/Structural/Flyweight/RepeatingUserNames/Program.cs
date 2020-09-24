using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;

namespace RepeatingUserNames
{
    public class User
    {
        public string fullName;

        public User(string fullName)
        {
            this.fullName = fullName;
        }
    }

    public class User2
    {
        static List<string> strings = new List<string>();
        private int[] names;

        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName => string.Join(" ", names.Select(i => strings[i]));
    }

    [TestFixture]
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //new Program().TestUser();
        }

        //[Test]
        //public void TestUser() // 6447680
        //{
        //    var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
        //    var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());
        //    var users = new List<User>();
        //    foreach (var firstname in firstNames)
        //        foreach (var lastname in lastNames)
        //            users.Add(new User(fullName: $"{firstname} {lastname}"));

        //    ForceGC();

        //    dotMemory.Check(memory => Console.WriteLine(memory.SizeInBytes));
        //}

        [Test]
        public void TestUser2() // 6843596...?
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var users2 = new List<User2>();
            foreach (var firstname in firstNames)
                foreach (var lastname in lastNames)
                    users2.Add(new User2(fullName: $"{firstname} {lastname}"));

            ForceGC();

            dotMemory.Check(memory => Console.WriteLine(memory.SizeInBytes));
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private string RandomString()
        {
            Random rand = new Random();
            return new string(
                    Enumerable
                    .Range(0, 10)
                    .Select(i => (char)('a' + rand.Next(26))).ToArray()
                );
        }
    }
}
