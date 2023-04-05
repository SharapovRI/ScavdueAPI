using Scavdue.Core.Models;

namespace Scavdue.Core.Interfaces;

public interface IAdministrativeUnitAdapter
{
    Task<AdministrativeUnit> GetCountry(string country);

    Task<List<AdministrativeUnit>> GetChildUnits(int id, string parentName, int admin_level, int countryId, string countryName);
}