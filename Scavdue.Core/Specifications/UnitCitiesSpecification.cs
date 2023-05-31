using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public class UnitCitiesSpecification : BaseSpecification<AdministrativeUnit>
{
    public UnitCitiesSpecification(int countryId, int minAdminLevel)
        : base(p => p.CountryId == countryId && p.AdministrativeLevel >= minAdminLevel && p.Place != null)
    {
    }
}