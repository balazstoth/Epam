using System.Text.RegularExpressions;

namespace Airports
{
    static class Pattern
    {
        //Full regex check
        internal static readonly Regex FullPattern = new Regex("^[1-9][0-9]*,(\"[A-Z'\\.][A-Z'\\s\\.]+[A-Z'\\.]\",){3}\"[A-Z]{3}\",\"[A-Z]{4}\"(,-?[0-9]*\\.?[0-9]+){3}", RegexOptions.IgnoreCase);

        //Ignore the city, country and airport name
        internal static readonly Regex ReducedPattern = new Regex("^[1-9][0-9]*,(\"[^,]+\",){3}\"[A-Z]{3}\",\"[A-Z]{4}\"(,-?[0-9]*\\.?[0-9]+){3}", RegexOptions.IgnoreCase);

        internal static readonly Regex LocationPattern = new Regex("-?[0-9]*\\.?[0-9]+,-?[0-9]*\\.?[0-9]+");
    }
}
