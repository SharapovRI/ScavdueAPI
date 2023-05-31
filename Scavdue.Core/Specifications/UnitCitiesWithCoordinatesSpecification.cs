namespace Scavdue.Core.Specifications;

public class UnitCitiesWithCoordinatesSpecification : UnitCitiesSpecification
{
    public UnitCitiesWithCoordinatesSpecification(int countryId, int minAdminLevel) 
        : base(countryId, minAdminLevel)
    {
        AddInclude(p => p.AdministrativeUnitPolygons);
    }
}
