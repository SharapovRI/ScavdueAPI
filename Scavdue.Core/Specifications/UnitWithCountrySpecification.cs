using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public class UnitWithCountrySpecification : BaseSpecification<AdministrativeUnit>
{
    public UnitWithCountrySpecification(int id)
        : base(p => p.Id == id)
    {
        AddInclude(p => p.Country);
    }
}