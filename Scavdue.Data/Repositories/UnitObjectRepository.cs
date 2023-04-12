using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class UnitObjectRepository : BaseRepository<UnitObject>, IUnitObjectRepository
{
    public UnitObjectRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }

    public async Task<List<UnitObject>> CreateRangeAsync(List<UnitObject> entities)
    {
        await _set.AddRangeAsync(entities);

        await _context.SaveChangesAsync();

        return entities;
    }
}