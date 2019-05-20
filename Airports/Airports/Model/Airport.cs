using Newtonsoft.Json;
using System;

namespace Airports
{
    class Airport
    {
        public int Id { get; set; }
        public int CountryId { get => Country.Id; }
        public int CityId { get => City.Id; }
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
        public string Name { get; set; }
        public string TimeZoneId { get; set; }
        public Location Location { get; set; }

        [JsonIgnore]
        public City City { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }

        [JsonIgnore]
        public int tmpCityId { get; }

        [JsonIgnore]
        public int tmpCountryId { get; }

        [JsonIgnore]
        public string FullName => Name.ToLower().Contains("airport") ? Name : Name + " Airport";

        [JsonIgnore]
        public TimeZoneInfo timeZoneInfo { get; }

        [JsonConstructor]
        public Airport(int id, int countryId, int cityId, string IATACode, string ICAOCode, string name, string timeZoneId, Location location)
        {
            Id = id;
            tmpCountryId = countryId;
            tmpCityId = cityId;
            this.IATACode = IATACode;
            this.ICAOCode = ICAOCode;
            Name = name;
            TimeZoneId = timeZoneId;
            Location = location;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
        }

        public Airport(int id, Country country, City city, string IATACode, string ICAOCode, string name, string timeZoneId, Location location)
        {
            Id = id;
            Country = country;
            City = city;
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
