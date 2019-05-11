using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Airports
{
    class Handler
    {
        private Serializer serializer;
        private Deserializer deserializer;

        public Dictionary<CityKey, City> Cities { get; set; }
        public Dictionary<string, Country> Countries{ get; set; }
        public Dictionary<AirportKey, Airport> Airports { get; set; }

        public Handler()
        {
            if (!FileCheck.SourceFilesExist())
                throw new FileNotFoundException();

            if(FileCheck.JsonFilesExist())
                serializer = new Serializer();

            deserializer = new Deserializer();
            SetValues();
        }

        private void SetValues()
        {
            Cities = deserializer.DeserializeCities().ToDictionary(k => new CityKey() { CountryID = k.CountryId, CityName = k.Name });
            Countries = deserializer.DeserializeCountries().ToDictionary(k => k.Name);
            Airports = deserializer.DeserializeAirports().ToDictionary(k => new AirportKey() { AirportName = k.Name, CityID = k.CityId });
        }
    }
}