using Newtonsoft.Json;
using System;

namespace Airports
{
    class Airport
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
        public string Name { get; set; }
        public string TimeZoneId { get; set; }
        public Location Location { get; set; }

        [JsonIgnore]
        public string FullName => Name.ToLower().Contains("airport") ? Name : Name + " Airport";

        [JsonIgnore]
        public TimeZoneInfo timeZoneInfo { get; }

        [JsonIgnore]
        public string TimeZoneName { get { return timeZoneInfo?.DisplayName; } }

        public Airport(int id, int countryID, int cityID, string IATACode, string ICAOCode, string name, string timeZoneId, Location location)
        {
            Id = id;
            CountryId = countryID;
            CityId = cityID;
            this.IATACode = IATACode;
            this.ICAOCode = ICAOCode;
            Name = name;
            TimeZoneId = timeZoneId;
            Location = location;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
