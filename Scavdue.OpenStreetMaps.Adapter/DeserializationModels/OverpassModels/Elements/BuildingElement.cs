using Newtonsoft.Json;
using Scavdue.OpenStreetMaps.Adapter.Interfaces;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Elements
{
    public class BuildingElement<TTags> : IElements<TTags> where TTags : class, ITags
    {
        [JsonProperty("center")]
        public Center Center { get; set; }

        [JsonProperty("tags")]
        public TTags Tags { get; set; }
    }
}
