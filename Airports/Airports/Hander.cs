using Elect.Location.Coordinate.DistanceUtils;
using Elect.Location.Models;
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
        public Dictionary<string, Country> Countries { get; set; }
        public Dictionary<AirportKey, Airport> Airports { get; set; }

        public Handler()
        {
            if (!FileCheck.SourceFilesExist())
                throw new FileNotFoundException();

            if (!FileCheck.JsonFilesExist())
                serializer = new Serializer();

            deserializer = new Deserializer();
            SetValues();
        }

        private void SetValues()
        {
            using (new Timer("deserialization"))
            {
                Cities = deserializer.DeserializeCities()
                            .ToDictionary(
                                k => new CityKey() { CountryID = k.CountryId, CityName = k.Name },
                                v => new City(v.CountryId, v.Name, v.TimeZoneId));

                Countries = deserializer.DeserializeCountries().ToDictionary(k => k.Name);

                Airports = deserializer.DeserializeAirports()
                            .ToDictionary(
                                k => new AirportKey() { AirportName = k.Name, CityID = k.CityId },
                                v => new Airport(v.Id, v.CountryId, v.CityId, v.IATACode, v.ICAOCode, v.Name, v.TimeZoneId, v.Location));
            }
        }

        //Queries
        public string GetCountriesAndTheirAirportsCount()
        {
            var airportCountryPairs = Countries
                        .Join(Cities, country => country.Value.Id, city => city.Value.CountryId, (country, city) => new { country, city })
                        .Join(Airports, CCpair => CCpair.city.Value.Id, airport => airport.Value.CityId, (CCpair, airport) => new { Airport = airport, CCP = CCpair });

            var CountPerCountries = from pairs in airportCountryPairs
                                    group pairs by pairs.CCP.country.Value.Name into g
                                    orderby g.Key
                                    select new { Country = g.Key, Count = g.Count(), Lenght = g.Key.Length };

            int maxCharacterCount = CountPerCountries.Max(x => x.Lenght);
            return string.Join(Environment.NewLine, CountPerCountries.Select(x => x.Country.PadRight(maxCharacterCount) + " - " + x.Count));
        }
        public string GetWhichCityHasMostAirPorts()
        {
            var pairs = from pair in Airports.Join(Cities,
                                                    airport => airport.Value.CityId,
                                                    city => city.Value.Id,
                                                    (airport, city) => new { Airport = airport, City = city })
                        group pair by pair.City into g
                        select new { Name = g.Key, Value = g, Count = g.Count() };

            int max = pairs.Max(p => p.Count);
            return string.Join(Environment.NewLine, pairs.Where(p => p.Count == max).Select(p => p.Value.Key.Value.Name + " - " + p.Count));
        }
        public string GetClosestAirport(CoordinateModel original)
        {
            var result = Airports
                .Join(Cities, a => a.Value.CityId, c => c.Value.Id, (a, c) => new { Airport = a, City = c })
                .Select(
                    pair => new
                    {
                        Distance = DistanceHelper.GetDistanceByGeo(original, new CoordinateModel(pair.Airport.Value.Location.Longitude, pair.Airport.Value.Location.Latitude), UnitOfLengthModel.Kilometer),
                        AirportName = pair.Airport.Value.FullName,
                        CityName = pair.City.Value.Name
                    }).OrderBy(p => p.Distance).Take(1).Select(p => $"{p.AirportName} in {p.CityName}");

            return string.Join(Environment.NewLine, result);
        }
    }
}