using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Airports
{
    class Transform
    {
        private const string FilePath = "SourceFiles/airports.dat";


        public static Dictionary<int, Airport> GetAirportsFromFile()
        {
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }

            return null;
        }
    }
}
