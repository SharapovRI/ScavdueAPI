using Newtonsoft.Json;
using Scavdue.OpenStreetMaps.Adapter.Interfaces;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Elements
{
    public class BuildingElement<TTags> : IElements<TTags> where TTags : class, ITags
    {
        [JsonProperty("center")]
        public Center Center { get; set; }

        [JsonProperty("geometry")]
        public Geometry[] Geometry { get; set; }

        [JsonProperty("members")]
        public Polygon[] Polygons { get; set; }

        [JsonProperty("bounds")]
        public Bounds Bounds { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("lat")]
        public float Lat { get; set; }

        [JsonProperty("lon")]
        public float Lon { get; set; }

        public TTags tags { get; set; }
    }
}
