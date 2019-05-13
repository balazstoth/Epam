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
            Handler jsonHander = new Handler();

            //Q1
            Console.WriteLine("Number of airports per countries:");
            Console.WriteLine(jsonHander.GetCountriesAndTheirAirportsCount());
            Console.WriteLine("");

            //Q2
            Console.WriteLine("Which cities have the most airports:");
            Console.WriteLine(jsonHander.GetWhichCityHasMostAirPorts());
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

        static string GetClosestAirport(Handler handler)
        {
            Console.WriteLine("Enter the coordinates of your location: (Longitude, Latitude)");
            string location = Console.ReadLine();

            if (!Pattern.LocationPattern.IsMatch(location))
                throw new ArgumentException(location);

            // Epam: 47.48882, 19.08004

            var splitted = location.Split(",");
            CoordinateModel originalCoordinate = new CoordinateModel(double.Parse(splitted[0]), double.Parse(splitted[1]));
            return handler.GetClosestAirport(originalCoordinate);
        }
        static string GetAirportFromIATA(Handler handler)
        {
            Console.WriteLine("Enter an IATA code:");
            string code = Console.ReadLine();

            if (!Pattern.IATAPattern.IsMatch(code))
                throw new ArgumentException(code);

            return handler.GetAirportFromIATA(code);
        }
    }
}
