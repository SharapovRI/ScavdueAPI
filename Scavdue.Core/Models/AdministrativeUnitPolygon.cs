namespace Scavdue.Core.Models
{
    public partial class AdministrativeUnitPolygon : Entity
    {
        public int AdministrativeUnitId { get; set; }

        public virtual AdministrativeUnit AdministrativeUnit { get; set; } = null!;

        public float? CenterLong { get; set; }

        public float? CenterLat { get; set; }

        public string Coordinates { get; set; } = string.Empty;
    }
}
