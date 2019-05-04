using System.IO;
using System.Linq;

namespace Airports
{
    public static class FileCheck
    {
        private static string[] fileNames = new string[] { "Airports", "Cities", "Countries" };
        private static string extenstion = ".JSON";

        public static bool FileExist()
        {
            return fileNames.All(file => File.Exists(file + extenstion));
        }
    }
}
