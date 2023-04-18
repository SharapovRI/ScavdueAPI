using Newtonsoft.Json;
using Scavdue.OpenStreetMaps.Adapter.Interfaces;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Tags
{
    public class ChildUnitTags : ITags
    {
        [JsonProperty("ISO3166-2")]
        public string Iso31662 { get; set; }

        [JsonProperty("addr:country")]
        public string Addrcountry { get; set; }

        [JsonProperty("admin_level")]
        public int AdminLevel { get; set; }

        [JsonProperty("name:ru")]
        public string Name { get; set; }

        [JsonProperty("population")]
        public string Population { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }
    }
}
