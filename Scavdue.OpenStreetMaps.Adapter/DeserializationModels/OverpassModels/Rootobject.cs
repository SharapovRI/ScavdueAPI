using Newtonsoft.Json;
using Scavdue.OpenStreetMaps.Adapter.Interfaces;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels
{
    public class Rootobject<TElement, TTags> where TElement : class, IElements<TTags> where TTags : class, ITags
    {
        [JsonProperty("elements")]
        public TElement[] Elements { get; set; }
    }
}
