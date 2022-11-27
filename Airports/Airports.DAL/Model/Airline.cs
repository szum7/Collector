using Airports.DAL.PatternParser;

namespace Airports.DAL.Model
{
    [RegexValidator(@"^[1-9]+\d?,[A-Z0-9]{2},[A-Z0-9]{3},[^,]+,[^,]+$")]
    class Airline
    {
        [Column("iata")]
        public string IATACode { get; set; }

        public string name { get; set; }

        [Column("callsign")]
        public string CallSign { get; set; }

        [Column("icao")]
        public string ICAOCode { get; set; }

        public int id { get; set; }
    }
}
