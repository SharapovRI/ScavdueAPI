using Scavdue.Core.Models;

namespace Scavdue.Core.Interfaces;

public interface IUnitObjectAdapter
{
    Task<List<UnitObject>> GetUnitObjects(string unitName, int adminLevel, List<UnitObjectClass> classes);
}