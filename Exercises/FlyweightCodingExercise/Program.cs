using System;
using System.Collections.Generic;
using System.Text;

namespace FlyweightCodingExercise
{

    public class Sentence
    {
        List<WordToken> wordTokens = new List<WordToken>();
        string plainText;
        public Sentence(string plainText)
        {
            // todo
            for (var i = 0; i < plainText.Split(" ").Length; i++) wordTokens.Add(new WordToken { Capitalize = false });
            this.plainText = plainText;
        }

        public WordToken this[int index]
        {
            get
            {
                return wordTokens[index];
            }
        }

        public override string ToString()
        {
            // output formatted text here
            var sb = new StringBuilder();
            int i = 0;
            foreach (var word in plainText.Split(" "))
            {
                if (wordTokens[i].Capitalize) sb.Append(word.ToUpper()); else sb.Append(word);
                if (++i < wordTokens.Count) sb.Append(" ");

            }
            return sb.ToString();
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Sentence s = new Sentence("hello world");
            s[1].Capitalize = true;
            Console.WriteLine(s);
        }
    }
}
