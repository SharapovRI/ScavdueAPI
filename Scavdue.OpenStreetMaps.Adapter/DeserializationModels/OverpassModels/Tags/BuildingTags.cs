using Newtonsoft.Json;
using Scavdue.OpenStreetMaps.Adapter.Interfaces;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Tags
{
    public class BuildingTags : ITags
    {
        [JsonProperty("addrhousenumber")]
        public string Addrhousenumber { get; set; }

        [JsonProperty("addrstreet")]
        public string Addrstreet { get; set; }

        [JsonProperty("building")]
        public string Building { get; set; }

        [JsonProperty("building:levels")]
        public string Buildinglevels { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
