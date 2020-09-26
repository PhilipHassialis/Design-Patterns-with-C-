using System;
using System.Collections.Generic;
using System.Text;

namespace TextFormatting
{
    public class FormattedText
    {
        private readonly string plaintext;
        private bool[] capitalize;
        public FormattedText(string plaintext)
        {
            this.plaintext = plaintext;
            capitalize = new bool[plaintext.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (var i = start; i <= end; i++)
            {
                capitalize[i] = true;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < plaintext.Length; i++)
            {
                char c = plaintext[i];
                sb.Append(capitalize[i] ? char.ToUpper(c) : c);
            }
            return sb.ToString();
        }
    }

    public class BetterFormattedText
    {
        private readonly string plainText;
        private List<TextRange> formatting = new List<TextRange>();

        public BetterFormattedText(string plainText)
        {
            this.plainText = plainText;
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange() { Start = start, End = end };
            formatting.Add(range);
            return range;
        }


        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;
            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < plainText.Length; i++)
            {
                foreach(var tr in formatting)
                {
                    if (tr.Covers(i) && tr.Capitalize) sb.Append(char.ToUpper(plainText[i])); else sb.Append(plainText[i]);
                }
            }

            return sb.ToString();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var ft = new FormattedText("hello world");
            ft.Capitalize(3, 7);
            Console.WriteLine(ft);

            var ft2 = new BetterFormattedText("hello world");
            ft2.GetRange(3, 7).Capitalize = true;
            Console.WriteLine(ft2);

        }
    }
}
