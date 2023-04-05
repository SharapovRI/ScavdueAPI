﻿using Newtonsoft.Json;

namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.NominantimModels
{
    public class Place
    {
        [JsonProperty("place_id")]
        public int PlaceId { get; set; }

        [JsonProperty("licence")]
        public string Licence { get; set; }

        [JsonProperty("osm_type")]
        public string OsmType { get; set; }

        [JsonProperty("osm_id")]
        public long OsmId { get; set; }

        [JsonProperty("boundingbox")]
        public string[] Boundingbox { get; set; }

        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lon")]
        public string Lon { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("place_rank")]
        public int PlaceRank { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("importance")]
        public float Importance { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("geojson")]
        public Geojson Geojson { get; set; }
    }
}