using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class PopulationRepository : BaseRepository<Population>, IPopulationRepository
{
    public PopulationRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }
}