using System;

namespace EightQueens.ConsoleApp
{
    internal class DisplayTable
    {
        private const string QUEEN = " Q ";
        private const string EMPTY = " - ";

        public static void Display(Table table, int count)
        {
            Console.WriteLine($"Solution: {count + 1}");
            for (int i = 0; i < table.TableLength; i++)
            {
                for (int j = 0; j < table.TableLength; j++)
                {
                    if (table.Columns[j] == i)
                    {
                        Console.Write(QUEEN);
                    }
                    else
                    {
                        Console.Write(EMPTY);
                    }
                }

                Console.WriteLine(string.Empty);
            }

            Console.WriteLine(string.Empty);
        }
    }
}
