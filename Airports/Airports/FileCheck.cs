using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Airports
{
    public static class FileCheck
    {
        private static string[] fileNames = new string[] { "Cities", "Countries", "Airports" };
        private static string extenstion = ".JSON";

        public static bool FileExist()
        {
            return fileNames.All(file => File.Exists(file + extenstion));
        }
    }
}
