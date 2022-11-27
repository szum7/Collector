using Airports.DAL.PatternParser;
using Newtonsoft.Json;

namespace Airports.DAL.Model
{
    class Airport
    {
        [JsonProperty("id")]
        [PatternProperty(".datRead", "id")]
        public long Id { get; set; }

        [JsonProperty("iataCode")]
        [PatternProperty(".datRead", "iataCode")]
        public string IATACode { get; set; }

        [JsonProperty("icaoCode")]
        [PatternProperty(".datRead", "icaoCode")]
        public string ICAOCode { get; set; }

        [JsonProperty("name")]
        [PatternProperty(".datRead", "name")]
        public string Name { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get => $"{Name} Airport"; }

        [JsonProperty("cityId")]
        public long CityId { get { if (City != null) return City.Id; return 0; } }

        [JsonProperty("countryId")]
        public long CountryId { get { if (Country != null) return Country.Id; return 0; } }

        [JsonProperty("timeZoneName")]
        public string TimeZoneName { get { if (City != null) return City.TimeZoneName; return null; } }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonIgnore]
        public City City { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }
    }
}
