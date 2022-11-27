using Airports.DAL.PatternParser;
using Newtonsoft.Json;

namespace Airports.DAL.Model
{
    class City
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        [PatternProperty(".datRead", "cityName")]
        [PatternKey(".datRead")]
        public string Name { get; set; }

        [JsonProperty("countryId")]
        public long CountryId { get { if (Country != null) return Country.Id; return 0; } }

        /// <summary>
        /// This property is present because it's a PatternKey
        /// </summary>
        [PatternProperty(".datRead", "countryName")]
        [PatternKey(".datRead")]
        [JsonIgnore]
        public string CountryName { get; set; }

        [JsonProperty("timeZoneName")]
        public string TimeZoneName { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }
    }
}
