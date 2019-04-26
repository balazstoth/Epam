using System;

namespace GenericTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<int> tree = new Tree<int>(10);

            tree.Root.Add(21);
            tree.Root.Add(22);
            tree.Root.Add(23);

            tree.Root[0].Add(31);
            tree.Root[0].Add(32);

            tree.Root[1].Add(33);
            tree.Root[1].Add(34);

            tree.Root[0][0].Add(40);
            tree.Root[0][1].Add(41);

            tree.Root[1][0].Add(42);
            tree.Root[1][1].Add(43);
            tree.Root[1][1].Add(44);
            tree.Root[1][1].Add(45);

            tree.Root[1][1][0].Add(50);
            tree.Root[1][1][0].Add(51);
            tree.Root[1][1][0].Add(52);

            tree.Root[1][1][1].Add(53);
            tree.Root[1][1][1].Add(54);
            tree.Root[1][1][1].Add(55);

            tree.Root[1][1][2].Add(56);
            tree.Root[1][1][2].Add(57);
            tree.Root[1][1][2].Add(58);
            tree.Display();
            
            Console.ReadKey();
        }
    }
}
