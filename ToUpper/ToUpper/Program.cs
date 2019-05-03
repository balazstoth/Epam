using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToUpper
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "  s sss s s  sssssss sssss s   ";
            Console.WriteLine(ToUpper(s));
            Console.ReadKey();
        }

        static bool IsSeparator(char c)
        {
            HashSet<char> separators = new HashSet<char>() { ' ', ',', '.' };
            return separators.Contains(c);
        }

        static string ToUpper(string text)
        {
            bool toUpper = false;
            bool isFirst = true;
            string newString = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                char current = text[i];

                if (IsSeparator(current))
                {
                    toUpper = true;
                    newString += current;
                }
                else
                {
                    if (isFirst)
                    {
                        newString += Char.ToUpper(current);
                        isFirst = false;
                    }
                    else if (toUpper)
                    {
                        newString += Char.ToUpper(current);
                        toUpper = false;
                    }
                    else
                        newString += current;
                }
            }
            return newString;
        }
    }
}
