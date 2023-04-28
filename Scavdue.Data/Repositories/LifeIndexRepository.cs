using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class LifeIndexRepository : BaseRepository<LifeIndex>, ILifeIndexRepository
{
    public LifeIndexRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }
}