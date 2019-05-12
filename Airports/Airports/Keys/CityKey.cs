using System;

namespace Airports
{
    struct CityKey
    {
        public string CityName { get; set; }
        public int CountryID { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(CityName, CountryID);
        }
    }
}
