using System.Linq.Expressions;
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
    private readonly IEvaluationCriteriaTypeRepository _evaluationCriteriaTypeRepository;
    private readonly IEvaluationCriteriaRepository _evaluationCriteriaRepository;
    private readonly IAdministrativeUnitAdapter _administrativeUnitAdapter;
    private readonly IUnitObjectAdapter _unitObjectAdapter;
    private readonly IUnitObjectRepository _unitObjectRepository;
    private readonly IUnitObjectClassRepository _unitObjectClassRepository;
    private readonly ILifeIndexRepository _lifeIndexRepository;

    public AssessmentService(IMapper mapper, 
        IAdministrativeUnitRepository administrativeUnitRepository,
        IAdministrativeUnitAdapter administrativeUnitAdapter, 
        IUnitObjectAdapter unitObjectAdapter, 
        IUnitObjectClassRepository unitObjectClassRepository,
        IEvaluationCriteriaTypeRepository evaluationCriteriaTypeRepository,
        IEvaluationCriteriaRepository evaluationCriteriaRepository,
        ILifeIndexRepository lifeIndexRepository,
        IUnitObjectRepository unitObjectRepository)
    {
        _mapper = mapper;
        _administrativeUnitRepository = administrativeUnitRepository;
        _administrativeUnitAdapter = administrativeUnitAdapter;
        _unitObjectAdapter = unitObjectAdapter;
        _unitObjectRepository = unitObjectRepository;
        _unitObjectClassRepository = unitObjectClassRepository;
        _evaluationCriteriaTypeRepository = evaluationCriteriaTypeRepository;
        _evaluationCriteriaRepository = evaluationCriteriaRepository;
        _lifeIndexRepository = lifeIndexRepository;
    }

    public async Task<List<int>> DoComplexAssessment()
    {
        var units = await _administrativeUnitRepository.GetList(new UnitObjectAssessmentSpecification());

        if (units is null)
        {
            throw new Exception("There is no unit objects for assessment");
        }

        foreach (var objectsGroup in units)
        {
            var a = await DoUnitAssessment(objectsGroup);
        }
        

        return null;
    }

    public async Task<LifeIndex?> DoUnitAssessment(AdministrativeUnit unit)
    {
        if (!unit.Populations.Any(p => p.NumberOfPeople > 0))
        {
            return null;
        }

        var lifeIndex = new LifeIndex()
        {
            AdministrativeUnitId = unit.Id,
            ReceivingDate = DateTime.UtcNow,
            EvaluationCriterias = new List<EvaluationCriteria>(),
        };

        IEnumerable<EvaluationCriteriaType?> evalTypes = await _evaluationCriteriaTypeRepository.GetList(new EvaluationCriteriaTypeSpecification());
        var tasks = new List<Task<EvaluationCriteria>>();

        var medicineAssessment = DoMedicineAssessment(unit.UnitObjects.Where(p => p.UnitObjectType.UnitObjectClass.Name is "Health" or "EmergencyServices").ToList(),
            unit.Populations.Where(p => p.NumberOfPeople > 0).OrderBy(p => p.Date).Last(),
            evalTypes);
        tasks.Add(medicineAssessment);

        var criterias = await Task.WhenAll(tasks.ToArray());
        lifeIndex.EvaluationCriterias = criterias;

        var index = await _lifeIndexRepository.CreateAsync(lifeIndex);
        //LifeIndex index = null;
        
        return index;
    }

    public static async Task<EvaluationCriteria> DoMedicineAssessment(List<UnitObject> objects, Population population, IEnumerable<EvaluationCriteriaType?> evalTypes)
    {
        await Task.Delay(1);
        if (evalTypes is null) evalTypes = new List<EvaluationCriteriaType?>();
        var populationValue = population.NumberOfPeople;
        List<float> grades = new();

        var ambulanceStations = objects.Where(p => p.UnitObjectType.Name == "ambulance_station");
        var ambulanceGrade = CoverAssessment(populationValue, ambulanceStations.Count() * 12000);
        grades.Add(ambulanceGrade);

        var clinics = objects.Where(p => p.UnitObjectType.Name == "clinic");
        var clinicsGrade = CoverAssessment(populationValue, clinics.Count() * 50000);
        grades.Add(clinicsGrade);

        var hospitals = objects.Where(p => p.UnitObjectType.Name == "hospital");
        var hospitalsGrade = CoverAssessment(populationValue, hospitals.Count() * 500000);
        grades.Add(hospitalsGrade);

        var pharmacys = objects.Where(p => p.UnitObjectType.Name == "pharmacy");
        var pharmacysGrade = CoverAssessment(populationValue, pharmacys.Count() * 13000);
        grades.Add(pharmacysGrade);

        var result = grades.Average();

        EvaluationCriteriaType? evalType = evalTypes.FirstOrDefault(b => b.Name == "Medicine");
        bool evalTypeExists = true;
        if (evalType == null)
        {
            evalType = new();
            evalType.Name = "Medicine";
            evalTypeExists = false;
        }

        EvaluationCriteria evalCriteria = new()
        {
            Value = result,
            Description = $"Ambulance stations amount grade: {ambulanceGrade}\n" +
                          $"Clinics amount grade: {clinicsGrade}\n" +
                          $"Hospitals amount grade: {hospitalsGrade}\n" +
                          $"Pharmacy amount grade: {pharmacysGrade}",
        };

        if (evalTypeExists)
        {
            evalCriteria.EvaluationCriteriaTypeId = evalType.Id;
        }
        else
        {
            evalCriteria.EvaluationCriteriaType = evalType;
        }

        return evalCriteria;
    }

    public static float CoverAssessment(float main, float cover)
    {
        if (cover >= main)
        {
            return 10;
        }

        float ratio = 100 - 100 * cover / main;
        var a = 10 - ratio / 10;
        return 100 * cover / main / 10;
    }
}