namespace Scavdue.Core.Models;

public partial class EvaluationCriteriaType : Entity
{
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<EvaluationCriteria> EvaluationCriterias { get; } = new List<EvaluationCriteria>();
}