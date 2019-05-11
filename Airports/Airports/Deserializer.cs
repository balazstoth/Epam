using Newtonsoft.Json;
using System.IO;

namespace Airports
{
    class Deserializer
    {
        public City[] DeserializeCities()
        {
            City[] cities;
            using (StreamReader sr = new StreamReader(FileCheck.GetFileName(0)))
                cities = JsonConvert.DeserializeObject<City[]>(sr.ReadToEnd());
            
            return cities;
        }
        public Country[] DeserializeCountries()
        {
            Country[] countries;
            using (StreamReader sr = new StreamReader(FileCheck.GetFileName(1)))
                countries = JsonConvert.DeserializeObject<Country[]>(sr.ReadToEnd());

            return countries;
        }
        public Airport[] DeserializeAirports()
        {
            Airport[] airports;
            using (StreamReader sr = new StreamReader(FileCheck.GetFileName(2)))
                airports = JsonConvert.DeserializeObject<Airport[]>(sr.ReadToEnd());

            return airports;
        }
    }
}
