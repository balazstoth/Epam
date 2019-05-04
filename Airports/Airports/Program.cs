using System;
using System.IO;

namespace Airports
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!FileCheck.FileExist())
            {
                //Create Files
                Transform.GetAirportsFromFile();
            }

            Console.ReadKey();
        }
    }
}
