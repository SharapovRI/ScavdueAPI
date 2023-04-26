namespace Scavdue.Core.Models;

public partial class LifeIndex : Entity
{
    public DateTime ReceivingDate { get; set; }

    public virtual ICollection<EvaluationCriteria> EvaluationCriterias { get; } = new List<EvaluationCriteria>();

    public int AdministrativeUnitId { get; set; }

    public virtual AdministrativeUnit AdministrativeUnit { get; set; } = null!;
}