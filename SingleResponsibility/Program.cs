using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace SingleResponsibility
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;
        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }


    }

    public class Persistence
    {
        public void SaveToFile(Journal j, string fileName, bool overwrite = false)
        {
            if (overwrite || !File.Exists(fileName))

                File.WriteAllText(fileName, j.ToString());

        }

        // public static Journal Load(string fileName)
        // {
        // TODO
        // }

    }

    class Program
    {


        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("Coded");
            j.AddEntry("Fooled around");
            WriteLine(j);

            var p = new Persistence();
            var fileName = "myjournal.txt";
            p.SaveToFile(j, fileName, true);

        }
    }
}
