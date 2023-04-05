using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public sealed class CountryWithCoordinatesSpecification : BaseSpecification<AdministrativeUnit>
{
    public CountryWithCoordinatesSpecification(string unitName)
        : base(p => p.Name == unitName && p.AdministrativeLevel == 2)
    {
        AddInclude(p => p.AdministrativeUnitPolygons);
        AddInclude(p => p.Country);
    }
}