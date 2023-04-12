using Newtonsoft.Json;
using Scavdue.OpenStreetMaps.Adapter.Interfaces;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Tags
{
    public class BuildingTags : ITags
    {
        [JsonProperty("addr:housenumber")]
        public string Addrhousenumber { get; set; }

        [JsonProperty("addr:street")]
        public string Addrstreet { get; set; }

        [JsonProperty("building")]
        public string Building { get; set; }

        [JsonProperty("amenity")]
        public string Amenity { get; set; }

        [JsonProperty("emergency")]
        public string Emergency { get; set; }

        [JsonProperty("building:levels")]
        public string BuildingLevels { get; set; }

        [JsonProperty("name:ru")]
        public string Name { get; set; }
    }
}
