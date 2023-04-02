using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;

namespace Scavdue.Data.Repositories;

public class AdministrativeUnitRepository : BaseRepository<AdministrativeUnit>, IAdministrativeUnitRepository
{
    public AdministrativeUnitRepository(ScavdueApiDbContext scavdueApiDbContext)
        : base(scavdueApiDbContext)
    {
    }
}