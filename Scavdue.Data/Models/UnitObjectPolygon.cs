using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Data.Models
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
