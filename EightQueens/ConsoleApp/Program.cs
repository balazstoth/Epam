using System;

namespace EightQueens.ConsoleApp
{
    internal class Program
    {
        public static void Main()
        {
            Table table = new Table(8);
            Solver solver = new Solver(table);
            solver.Start();

            Console.ReadKey();
        }
    }
}