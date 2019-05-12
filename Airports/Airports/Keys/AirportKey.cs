using System;

namespace Airports
{
    struct AirportKey
    {
        public string AirportName { get; set; }
        public int CityID { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(AirportName, CityID);
        }
    }
}
