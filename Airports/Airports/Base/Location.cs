namespace Airports
{
    struct Location
    {
        public double Altitude { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Location(double lon, double lat, double alt)
        {
            Altitude = alt;
            Latitude = lat;
            Longitude = lon;
        }
    }
}
