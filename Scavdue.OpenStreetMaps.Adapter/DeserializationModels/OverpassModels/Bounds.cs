using Newtonsoft.Json;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels
{
    public class Bounds
    {
        [JsonProperty("minlat")]
        public float MinLat { get; set; }

        [JsonProperty("minlon")]
        public float MinLon { get; set; }

        [JsonProperty("maxlat")]
        public float MaxLat { get; set; }

        [JsonProperty("maxlon")]
        public float MaxLon { get; set; }
    }
}
