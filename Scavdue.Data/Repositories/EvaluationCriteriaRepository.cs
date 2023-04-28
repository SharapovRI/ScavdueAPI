using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class EvaluationCriteriaRepository : BaseRepository<EvaluationCriteria>, IEvaluationCriteriaRepository
{
    public EvaluationCriteriaRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }
}