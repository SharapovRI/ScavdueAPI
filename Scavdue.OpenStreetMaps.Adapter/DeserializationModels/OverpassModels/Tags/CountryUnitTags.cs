using Newtonsoft.Json;
using Scavdue.OpenStreetMaps.Adapter.Interfaces;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Tags
{
    public class CountryUnitTags : ITags
    {
        [JsonProperty("ISO31661")]
        public string Iso31661 { get; set; }

        [JsonProperty("addrcountry")]
        public string Addrcountry { get; set; }

        [JsonProperty("admin_level")]
        public int AdminLevel { get; set; }

        [JsonProperty("name:ru")]
        public string Name { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }
    }
}
