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

            //Query1
            Console.WriteLine(jsonHander.GetCountriesAndTheirAirports());
            Console.ReadKey();
        }
    }
}
