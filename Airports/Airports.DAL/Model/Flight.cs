using Airports.DAL.PatternParser;
using Newtonsoft.Json;

namespace Airports.DAL.Model
{
    class Flight
    {
        public int id { get; set; }

        public string number { get; set; }

        [Column("segmentId")]
        public int SegmentId { get; set; }

        [Column("departureTime")]
        public string DepartureTime { get; set; }

        public string arrivalTime { get; set; }

        public Segment Segment { get; set; } 
        // design pattern ötlet
        // ha nincs meg neki, akkor segmentId alapján elkér egy gyűjteményt (hol határozom meg(?)), amiből előkeresi
        // elmenti egy field-be, hogy ne kelljen újra előkeresni. Ha megváltozik a SegmentId, akkor null-ozza a Segment-et is.
    }
}
