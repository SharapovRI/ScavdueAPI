using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public sealed class UnitObjectAssessmentSpecification : BaseSpecification<UnitObject>
{
    public UnitObjectAssessmentSpecification()
        : base(p => p.AdministrativeUnitId != null)
    {
        AddInclude(p => p.UnitObjectType);
        AddInclude(p => p.UnitObjectPolygons);
        AddInclude("UnitObjectType.UnitObjectClass");
    }
}