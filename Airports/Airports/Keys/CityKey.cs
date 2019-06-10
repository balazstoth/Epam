using System;

namespace Airports
{
    struct CityKey
    {
        [Key("city_name")]
        public string CityName { get; set; }

        [Key("country_id")]
        public int CountryID { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(CityName, CountryID);
        }
    }
}
