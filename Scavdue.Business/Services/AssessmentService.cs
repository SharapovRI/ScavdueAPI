using AutoMapper;
using Scavdue.Business.Interfaces;
using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;
using Scavdue.Core.Specifications;

namespace Scavdue.Business.Services;

public class AssessmentService : IAssessmentService
{
    private readonly IMapper _mapper;
    private readonly IAdministrativeUnitRepository _administrativeUnitRepository;
    private readonly IAdministrativeUnitAdapter _administrativeUnitAdapter;
    private readonly IUnitObjectAdapter _unitObjectAdapter;
    private readonly IUnitObjectRepository _unitObjectRepository;
    private readonly IUnitObjectClassRepository _unitObjectClassRepository;

    public AssessmentService(IMapper mapper, IAdministrativeUnitRepository administrativeUnitRepository,
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

    public async Task<List<int>> DoComplexAssessment()
    {
        var units = await _unitObjectRepository.GetList(new UnitObjectAssessmentSpecification());

        if (units is null)
        {
            throw new Exception("There is no unit objects for assessment");
        }

        var buildingClasses = await _unitObjectClassRepository.GetList(new UnitObjectClassesWithTypesSpecification());

        

        return null;
    }
}