using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public class UnitWithCoordinatesSpecification : BaseSpecification<AdministrativeUnit>
{
    public UnitWithCoordinatesSpecification(int id)
        : base(p => p.Id == id)
    {
        AddInclude(p => p.UnitObjects);
        AddInclude("UnitObjects.UnitObjectType.UnitObjectClass");
        AddInclude("UnitObjects.UnitObjectPolygons");
    }
}