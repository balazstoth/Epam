using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        private readonly string timeZoneFilePath;
        private readonly string airportFilePath;
        private readonly Regex pattern;
        private Dictionary<int, string> TimeZones;

        public Dictionary<CityKey, City> Cities { get; }
        public Dictionary<string, Country> Countries { get; }
        public Dictionary<AirportKey, Airport> Airports { get; }

        public Serializer()
        {
            if (!FileCheck.SourceFilesExist())
                throw new FileNotFoundException(string.Join(" / ", FileCheck.SourcefileNames));

            airportFilePath = FileCheck.GetSourceFilePath(0);
            timeZoneFilePath = FileCheck.GetSourceFilePath(1);
            pattern = new Regex(Pattern.linePattern, RegexOptions.IgnoreCase);
            Cities = new Dictionary<CityKey, City>();
            Countries = new Dictionary<string, Country>();
            Airports = new Dictionary<AirportKey, Airport>();
            TimeZones = DeserializeTimeZones().ToDictionary(k => k.AirportId, v => v.TimeZoneInfoId);
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
            int incorrectLinesCount = 0;
            string[] lines = File.ReadAllLines(airportFilePath);
            Match match;

            foreach (string currentLine in lines)
            {
                match = pattern.Match(currentLine);
                if (match.Success)
                {
                    CreateInstances(match);
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
            JsonSerializerSettings jss = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented };
            string[] serializedObjects = new string[3];

            serializedObjects[0] = JsonConvert.SerializeObject(Cities.Values.ToArray(), jss);
            serializedObjects[1] = JsonConvert.SerializeObject(Countries.Values.ToArray(), jss);
            serializedObjects[2] = JsonConvert.SerializeObject(Airports.Values.ToArray(), jss);

            for (int i = 0; i < serializedObjects.Length; i++)
                File.WriteAllText(FileCheck.GetJsonFileName(i), serializedObjects[i]);
        }
        private void CreateInstances(Match match)
        {
            int airportID = int.Parse(match.Groups["Id"].Value);
            string zoneInfoId = GetZoneInfo(airportID);
            string airportName = match.Groups["Airport"].Value;
            string cityName = match.Groups["City"].Value;
            string countryName = match.Groups["Country"].Value;
            string IATA = match.Groups["IATA"].Value;
            string ICAO = match.Groups["ICAO"].Value;
            Location location = new Location(
                double.Parse(match.Groups["Long"].Value, CultureInfo.InvariantCulture),
                double.Parse(match.Groups["Lat"].Value, CultureInfo.InvariantCulture),
                double.Parse(match.Groups["Alt"].Value, CultureInfo.InvariantCulture));

            Country country = CreateCountry(countryName);
            City city = CreateCity(cityName, country, zoneInfoId);
            CreateAirport(airportID, airportName, IATA, ICAO, country, city, zoneInfoId, location);
        }
        private string GetZoneInfo(int airportID)
        {
            if (TimeZones.ContainsKey(airportID))
                return TimeZones[airportID];
            return string.Empty;
        }
        private void CreateAirport(int airportID, string airportName, string IATA, string ICAO, Country country, City city, string zoneInfo, Location location)
        {
            AirportKey airportKey = new AirportKey() { AirportName = airportName, CityID = city.Id };
            if (!Airports.ContainsKey(airportKey))
                Airports.Add(airportKey, new Airport(airportID, country, city, IATA, ICAO, airportName, zoneInfo, location));
        }
        private City CreateCity(string cityName, Country country, string timeZone)
        {
            City city;
            CityKey cityKey = new CityKey() { CityName = cityName, CountryID = country.Id };
            if (!Cities.ContainsKey(cityKey))
            {
                city = new City(country, cityName, timeZone);
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
                RegionInfo rInfo = null;
                var currentCulture = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                                                .Where(c => c.EnglishName.ToLower().Contains(countryName.ToLower()))
                                                .FirstOrDefault();
                if (currentCulture == null)
                {
                    Log.Error(string.Format(Properties.Resources.Exception_Culture_NullCulture, countryName));
                    country = new Country(countryName, "", "");
                }
                else
                {
                    try
                    {
                        rInfo = new RegionInfo(currentCulture.Name);
                    }
                    catch (ArgumentException)
                    {
                        Log.Error(string.Format(Properties.Resources.Exception_Culture_InvalidCulture, currentCulture.Name));
                    }
                    country = new Country(countryName, 
                                            rInfo == null ? "" : rInfo.ThreeLetterISORegionName,
                                            rInfo == null ? "" : rInfo.TwoLetterISORegionName);
                }
                Countries[countryName] = country;
            }
            else
                country = Countries[countryName];
            return country;
        }
        private ZoneInfoPairs[] DeserializeTimeZones()
        {
            return JsonConvert.DeserializeObject<ZoneInfoPairs[]>(File.ReadAllText(timeZoneFilePath));
        }
    }
}
