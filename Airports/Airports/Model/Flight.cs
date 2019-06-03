using Newtonsoft.Json;
using System;

namespace Airports.Model
{
    class Flight
    {
        private static int currentID = 1;
        private TimeSpan arrivalTime;
        private TimeSpan departureTime;

        [JsonIgnore]
        public int tmpSegmentId { get => Segment == null ? -1 : Segment.Id; }

        public int Id { get; set; }
        public int Number { get; set; }
        public int SegmentId { get; set; }
        public TimeSpan ArrivalTime { get => arrivalTime; set => arrivalTime = value; }
        public TimeSpan DepartureTime { get => departureTime; set => departureTime = value; }

        [JsonIgnore]
        public Segment Segment { get; set; }

        public Flight(int id, int number, int segmentId, string arrivalTime, string departureTime)
        {
            Id = id;
            Number = number;
            SegmentId = segmentId;
            if (!TimeSpan.TryParse(arrivalTime, out this.arrivalTime))
                throw new ArgumentException(arrivalTime);

            if (!TimeSpan.TryParse(departureTime, out this.departureTime))
                throw new ArgumentException(departureTime);
        }
        public Flight(int id, int number, Segment segment, TimeSpan arrivalTime, TimeSpan departureTime)
        {
            Id = id == -1 ? currentID++ : id;
            Number = number;
            Segment = segment;
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
        }
    }
}
