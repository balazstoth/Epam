using Newtonsoft.Json;
using System;

namespace Airports
{
    class City
    {
        private static int currentID = 1;

        public string TimeZoneId { get; }
        public int CountryId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public TimeZoneInfo TimeZoneInfo { get; }

        public City(int countryID, string name, string timeZoneId)
        {
            Id = currentID++;
            Name = name;
            CountryId = countryID;
            TimeZoneId = timeZoneId;
            TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }

        public override string ToString()
        {
            return Name;
        }
        public static void ResetStaticID()
        {
            City.currentID = 1;
        }
    }
}
