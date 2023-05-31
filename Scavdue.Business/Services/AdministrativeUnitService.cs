using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Scavdue.Business.Interfaces;
using Scavdue.Business.Models.Response;
using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;
using Scavdue.Core.Specifications;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Scavdue.Business.Services;

public class AdministrativeUnitService : IAdministrativeUnitService
{
    private readonly IMapper _mapper;
    private readonly IAdministrativeUnitRepository _administrativeUnitRepository;
    private readonly IAdministrativeUnitAdapter _administrativeUnitAdapter;
    private readonly IUnitObjectsService _unitObjectsService;
    private readonly IAssessmentService _assessmentService;
    private readonly ILifeIndexRepository _lifeIndexRepository;
    private readonly DbContext _context;

    public AdministrativeUnitService(IMapper mapper, 
        IAdministrativeUnitRepository administrativeUnitRepository, 
        IAdministrativeUnitAdapter administrativeUnitAdapter, 
        IUnitObjectsService unitObjectsService, 
        IAssessmentService assessmentService, 
        ILifeIndexRepository lifeIndexRepository,
        IDatabaseContext context
        )
    {
        _mapper = mapper;
        _administrativeUnitRepository = administrativeUnitRepository;
        _administrativeUnitAdapter = administrativeUnitAdapter;
        _unitObjectsService = unitObjectsService;
        _context = (DbContext)context;
        _assessmentService = assessmentService;
        _lifeIndexRepository = lifeIndexRepository;
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

    public async Task<IList<UnitWithCoordinatesResponseModel>> GetCitiesList(int countryId)
    {
        if (countryId == null)
        {
            throw new Exception("Bad request");
        }

        var units = await _administrativeUnitRepository.GetList(new UnitCitiesWithCoordinatesSpecification(countryId, 4));

        return _mapper.Map<IList<UnitWithCoordinatesResponseModel>>(units);
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

        return null;

        //var country = await _administrativeUnitAdapter.GetCountry(unitName);
        //country = await CreateAsync(country);

        //return _mapper.Map<UnitWithCoordinatesResponseModel>(country);
    }

    public async Task<UnitWithLifeIndexResponseModel> GetUnitLifeIndex(int unitId)
    {
        var units = await _administrativeUnitRepository.GetList(new UnitWithLifeIndexSpecification(unitId));
        if (units != null && units.Any())
        {
            return _mapper.Map<UnitWithLifeIndexResponseModel>(units.First());
        }

        return null;
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

    public async Task<string> AdminComplexAdminUnits(string countryName)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        var country = await AdminGetCountryAsync(countryName);
        List<AdministrativeUnit> units = await _administrativeUnitAdapter.GetCountryCities(country.Id, country.Name,
            country.AdministrativeLevel, country.Country.Id, country.Country.Iso3166);

        units = await CreateAsync(units);

        try
        {
            var objects = await _unitObjectsService.GetUnitObjectsAdmin(country.Country.Id);
            var a = 0;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw new Exception("Transaction is canceled!");
        }
        //await _assessmentService.DoComplexAssessment();

        return "Complex updating complete successful";
    }

    public async Task<string> TestUnits()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var objects = await _assessmentService.DoComplexAssessment();
            var a = 0;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception("Transaction is canceled!");
        }
        return "Complex updating complete successful";
    }

    public async Task<AdministrativeUnit> AdminGetCountryAsync(string countryName)
    {
        if (string.IsNullOrWhiteSpace(countryName))
        {
            throw new Exception("Bad request");
        }

        var country = await _administrativeUnitAdapter.GetCountry(countryName);
        return await CreateAsync(country);
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