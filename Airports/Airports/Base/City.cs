using System;
using System.Collections.Generic;
using System.Text;

namespace Airports
{
    class City
    {
        private static int currentID = 1;

        public int CountryId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeZoneName { get; set; }

        public City(int countryID, string name, string timeZoneName)
        {
            Id = currentID++;
            Name = name;
            CountryId = countryID;
        }
    }
}
