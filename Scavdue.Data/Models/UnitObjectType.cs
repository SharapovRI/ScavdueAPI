using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Data.Models
{
    public partial class UnitObjectType : Entity
    {
        public string Name { get; set; } = null!;

        public int UnitObjectClassId { get; set; }

        public virtual UnitObjectClass UnitObjectClass { get; set; } = null!;

        public virtual ICollection<UnitObject> UnitObjects { get; } = new List<UnitObject>();
    }
}
