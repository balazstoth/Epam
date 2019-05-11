using System.IO;
using System.Linq;

namespace Airports
{
    public static class FileCheck
    {
        private static string[] sourcefileNames = new string[] { "airports.dat", "timezoneinfo.json" };
        private static string path = @"SourceFiles/";
        private static string[] jsonfileNames = new string[] { "Cities", "Countries", "Airports" };
        private static string extenstion = ".json";

        public static bool JsonFilesExist()
        {
            return jsonfileNames.All(file => File.Exists(file + extenstion));
        }
        public static bool SourceFilesExist()
        {
            return sourcefileNames.All(file => File.Exists(Path.Combine(path,file)));
        }
        public static void DeleteLogs(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        public static string GetFileName(int index)
        {
            return jsonfileNames[index] + extenstion; 
        }
    }
}
