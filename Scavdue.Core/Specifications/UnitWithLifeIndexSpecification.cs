using Scavdue.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Core.Specifications;

public class UnitWithLifeIndexSpecification : BaseSpecification<AdministrativeUnit>
{
    public UnitWithLifeIndexSpecification(int unitId)
        : base(p => p.Id == unitId)
    {
        AddInclude(p => p.LifeIndexes);
        AddInclude("LifeIndexes.EvaluationCriterias");
        AddInclude("LifeIndexes.EvaluationCriterias.EvaluationCriteriaType");
    }
}
