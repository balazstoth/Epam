using System.Text.RegularExpressions;

namespace Airports
{
    static class Pattern
    {
        private static string pattern = "^(?<Id>[1-9][0-9]*)," + 
                                      "\"(?<Airport>[^,]+)\"," + 
                                      "\"(?<City>[^,]+)\"," +
                                      "\"(?<Country>[^,]+)\"," +
                                      "\"(?<IATA>[A-Z]{3})\"," +
                                      "\"(?<ICAO>[A-Z]{4})\"" +
                                      ",(?<Long>-?[0-9]*\\.?[0-9]+)" +
                                      ",(?<Lat>-?[0-9]*\\.?[0-9]+)" +
                                      ",(?<Alt>[0-9]*\\.?[0-9]+)";

        internal static readonly Regex Regex = new Regex(pattern, RegexOptions.IgnoreCase);
        internal static readonly Regex LocationPattern = new Regex("^-?[0-9]*\\.?[0-9]+,-?[0-9]*\\.?[0-9]+$");
        internal static readonly Regex IATAPattern = new Regex("^[A-Z]{3}$");
    }
}
