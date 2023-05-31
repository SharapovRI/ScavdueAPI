using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public sealed class UnitObjectAssessmentSpecification : BaseSpecification<AdministrativeUnit>
{
    public UnitObjectAssessmentSpecification()
        : base(p => p.UnitObjects != null && p.UnitObjects.Count > 0 && p.Populations != null && p.Populations.Count > 0)
    {
        AddInclude(p => p.Populations);
        AddInclude(p => p.UnitObjects);
        AddInclude("UnitObjects.UnitObjectType");
        AddInclude("UnitObjects.UnitObjectPolygons");
        AddInclude("UnitObjects.UnitObjectType.UnitObjectClass");
    }
}