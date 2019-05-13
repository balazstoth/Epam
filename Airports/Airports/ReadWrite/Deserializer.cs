using Newtonsoft.Json;
using System.IO;

namespace Airports
{
    class Deserializer
    {
        public City[] DeserializeCities()
        {
            City[] cities;
            using (new Timer("city"))
            {
                string result = File.ReadAllText(FileCheck.GetFileName(0));
                cities = JsonConvert.DeserializeObject<City[]>(result);
            }
            City.ResetStaticID();
            return cities;
        }
        public Country[] DeserializeCountries()
        {
            Country[] countries;
            using (new Timer("country"))
            {
                string result = File.ReadAllText(FileCheck.GetFileName(1));
                countries = JsonConvert.DeserializeObject<Country[]>(result);
            }
            Country.ResetStaticID();
            return countries;
        }
        public Airport[] DeserializeAirports()
        {
            Airport[] airports;
            using (new Timer("airport"))
            {
                string result = File.ReadAllText(FileCheck.GetFileName(2));
                airports = JsonConvert.DeserializeObject<Airport[]>(result);
            }
            return airports;
        }
    }
}
