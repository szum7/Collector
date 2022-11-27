using Airports.DAL.PatternParser;
using Newtonsoft.Json;

namespace Airports.DAL.Model
{
    class Country
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        [PatternProperty(".datRead", "countryName")]
        [PatternKey(".datRead")]
        public string Name { get; set; }

        [JsonProperty("twoLetterISOCode")]
        public string TwoLetterIsoCode { get; set; }

        [JsonProperty("threeLetterISOCode")]
        public string ThreeLetterIsoCode { get; set; }
    }
}
