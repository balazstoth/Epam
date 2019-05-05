using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Airports
{
    class Transform
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string FilePath = "SourceFiles/airports.dat";
        private readonly Regex pattern = new Regex("^[1-9][0-9]*?(,\"([A-Z]+\\s?[A-Z]+)+\"){3},\"[A-Z]{3}\",\"[A-Z]{4}\"(,-?[0-9]*\\.?[0-9]+){3}", RegexOptions.IgnoreCase);

        public List<City> Cities { get; set; }
        public List<Country> Countries{ get; set; }
        public List<Airport> Airports { get; set; }

        public Transform()
        {
            Cities = new List<City>();
            Countries = new List<Country>();
            Airports = new List<Airport>();
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
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine();
                    if (!IsMatch(currentLine))
                    {
                        logger.Error(currentLine);
                        incorrectLinesCount++;
                    }
                    else
                    {
                        var splitted = currentLine.Split(',');
                        CreateInstances(splitted);
                    }
                }
            }
            logger.Info($"There are {incorrectLinesCount} incorrect lines!");
        }
        private void CreateJSONFiles()
        {
            throw new NotImplementedException();
        }
        private void CreateInstances(string[] splitted)
        {
            int airportID = int.Parse(splitted[0]);
            string airportName = splitted[1];
            string cityName = splitted[2];
            string countryName = splitted[3];
            string IATA = splitted[4];
            string ICAO = splitted[5];
            Location location = new Location(double.Parse(splitted[6]), double.Parse(splitted[7]), double.Parse(splitted[8]));

            var country = Countries.Where(c => c.Name.Equals(countryName)).FirstOrDefault();
            if (country == null)
            {
                country = new Country(countryName, "", "");
                Countries.Add(country);
            }

            var city = Cities.Where(c => c.Name.Equals(cityName)).FirstOrDefault();
            if (city == null)
            {
                city = new City(country.Id, cityName, "");
                Cities.Add(city);
            }

            Airports.Add(new Airport()
            {
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