using System;
using System.Linq;

namespace EightQueens.ConsoleApp
{
    internal class Solver
    {
        private readonly Table table;
        private int count;

        public Solver(Table table)
        {
            this.table = table;
            count = 0;
        }

        public void Start()
        {
            Solve();

            if (count == 0)
            {
                Console.WriteLine("There's no solution!");
            }
        }

        private bool IsEnabled(int row, int column)
        {
            return CheckRow(row) && CheckDiagonalLeftDown(row, column) && CheckDiagonalLeftUp(row, column);
        }

        private bool CheckRow(int row)
        {
            return !table.Columns.Contains(row);
        }

        private bool CheckDiagonalLeftUp(int row, int column)
        {
            int i = 0;
            while (i <= column && i <= row)
            {
                if (table.Columns[column - i] == row - i)
                {
                    return false;
                }

                i++;
            }

            return true;
        }

        private bool CheckDiagonalLeftDown(int row, int column)
        {
            int i = 0;
            while (i <= column && row + i < table.TableLength)
            {
                if (table.Columns[column - i] == row + i)
                {
                    return false;
                }

                i++;
            }

            return true;
        }

        private void Solve(int column = 0)
        {
            if (column >= table.TableLength)
            {
                DisplayTable.Display(table, count);
                count++;
                return;
            }

            for (int i = 0; i < table.TableLength; i++)
            {
                if (IsEnabled(i, column))
                {
                    table.Columns[column] = i;
                    Solve(column + 1);
                    table.Columns[column] = -1;
                }
            }

            return;
        }
    }
}