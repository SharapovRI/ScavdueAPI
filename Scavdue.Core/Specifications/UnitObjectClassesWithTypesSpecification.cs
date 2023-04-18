using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public class UnitObjectClassesWithTypesSpecification : BaseSpecification<UnitObjectClass>
{
    public UnitObjectClassesWithTypesSpecification()
        : base(b => true)
    {
        AddInclude(p => p.UnitObjectTypes);
    }
}