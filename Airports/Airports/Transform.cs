using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using JSON = Newtonsoft.Json;

namespace Airports
{
    class Transform
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string FilePath = "SourceFiles/airports.dat";
        //private readonly Regex pattern = new Regex("^[1-9][0-9]*?(,\"([A-Z]+\\s?[A-Z]+)+\"){3},\"[A-Z]{3}\",\"[A-Z]{4}\"(,-?[0-9]*\\.?[0-9]+){3}", RegexOptions.IgnoreCase);
        private readonly Regex pattern = new Regex("^[1-9][0-9]*?(,\"([A-Z']+\\s?[A-Z']+)+\"){3},\"[A-Z']{3}\",\"[A-Z']{4}\"(,-?[0-9]*\\.?[0-9]+){3}", RegexOptions.IgnoreCase);

        public Dictionary<CityKey, City> Cities { get; set; }
        public Dictionary<string, Country> Countries{ get; set; }
        public Dictionary<AirportKey, Airport> Airports { get; set; }

        public Transform()
        {
            Cities = new Dictionary<CityKey, City>();
            Countries = new Dictionary<string, Country>();
            Airports = new Dictionary<AirportKey, Airport>();
        }

        public void StartTransform()
        {
            ReadFromFile();
            CreateJSONFiles();
        }
        private void ReadFromFile()
        {
            if (!File.Exists(FilePath))
                throw new FileNotFoundException(FilePath);

            int incorrectLinesCount = 0;
            string[] s = File.ReadAllLines(FilePath);
            foreach (string currentLine in s)
            {
                Console.WriteLine(currentLine);
                if (IsMatch(currentLine))
                {
                    var splitted = currentLine.Replace("\"", string.Empty).Split(',');
                    CreateInstances(splitted);
                }
                else
                {
                    logger.Error(currentLine);
                    incorrectLinesCount++;
                }
            }
            logger.Info($"There are {incorrectLinesCount} incorrect lines!");
        }
        private void CreateJSONFiles()
        {
            JSON.JsonSerializer serializer = new JSON.JsonSerializer();

            using (StreamWriter sw = new StreamWriter(new FileStream("Cities.JSON", FileMode.Create)))
            {
                serializer.Serialize(sw, Cities.ToArray());
            }

            using (StreamWriter sw = new StreamWriter(new FileStream("Countries.JSON", FileMode.Create)))
            {
                serializer.Serialize(sw, Countries.ToArray());
            }

            using (StreamWriter sw = new StreamWriter(new FileStream("Airports.JSON", FileMode.Create)))
            {
                serializer.Serialize(sw, Airports.ToArray());
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

            Country country;
            if (!Countries.ContainsKey(countryName))
            {
                country = new Country(countryName, "", "");
                Countries[countryName] = country;
            }
            else
                country = Countries[countryName];

            City city;
            CityKey cityKey = new CityKey() { CityName = cityName, CountryID = country.Id };
            if (!Cities.ContainsKey(cityKey))
            {
                city = new City(country.Id, cityName, "");
                Cities[cityKey] = city;
            }
            else
                city = Cities[cityKey];

            AirportKey airportKey = new AirportKey() { AirportName = airportName, CityID = city.Id };
            Airports.Add(airportKey, new Airport()
            {
                Id = airportID,
                CityId = city.Id,
                CountryId = country.Id,
                IATACode = IATA,
                ICAOCode = ICAO,
                Name = airportName,
                TimeZoneName = ""
            });
        }
        private bool IsMatch(string text)
        {
            return pattern.IsMatch(text);
        }
    }
}