using Newtonsoft.Json;
using System.IO;

namespace Airports
{
    class Deserializer
    {
        public City[] DeserializeCities()
        {
            string result = File.ReadAllText(FileCheck.GetFileName(0));
            City[] cities = JsonConvert.DeserializeObject<City[]>(result);
            City.ResetStaticID();
            return cities;
        }
        public Country[] DeserializeCountries()
        {
            string result = File.ReadAllText(FileCheck.GetFileName(1));
            Country[] countries = JsonConvert.DeserializeObject<Country[]>(result);
            Country.ResetStaticID();
            return countries;
        }
        public Airport[] DeserializeAirports()
        {
            string result = File.ReadAllText(FileCheck.GetFileName(2));
            Airport[] airports = JsonConvert.DeserializeObject<Airport[]>(result);
            return airports;
        }
    }
}
