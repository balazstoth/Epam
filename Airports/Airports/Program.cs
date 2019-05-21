using Serilog;
using System;
using Elect.Location.Models;

namespace Airports
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"Logs/InvalidLines.txt";
            FileCheck.DeleteLogs(path);
            Log.Logger = new LoggerConfiguration().WriteTo.File(path).CreateLogger();
            Manager jsonHander = new Manager();

            //Q1
            Console.WriteLine("Number of airports per countries:");
            Console.WriteLine(GetCountriesAndTheirAirportsCount(jsonHander));
            Console.WriteLine("");

            //Q2
            Console.WriteLine("Which cities have the most airports:");
            Console.WriteLine(GetWhichCityHasMostAirPorts(jsonHander));
            Console.WriteLine("");

            //Q3
            Console.WriteLine("Which one is the closest airport:");
            Console.WriteLine(GetClosestAirport(jsonHander));
            Console.WriteLine("");

            //Q4
            Console.WriteLine("Get airport from IATA code:");
            Console.WriteLine(GetAirportFromIATA(jsonHander));
            Console.ReadKey();
        }


        static string GetCountriesAndTheirAirportsCount(Manager handler)
        {
            return handler.CallGetCountriesAndTheirAirportsCount();
        }
        static string GetWhichCityHasMostAirPorts(Manager handler)
        {
            return handler.CallGetWhichCityHasMostAirPorts();
        }
        static string GetClosestAirport(Manager handler)
        {
            string result = string.Empty;
            try
            {
                result = handler.CallGetClosesAirport();
            }
            catch (ArgumentException ex)
            {
                Log.Error(string.Format(Properties.Resources.Exception_argument_InvalidArgument,ex.Message));
            }
            return result;
        }
        static string GetAirportFromIATA(Manager handler)
        {
            string result = string.Empty;
            try
            {
                result = handler.CallGetAirportFromIATA();
            }
            catch (ArgumentException ex)
            {
                Log.Error(string.Format(Properties.Resources.Exception_argument_InvalidArgument, ex.Message));
            }
            return result;
        }
    }
}
