using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Data.Models
{
    public partial class Population : Entity
    {
        public int AdministrativeUnitId { get; set; }

        public virtual AdministrativeUnit AdministrativeUnit { get; set; } = null!;

        public int NumberOfPeople { get; set; }

        public DateOnly Date { get; set; }
    }
}
