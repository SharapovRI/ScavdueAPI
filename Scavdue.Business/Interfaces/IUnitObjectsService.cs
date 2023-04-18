using Scavdue.Core.Models;

namespace Scavdue.Business.Interfaces;

public interface IUnitObjectsService
{
    Task<List<UnitObject>> GetUnitObjects(int unitId);
    Task<List<UnitObject>> GetUnitObjectsAdmin();
}