using Elect.Location.Coordinate.DistanceUtils;
using Elect.Location.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (!FileCheck.JsonFilesExist())
            {
                try
                {
                    serializer = new Serializer();
                }
                catch (FileNotFoundException ex)
                {
                    Log.Error(string.Format(Properties.Resources.Exception_File_FileNotFound, ex.Message));
                    Environment.Exit(1);
                }
            }

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

        //Call queries
        public string CallGetCountriesAndTheirAirportsCount()
        {
            return GetCountriesAndTheirAirportsCount();
        }
        public string CallGetWhichCityHasMostAirPorts()
        {
            return GetWhichCityHasMostAirPorts();
        }
        public string CallGetClosesAirport()
        {
            Console.WriteLine("Enter the coordinates of your location: (Longitude, Latitude)");
            string location = Console.ReadLine();

            if (!Pattern.LocationPattern.IsMatch(location))
                throw new ArgumentException(location);

            // Epam: 47.48882, 19.08004

            var splitted = location.Split(",");
            CoordinateModel originalCoordinate = new CoordinateModel(double.Parse(splitted[0]), double.Parse(splitted[1]));
            return GetClosestAirport(originalCoordinate);
        }
        public string CallGetAirportFromIATA()
        {
            Console.WriteLine("Enter an IATA code:");
            string code = Console.ReadLine().ToUpper();

            if (!Pattern.IATAPattern.IsMatch(code))
                throw new ArgumentException(code);

            return GetAirportFromIATA(code);
        }

        //Execute queries
        private string GetCountriesAndTheirAirportsCount()
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
        private string GetWhichCityHasMostAirPorts()
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
        private string GetClosestAirport(CoordinateModel original)
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
        private string GetAirportFromIATA(string code)
        {
            var airport = Airports.FirstOrDefault(a => a.Value.IATACode == code.ToUpper());

            if (airport.Value == null)
                throw new ArgumentException(code);

            var city = Cities.FirstOrDefault(c => c.Value.Id == airport.Value.CityId);
            var country = Countries.FirstOrDefault(c => c.Value.Id == city.Value.CountryId);
            var localtime = TimeZoneInfo.ConvertTime(DateTime.Now, airport.Value.timeZoneInfo);
            var offset = new DateTimeOffset(localtime);
            return $"{airport.Value.Name} - {city.Value.Name}, {country.Value.Name} - Local time: {localtime.ToString("HH:mm:ss")} (GMT {offset.Offset})";
        }
    }
}