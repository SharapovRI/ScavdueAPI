using Scavdue.Business.Models.Response;
using Scavdue.Core.Models;

namespace Scavdue.Business.Interfaces;

public interface IAdministrativeUnitService
{
    Task<IList<UnitByNameResponseModel>> GetUnitListByNameAsync(string unitName);

    Task<UnitWithCoordinatesResponseModel> GetCountryAsync(string unitName);

    Task<IList<UnitWithCoordinatesResponseModel>> GetChildsAsync(int parentId);

    Task<string> AdminComplexAdminUnits(string countryName);
    Task<string> TestUnits();
}