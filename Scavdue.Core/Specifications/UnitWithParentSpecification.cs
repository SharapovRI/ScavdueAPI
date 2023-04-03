using System.Linq.Expressions;
using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public sealed class UnitWithParentSpecification : BaseSpecification<AdministrativeUnit>
{
    public UnitWithParentSpecification(string unitName) 
        : base(p => p.Name == unitName)
    {
        AddInclude(p => p.ParentAdministrativeUnit);
        AddInclude(p => p.Country);
    }
}