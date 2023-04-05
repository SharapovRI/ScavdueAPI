using Newtonsoft.Json;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels
{
    public class Center
    {
        [JsonProperty("lat")]
        public float Lat { get; set; }

        [JsonProperty("lon")]
        public float Lon { get; set; }
    }
}
