using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public sealed class ChildUnitsByParentIdSpecification : BaseSpecification<AdministrativeUnit>
{
    public ChildUnitsByParentIdSpecification(int id)
        : base(p => p.ParentAdministrativeUnitId == id)
    {
        AddInclude(p => p.AdministrativeUnitPolygons);
    }
}