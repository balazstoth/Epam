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

            Console.WriteLine("Ready");
            Console.ReadKey();
        }
    }
}
