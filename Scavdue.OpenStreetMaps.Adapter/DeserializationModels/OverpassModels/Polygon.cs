using Newtonsoft.Json;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels
{
    public class Polygon
    {
        [JsonProperty("geometry")]
        public Geometry[] Geometry { get; set; }
    }
}