using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class UnitObjectClassRepository : BaseRepository<UnitObjectClass>, IUnitObjectClassRepository
{
    public UnitObjectClassRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }
}