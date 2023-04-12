using Scavdue.Core.Models;

namespace Scavdue.Core.Interfaces;

public interface IUnitObjectRepository : IBaseRepository<UnitObject>
{
    Task<List<UnitObject>> CreateRangeAsync(List<UnitObject> entities);
}