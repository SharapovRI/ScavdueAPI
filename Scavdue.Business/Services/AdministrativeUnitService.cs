using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Scavdue.Business.Interfaces;
using Scavdue.Business.Models.Response;
using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;
using Scavdue.Core.Specifications;
using System.Collections.Generic;

namespace Scavdue.Business.Services;

public class AdministrativeUnitService : IAdministrativeUnitService
{
    private readonly IMapper _mapper;
    private readonly IAdministrativeUnitRepository _administrativeUnitRepository;
    private readonly IAdministrativeUnitAdapter _administrativeUnitAdapter;

    public AdministrativeUnitService(IMapper mapper, IAdministrativeUnitRepository administrativeUnitRepository, 
        IAdministrativeUnitAdapter administrativeUnitAdapter)
    {
        _mapper = mapper;
        _administrativeUnitRepository = administrativeUnitRepository;
        _administrativeUnitAdapter = administrativeUnitAdapter;
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

        if (unit != null && unit.Any())
        {
            return _mapper.Map<UnitWithCoordinatesResponseModel>(unit.FirstOrDefault());
        }

        var country = await _administrativeUnitAdapter.GetCountry(unitName);
        country = await CreateAsync(country);

        return _mapper.Map<UnitWithCoordinatesResponseModel>(country);
    }

    public async Task<IList<UnitWithCoordinatesResponseModel>> GetChildsAsync(int parentId)
    {
        var units = await _administrativeUnitRepository.GetList(new ChildUnitsByParentIdSpecification(parentId));

        if (units != null && units.Any())
        {
            return _mapper.Map<List<UnitWithCoordinatesResponseModel>>(units.ToList());
        }

        var parentResultList = await _administrativeUnitRepository.GetList(new UnitWithCountrySpecification(parentId));
        var parent = parentResultList.FirstOrDefault();
        if (parent is null)
        {
            throw new Exception("There is no unit with this ID in the database");
        }

        var childUnits = await _administrativeUnitAdapter.GetChildUnits(parent.Id, parent.Name,
            parent.AdministrativeLevel, parent.Country.Id, parent.Country.Iso3166);
        childUnits = await CreateAsync(childUnits);

        return _mapper.Map<List<UnitWithCoordinatesResponseModel>>(childUnits);
    }

    private async Task<AdministrativeUnit> CreateAsync(AdministrativeUnit administrativeUnit)
    {
        var result = await _administrativeUnitRepository.CreateAsync(administrativeUnit);

        return result;
    }

    private async Task<List<AdministrativeUnit>> CreateAsync(List<AdministrativeUnit> administrativeUnits)
    {
        List<AdministrativeUnit> units = new();
        foreach (var unit in administrativeUnits)
        {
            units.Add(await _administrativeUnitRepository.CreateAsync(unit));
        }

        return units;
    }
}