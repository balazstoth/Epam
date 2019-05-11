using System;

namespace Airports
{
    class City
    {
        private static int currentID = 1;

        public int CountryId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeZoneName { get { return TimeZoneInfo?.DisplayName; } }
        public TimeZoneInfo TimeZoneInfo { get; }

        public City(int countryID, string name, TimeZoneInfo timeZoneInfo)
        {
            Id = currentID++;
            Name = name;
            CountryId = countryID;
            this.TimeZoneInfo = timeZoneInfo;
        }
    }
}
