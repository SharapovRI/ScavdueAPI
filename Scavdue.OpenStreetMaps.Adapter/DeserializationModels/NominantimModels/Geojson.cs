using Newtonsoft.Json;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.NominantimModels
{
    public class Geojson
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public object[] Coordinates { get; set; }
    }
}
