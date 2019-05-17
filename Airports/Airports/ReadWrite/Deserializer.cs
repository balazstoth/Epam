using Newtonsoft.Json;
using System.IO;

namespace Airports
{
    class Deserializer
    {
        public City[] DeserializeCities()
        {
            string result = File.ReadAllText(FileCheck.GetJsonFileName(0));
            var cities = JsonConvert.DeserializeObject<City[]>(result);
            City.ResetStaticID();
            return cities;
        }
        public Country[] DeserializeCountries()
        {
            string result = File.ReadAllText(FileCheck.GetJsonFileName(1));
            var countries = JsonConvert.DeserializeObject<Country[]>(result);
            Country.ResetStaticID();
            return countries;
        }
        public Airport[] DeserializeAirports()
        {
            string result = File.ReadAllText(FileCheck.GetJsonFileName(2));
            var airports = JsonConvert.DeserializeObject<Airport[]>(result);
            return airports;
        }
    }
}
