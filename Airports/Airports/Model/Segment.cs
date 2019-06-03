using Newtonsoft.Json;

namespace Airports.Model
{
    class Segment
    {
        private static int currentID = 1;

        public int Id { get; set; }
        public int AirlineId { get => Airline == null ? -1 : Airline.Id; }
        public int ArrivalAirportId { get => ArrivalAirport == null ? -1 : ArrivalAirport.Id; }
        public int DepartureAirportId { get => DepartureAirport == null ? -1 : DepartureAirport.Id; }

        [JsonIgnore]
        public int tmpAirlineId { get; }

        [JsonIgnore]
        public int tmpArrivalAirportId { get; }

        [JsonIgnore]
        public int tmpDepartureId { get; }

        [JsonIgnore]
        public Airline Airline { get; set; }

        [JsonIgnore]
        public Airport ArrivalAirport { get; set; }

        [JsonIgnore]
        public Airport DepartureAirport { get; set; }

        public Segment(int id, int airlineId, int arrivalAirportId, int departureAirportId)
        {
            Id = id;
            tmpAirlineId = airlineId;
            tmpArrivalAirportId = arrivalAirportId;
            tmpDepartureId = departureAirportId;
        }
        public Segment(int id, Airline airline, Airport arrivalAirport, Airport departureAirport)
        {
            Id = id == -1 ? currentID++ : id;
            Airline = airline;
            ArrivalAirport = arrivalAirport;
            DepartureAirport = departureAirport;
        }
    }
}
