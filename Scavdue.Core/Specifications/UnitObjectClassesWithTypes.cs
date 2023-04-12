using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public class UnitObjectClassesWithTypes : BaseSpecification<UnitObjectClass>
{
    public UnitObjectClassesWithTypes()
        : base(b => true)
    {
        AddInclude(p => p.UnitObjectTypes);
    }
}