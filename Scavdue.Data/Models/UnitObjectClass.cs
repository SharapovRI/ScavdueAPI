using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Data.Models
{
    public partial class UnitObjectClass : Entity
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<UnitObjectType> UnitObjectTypes { get; } = new List<UnitObjectType>();
    }
}
