using System;
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

            if(!FileCheck.JsonFilesExist())
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
        public string GetCountriesAndTheirAirports()
        {
            var q1 = Countries
                .Join(Cities, country => country.Value.Id, city => city.Value.CountryId, (country, city) => new { country, city })
                .Join(Airports, CCpair => CCpair.city.Value.Id, airport => airport.Value.CityId, (CCpair, airport) => new { Airport = airport, CCP = CCpair });

            var q2 = from x in q1
                     group x by x.CCP.country.Value.Name into g
                     orderby g.Key
                     select new { Country = g.Key, Count = g.Count() };

            return string.Join(Environment.NewLine, q2.Select(x => x.Country + " - " + x.Count));
        }
    }
}