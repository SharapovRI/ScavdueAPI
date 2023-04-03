using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Scavdue.Business.Interfaces;
using Scavdue.Business.Models.Response;
using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;
using Scavdue.Core.Specifications;

namespace Scavdue.Business.Services;

public class AdministrativeUnitService : IAdministrativeUnitService
{
    private readonly IMapper _mapper;
    private readonly IAdministrativeUnitRepository _administrativeUnitRepository;

    public AdministrativeUnitService(IMapper mapper, IAdministrativeUnitRepository administrativeUnitRepository)
    {
        _mapper = mapper;
        _administrativeUnitRepository = administrativeUnitRepository;
    }

    public async Task<IList<UnitByNameResponseModel>> GetUnitListByNameAsync(string unitName)
    {
        if (string.IsNullOrWhiteSpace(unitName))
        {
            throw new Exception("Bad request");
        }

        var units = await _administrativeUnitRepository.GetList(new UnitWithParentSpecification(unitName));
        var result = _mapper.Map<IList<UnitByNameResponseModel>> (units.ToList());
        return result;
    }

    public async Task<UnitWithCoordinatesResponseModel> GetCountryAsync(string unitName)
    {
        if (string.IsNullOrWhiteSpace(unitName))
        {
            throw new Exception("Bad request");
        }

        var unit = await _administrativeUnitRepository.GetList(new CountryWithCoordinatesSpecification(unitName));

        return _mapper.Map<UnitWithCoordinatesResponseModel>(unit.FirstOrDefault());
    }

    public async Task<IList<UnitWithCoordinatesResponseModel>> GetChildsAsync(int parentId)
    {
        var units = await _administrativeUnitRepository.GetList(new ChildUnitsByParentIdSpecification(parentId));

        return _mapper.Map<List<UnitWithCoordinatesResponseModel>>(units.ToList());
    }
}