using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class UnitObjectTypeRepository : BaseRepository<UnitObjectType>, IUnitObjectTypeRepository
{
    public UnitObjectTypeRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }
}