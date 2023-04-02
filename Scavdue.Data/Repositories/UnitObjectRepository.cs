using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class UnitObjectRepository : BaseRepository<UnitObject>, IUnitObjectRepository
{
    public UnitObjectRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }
}