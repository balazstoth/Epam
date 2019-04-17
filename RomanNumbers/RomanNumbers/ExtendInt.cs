using System;
using System.Collections.Generic;

namespace RomanNumbers
{
    public static class ExtendInt
    {
        private const int dLimit = 0;
        private const int uLimit = 4000;
        private static readonly Dictionary<string, int> keys;

        static ExtendInt()
        {
            keys = new Dictionary<string, int>()
            {
                { "M", 1000 },
                { "CM", 900},
                { "D", 500},
                { "CD", 400},
                { "C", 100 },
                { "XC", 90},
                { "L", 50},
                { "XL", 40},
                { "X", 10 },
                { "IX", 9 },
                { "V", 5 },
                { "IV", 4 },
                { "I", 1 },
            };
        }

        public static string ToRoman(this int number)
        {
            if (!isValid(number))
                throw new ArgumentOutOfRangeException();

            string result = String.Empty;
            foreach (KeyValuePair<string,int> item in keys)
            {
                while(number >= item.Value)
                {
                    result += item.Key;
                    number -= item.Value;
                }

                if (number == 0)
                    break;
            }

            return result;
        }

        private static bool isValid(int i)
        {
            return i < uLimit && i > dLimit;
        }
    }
}
