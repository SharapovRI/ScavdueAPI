namespace Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels
{
    public class Member
    {
        public string type { get; set; }
        public string role { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public Geometry[] geometry { get; set; }
    }
}
