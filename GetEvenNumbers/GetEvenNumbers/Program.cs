using System;
using System.Collections.Generic;

namespace GetEvenNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (int item in GetEvenNumbers())
                Console.WriteLine(item);

            Console.ReadKey();
        }

        static IEnumerable<int> GetEvenNumbers()
        {
            int i = 0;
            while (true)
            {
                if (i % 2 == 0)
                    yield return i;
                i++;
            }
        }
    }
}
