using Newtonsoft.Json;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.NominantimModels;
using Scavdue.OpenStreetMaps.Adapter.Interfaces;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Elements
{
    public class UnitElement<TTags> : IElements<TTags> where TTags : class, ITags
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("tags")]
        public TTags Tags { get; set; }

        [JsonProperty("NominatimRoot")]
        public Place NominatimRoot;
    }
}
