using AutoMapper;
using Scavdue.Business.Interfaces;
using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;
using Scavdue.Core.Specifications;

namespace Scavdue.Business.Services;

public class UnitObjectsService : IUnitObjectsService
{
    private readonly IMapper _mapper;
    private readonly IAdministrativeUnitRepository _administrativeUnitRepository;
    private readonly IAdministrativeUnitAdapter _administrativeUnitAdapter;
    private readonly IUnitObjectAdapter _unitObjectAdapter;
    private readonly IUnitObjectRepository _unitObjectRepository;
    private readonly IUnitObjectClassRepository _unitObjectClassRepository;

    public UnitObjectsService(IMapper mapper, IAdministrativeUnitRepository administrativeUnitRepository,
        IAdministrativeUnitAdapter administrativeUnitAdapter, IUnitObjectAdapter unitObjectAdapter, IUnitObjectClassRepository unitObjectClassRepository,
        IUnitObjectRepository unitObjectRepository)
    {
        _mapper = mapper;
        _administrativeUnitRepository = administrativeUnitRepository;
        _administrativeUnitAdapter = administrativeUnitAdapter;
        _unitObjectAdapter = unitObjectAdapter;
        _unitObjectRepository = unitObjectRepository;
        _unitObjectClassRepository = unitObjectClassRepository;
    }

    public async Task<List<UnitObject>> GetUnitObjects(int unitId)
    {
        var unit = (await _administrativeUnitRepository.GetList(new UnitWithCoordinatesSpecification(unitId))).FirstOrDefault();

        if (unit is null)
        {
            throw new Exception("There is no unit with this ID in the database");
        }

        if (unit.UnitObjects != null && unit.UnitObjects.Count > 0)
        {
            return unit.UnitObjects.ToList();
        }

        var buildingClasses = await _unitObjectClassRepository.GetList(new UnitObjectClassesWithTypes());

        var unitObjects = await _unitObjectAdapter.GetUnitObjects(unit.Name, unit.AdministrativeLevel, buildingClasses.ToList());
        foreach (var unitObject in unitObjects)
        {
            unitObject.AdministrativeUnit = unit;
        }
        await _unitObjectRepository.CreateRangeAsync(unitObjects);

        return unitObjects;
    }
}