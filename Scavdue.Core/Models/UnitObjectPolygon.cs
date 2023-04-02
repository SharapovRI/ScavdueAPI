using Scavdue.Core.Interfaces.Models;

namespace Scavdue.Core.Models
{
    public partial class UnitObjectPolygon : Entity
    {
        public int UnitObjectId { get; set; }

        public virtual UnitObject UnitObject { get; set; } = null!;

        public float CenterLong { get; set; }

        public float CenterLat { get; set; }

        public string Coordinates { get; set; } = string.Empty;
    }
}
