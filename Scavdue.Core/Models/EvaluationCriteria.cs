namespace Scavdue.Core.Models;

public partial class EvaluationCriteria : Entity
{
    public float Value { get; set; }

    public string Description { get; set; } = string.Empty;

    public int EvaluationCriteriaTypeId { get; set; }

    public virtual EvaluationCriteriaType? EvaluationCriteriaType { get; set; } = null!;

    public int LifeIndexId { get; set; }

    public virtual LifeIndex LifeIndex { get; set; } = null!;
}