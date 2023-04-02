using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class UnitObjectPolygonRepository : BaseRepository<UnitObjectPolygon>, IUnitObjectPolygonRepository
{
    public UnitObjectPolygonRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }
}