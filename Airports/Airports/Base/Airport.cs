using System;

namespace Airports
{
    class Airport
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string FullName => Name + " Airport";
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
        public string Name { get; set; }
        public TimeZoneInfo timeZoneInfo { get; set; }
        public string TimeZoneName { get { return timeZoneInfo?.DisplayName; } }

        public Airport()
        {
        }

        public Airport(int id, int countryID, int cityID, string IATA, string ICAO, string name, TimeZoneInfo zoneInfo)
        {
            Id = id;
            CountryId = countryID;
            CityId = cityID;
            IATACode = IATA;
            ICAOCode = ICAO;
            Name = name;
            timeZoneInfo = zoneInfo;
        }
    }
}
