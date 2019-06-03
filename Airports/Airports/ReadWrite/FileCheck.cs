using System.IO;
using System.Linq;

namespace Airports
{
    public static class FileCheck
    {
        public static string[] SourcefileNames { get; } = new string[] { "airports.dat", "timezoneinfo.json", "airlines.dat", "segments.dat", "flights.dat" };
        private static string path = "SourceFiles";
        private static string[] jsonfileNames = new string[] { "Cities", "Countries", "Airports", "Airlines", "Segments", "Flights" };
        private static string extenstion = ".json";

        public static bool JsonFilesExist()
        {
            return jsonfileNames.All(file => File.Exists(file + extenstion));
        }
        public static bool SourceFilesExist()
        {
            return SourcefileNames.All(file => File.Exists(Path.Combine(path,file)));
        }
        public static void DeleteLogs(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        public static string GetJsonFileName(int index)
        {
            return jsonfileNames[index] + extenstion; 
        }
        public static string GetSourceFilePath(int index)
        {
            return Path.Combine(path, SourcefileNames[index]);
        }
    }
}
