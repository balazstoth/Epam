﻿using System.Text.RegularExpressions;

namespace Airports
{
    static class Pattern
    {
        public const string linePattern = "^(?<Id>[1-9][0-9]*)," + 
                                      "\"(?<Airport>[^,]+)\"," + 
                                      "\"(?<City>[^,]+)\"," +
                                      "\"(?<Country>[^,]+)\"," +
                                      "\"(?<IATA>[A-Z]{3})\"," +
                                      "\"(?<ICAO>[A-Z]{4})\"" +
                                      ",(?<Long>-?[0-9]*\\.?[0-9]+)" +
                                      ",(?<Lat>-?[0-9]*\\.?[0-9]+)" +
                                      ",(?<Alt>[0-9]*\\.?[0-9]+)";

        public const string LocationPattern = "^-?[0-9]*\\.?[0-9]+,-?[0-9]*\\.?[0-9]+$";
        public const string IATAPattern = "^[A-Z]{3}$";
    }
}
