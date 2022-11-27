using Airports.DAL.PatternParser;

namespace Airports.DAL.Model
{
    class Segment
    {
        public int id { get; set; }

        [Column("departureAirport")]
        public int DepartureAirportId { get; set; }

        [Column("airline")]
        public int airlineId { get; set; }

        [Column("arrivalAirport")]
        public int arrivalAirportId { get; set; }

        public Airline Airline { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }
    }
}
