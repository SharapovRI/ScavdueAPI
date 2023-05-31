using Scavdue.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Core.Specifications
{
    public class UnitObjectsWithClassesSpecification : BaseSpecification<UnitObject>
    {
        public UnitObjectsWithClassesSpecification(int unitId)
            : base(p => p.AdministrativeUnitId == unitId)
        {
            AddInclude(p => p.UnitObjectType);
            AddInclude(p => p.UnitObjectType.UnitObjectClass);
        }
    }
}
