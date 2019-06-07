using System;

namespace EightQueens.ConsoleApp
{
    internal class Table
    {
        public int TableLength { get; set; }

        public int[] Columns { get; set; }

        public Table(int tableLength)
        {
            if (tableLength <= 0)
            {
                throw new ArgumentException($" The tableLength parameter is incorrect: {tableLength}");
            }

            TableLength = tableLength;
            Columns = new int[TableLength];
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < TableLength; i++)
            {
                Columns[i] = -1;
            }
        }
    }
}
