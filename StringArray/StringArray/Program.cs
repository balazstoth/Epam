using System;
using System.Collections.Generic;
using System.Linq;

namespace StringArray
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] strings = {
                                "You only live forever in the lights you make",
                                "When we were young we used to say",
                                "That you only hear the music when your heart begins to break",
                                "Now we are the kids from yesterday"
                               };

            //Task1
            foreach (var item in WordCount(strings))
                Console.WriteLine(item);

            //Task2
            foreach (var item in Vowel(strings))
                Console.WriteLine(item);

            //Task 3
            Console.WriteLine(LongestWord(strings));

            //Task 4
            Console.WriteLine(Average(strings));

            //Task 5
            foreach (var item in AlphabeticalOrder(strings))
                Console.WriteLine(item);

            Console.ReadKey();
        }

        static IEnumerable<int> WordCount(string[] array)
        {
           return array.Select(x => x.Split(' ').Count()); 
        }
        static IEnumerable<string> Vowel(string[] array)
        {
            HashSet<char> vowels = new HashSet<char>() { 'A', 'E', 'I', 'O', 'U' };
            return array.SelectMany(s => s.Split(' ')).Where(w => vowels.Contains(w.ToUpper().First()));
        }
        static string LongestWord(string[] array)
        {
            return array.SelectMany(s => s.Split(' ')).OrderByDescending(x => x.Count()).FirstOrDefault();
        }
        static double Average(string[] array)
        {
            return array.Average(x => x.Split(' ').Count());
        }
        static IEnumerable<string> AlphabeticalOrder(string[] array)
        {
            return array.SelectMany(s => s.ToLower().Split(' ')).OrderBy(x => x).Distinct();
        }
    }
}
