using System;
using System.Collections.Generic;
using System.Text;

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
        public string TimeZoneName { get; set; }

        public Airport()
        {
        }

        public Airport(int id, int countryID, int cityID, string IATA, string ICAO, string name, string timeZoneName)
        {
            Id = id;
            CityId = cityID;
            IATACode = IATA;
            ICAOCode = ICAO;
            Name = name;
            TimeZoneName = timeZoneName;
        }
    }
}
