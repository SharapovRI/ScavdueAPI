using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class EvaluationCriteriaTypeRepository : BaseRepository<EvaluationCriteriaType>, IEvaluationCriteriaTypeRepository
{
    public EvaluationCriteriaTypeRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }
}