using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class AdministrativeUnitPolygonRepository : BaseRepository<AdministrativeUnitPolygon>, IAdministrativeUnitPolygonRepository
{
    public AdministrativeUnitPolygonRepository(ScavdueApiDbContext scavdueApiDbContext) 
        : base(scavdueApiDbContext)
    {
    }
}