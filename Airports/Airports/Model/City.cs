﻿using Newtonsoft.Json;
using System;

namespace Airports
{
    class City
    {
        private static int currentID = 1;

        public string TimeZoneId { get; }
        public int CountryId { get => Country == null ? -1 : Country.Id; }
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public int tmpCountryId { get; }

        [JsonIgnore]
        public Country Country { get; set; }

        [JsonIgnore]
        public TimeZoneInfo TimeZoneInfo { get; }

        [JsonConstructor]
        public City(int id, int countryId, string name, string timeZoneId)
        {
            Id = id;
            tmpCountryId = countryId;
            Name = name;
            Country = null;
            TimeZoneId = timeZoneId;
            TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        public City(Country country, string name, string timeZoneId)
        {
            Id = currentID++;
            Name = name;
            Country = country;
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
