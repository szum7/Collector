using Airports.DAL.PatternParser;
using Newtonsoft.Json;

namespace Airports.DAL.Model
{
    class Location
    {
        [JsonProperty("longitude")]
        [PatternProperty(".datRead", "longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        [PatternProperty(".datRead", "latitude")]
        public double Latitude { get; set; }

        [JsonProperty("altitude")]
        [PatternProperty(".datRead", "altitude")]
        public double Altitude { get; set; }
    }
}
