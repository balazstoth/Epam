using System.Collections.Generic;

namespace Airports.Other
{
    class DictionaryContainer
    {
        public Dictionary<CityKey, City> Cities { get; set; }
        public Dictionary<string, Country> Countries { get; set; }
        public Dictionary<AirportKey, Airport> Airports { get; set; }
    }
}
