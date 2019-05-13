using Serilog;
using System;

namespace Airports
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"Logs/InvalidLines.txt";
            FileCheck.DeleteLogs(path);
            Log.Logger = new LoggerConfiguration().WriteTo.File(path).CreateLogger();
            Handler jsonHander = new Handler();

            //Q1
            Console.WriteLine("Number of airports per countries:");
            Console.WriteLine(jsonHander.GetCountriesAndTheirAirportsCount());
            Console.WriteLine("");

            //Q2
            Console.WriteLine("Which cities have the most airports:");
            Console.WriteLine(jsonHander.GetWhichCityHasMostAirPorts());

            Console.ReadKey();
        }
    }
}
