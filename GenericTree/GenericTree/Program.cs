using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<int> tree = new Tree<int>(1);
            tree.Root.Add(2);
            tree.Root.Add(3);
            tree.Root.Add(4);
            tree.Root.Add(5);

            Console.WriteLine("Root: " + tree);
            foreach (int item in tree)
                Console.WriteLine("Level1: " + item);
            Console.ReadKey();
        }
    }
}
