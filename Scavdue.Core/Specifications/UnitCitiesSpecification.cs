using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public sealed class UnitCitiesSpecification : BaseSpecification<AdministrativeUnit>
{
    public UnitCitiesSpecification(int minAdminLevel)
        : base(p => p.AdministrativeLevel >= minAdminLevel && p.Place != null)
    {
    }
}