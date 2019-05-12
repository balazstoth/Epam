using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Airports
{
    class Serializer
    {
        private readonly string timeZoneFilePath = "SourceFiles/timezoneinfo.json";
        private readonly string airportFilePath = "SourceFiles/airports.dat";
        private readonly Regex pattern = Pattern.FullPattern;
        private Dictionary<int, string> TimeZones;

        public Dictionary<CityKey, City> Cities { get; }
        public Dictionary<string, Country> Countries { get; }
        public Dictionary<AirportKey, Airport> Airports { get; }

        public Serializer()
        {
            TimeZones = DeserializeTimeZones().ToDictionary(k => k.AirportId, v => v.TimeZoneInfoId);
            Cities = new Dictionary<CityKey, City>();
            Countries = new Dictionary<string, Country>();
            Airports = new Dictionary<AirportKey, Airport>();
            StartSerialize();
        }

        public void StartSerialize()
        {
            using (new Timer("serialization"))
            {
                ReadFromFile();
                CreateJSONFiles();
            }
        }
        private void ReadFromFile()
        {
            if (!File.Exists(airportFilePath))
                throw new FileNotFoundException(airportFilePath);

            int incorrectLinesCount = 0;
            string[] lines = File.ReadAllLines(airportFilePath);

            foreach (string currentLine in lines)
            {
                if (IsMatch(currentLine))
                {
                    CreateInstances(currentLine.Replace("\"", "").Split(','));
                }
                else
                {
                    Log.Error("Not match: " + currentLine);
                    incorrectLinesCount++;
                }
            }
            Log.Information($"There are {incorrectLinesCount} incorrect lines!");
        }
        private void CreateJSONFiles()
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(new FileStream("Cities.JSON", FileMode.Create)))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(jw, Cities.Values.ToArray());
            }

            using (StreamWriter sw = new StreamWriter(new FileStream("Countries.JSON", FileMode.Create)))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(jw, Countries.Values.ToArray());
            }

            using (StreamWriter sw = new StreamWriter(new FileStream("Airports.JSON", FileMode.Create)))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(jw, Airports.Values.ToArray());
            }
        }
        private void CreateInstances(string[] splitted)
        {
            int airportID = int.Parse(splitted[0]);
            string airportName = splitted[1];
            string cityName = splitted[2];
            string countryName = splitted[3];
            string IATA = splitted[4];
            string ICAO = splitted[5];
            Location location = new Location(
                double.Parse(splitted[6], CultureInfo.InvariantCulture),
                double.Parse(splitted[7], CultureInfo.InvariantCulture),
                double.Parse(splitted[8], CultureInfo.InvariantCulture));

            string zoneInfoId = GetZoneInfo(airportID);
            Country country = CreateCountry(countryName);
            City city = CreateCity(cityName, country, zoneInfoId);
            CreateAirport(airportID, airportName, IATA, ICAO, country, city, zoneInfoId);
        }
        private string GetZoneInfo(int airportID)
        {
            if (TimeZones.ContainsKey(airportID))
                return TimeZones[airportID];
            return string.Empty;
        }
        private void CreateAirport(int airportID, string airportName, string IATA, string ICAO, Country country, City city, string zoneInfo)
        {
            AirportKey airportKey = new AirportKey() { AirportName = airportName, CityID = city.Id };
            if (!Airports.ContainsKey(airportKey))
                Airports.Add(airportKey, new Airport(airportID, country.Id, city.Id, IATA, ICAO, airportName, zoneInfo));
        }
        private City CreateCity(string cityName, Country country, string timeZone)
        {
            City city;
            CityKey cityKey = new CityKey() { CityName = cityName, CountryID = country.Id };
            if (!Cities.ContainsKey(cityKey))
            {
                city = new City(country.Id, cityName, timeZone);
                Cities[cityKey] = city;
            }
            else
            {
                city = Cities[cityKey];
            }
            return city;
        }
        private Country CreateCountry(string countryName)
        {
            Country country;
            if (!Countries.ContainsKey(countryName))
            {
                var currentCulture = CultureInfo.GetCultures(CultureTypes.AllCultures)
                                                .Where(c => c.EnglishName.ToLower().Contains(countryName.ToLower()))
                                                .FirstOrDefault();
                if (currentCulture == null)
                    country = new Country(countryName, "", "");
                else
                {
                    RegionInfo rInfo = null;
                    try { rInfo = new RegionInfo(currentCulture.Name); } catch (Exception) { };
                    country = new Country(countryName, rInfo?.ThreeLetterISORegionName, rInfo?.TwoLetterISORegionName);
                }
                Countries[countryName] = country;
            }
            else
                country = Countries[countryName];
            return country;
        }
        private bool IsMatch(string text)
        {
            return pattern.IsMatch(text);
        }
        private ZoneInfoPairs[] DeserializeTimeZones()
        {
            using (StreamReader sr = new StreamReader(timeZoneFilePath))
                return JsonConvert.DeserializeObject<ZoneInfoPairs[]>(sr.ReadToEnd());
        }
    }
}
