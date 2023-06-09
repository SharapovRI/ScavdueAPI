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

        var index = 0;

        foreach (var objectsGroup in units)
        {
            try
            {
                index++;
                var a = await DoUnitAssessment(objectsGroup);
            }
            catch (Exception ex)
            {
                var a = 0;
            }
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
        if (evalTypes is null) evalTypes = new List<EvaluationCriteriaType?>();
        var tasks = new List<Task<EvaluationCriteria>>();

        EvaluationCriteriaType? medEvalType = evalTypes.FirstOrDefault(b => b.Name == "Medicine");
        var medicineAssessment = DoMedicineAssessment(unit.UnitObjects.Where(p => p.UnitObjectType.UnitObjectClass.Name is "Health" or "EmergencyServices").ToList(),
            unit.Populations.Where(p => p.NumberOfPeople > 0).OrderBy(p => p.Date).Last(),
            medEvalType);

        EvaluationCriteriaType? edEvalType = evalTypes.FirstOrDefault(b => b.Name == "Education");
        var educationAssessment = DoEducationAssessment(unit.UnitObjects.Where(p => p.UnitObjectType.UnitObjectClass.Name is "Education").ToList(),
            unit.Populations.Where(p => p.NumberOfPeople > 0).OrderBy(p => p.Date).Last(),
            edEvalType);

        tasks.Add(medicineAssessment);
        tasks.Add(educationAssessment);

        var criterias = await Task.WhenAll(tasks.ToArray());
        lifeIndex.EvaluationCriterias = criterias;

        var index = await _lifeIndexRepository.CreateAsync(lifeIndex);
        //LifeIndex index = null;
        
        return index;
    }

    public static async Task<EvaluationCriteria> DoEducationAssessment(List<UnitObject> objects, Population population, EvaluationCriteriaType? evalType)
    {
        await Task.Delay(1);
        var populationValue = population.NumberOfPeople;
        List<float> grades = new();

        var schools = objects.Where(p => p.UnitObjectType.Name == "school");
        var schoolsAvailabilityGrade = 0f;
        if (schools.Any())
        {
            schoolsAvailabilityGrade = (populationValue / 1000 * 112) / (schools.Count() * 2000) * 10;
            if (schoolsAvailabilityGrade > 10) schoolsAvailabilityGrade = 10;
        }
        grades.Add(schoolsAvailabilityGrade);

        var kindergartens = objects.Where(p => p.UnitObjectType.Name == "kindergarten");
        var kindergartensAvailabilityGrade = 0f;
        if (kindergartens.Any())
        {
            kindergartensAvailabilityGrade = (populationValue / 1000 * 55) / (kindergartens.Count() * 140) * 10;
            if (kindergartensAvailabilityGrade > 10) kindergartensAvailabilityGrade = 10;
        }
        grades.Add(kindergartensAvailabilityGrade);

        var libraries = objects.Where(p => p.UnitObjectType.Name == "library");
        var languageSchools = objects.Where(p => p.UnitObjectType.Name == "language_school");
        var musicSchools = objects.Where(p => p.UnitObjectType.Name == "music_school");
        var drivingSchools = objects.Where(p => p.UnitObjectType.Name == "driving_school");
        var additionalEducationSchoolsGrade = 0f;
        if (kindergartens.Any())
        {
            additionalEducationSchoolsGrade += 2.5f;
        }
        if(languageSchools.Any())
        {
            additionalEducationSchoolsGrade += 2.5f;
        }
        if (musicSchools.Any())
        {
            additionalEducationSchoolsGrade += 2.5f;
        }
        if (drivingSchools.Any())
        {
            additionalEducationSchoolsGrade += 2.5f;
        }
        grades.Add(additionalEducationSchoolsGrade);

        var nextStepEduSchools = objects.Where(p => p.UnitObjectType.Name == "university" || p.UnitObjectType.Name == "college");
        var nextStepEduSchoolsGrade = 0f;
        if (nextStepEduSchools.Any())
        {
            nextStepEduSchoolsGrade = (populationValue / 1000 * 55) / (nextStepEduSchools.Count() * 1100) * 10;
            if (nextStepEduSchoolsGrade > 10) nextStepEduSchoolsGrade = 10;
        }
        grades.Add(nextStepEduSchoolsGrade);

        var result = grades.Average();

        bool evalTypeExists = true;
        if (evalType == null)
        {
            evalType = new();
            evalType.Name = "Education";
            evalTypeExists = false;
        }

        EvaluationCriteria evalCriteria = new()
        {
            Value = result,
            Description = $"Оценка доступности детских садов: {kindergartensAvailabilityGrade}\n" +
                          $"Оценка доступности школ: {schoolsAvailabilityGrade}\n" +
                          $"Оценка доступности послешкольного образования: {nextStepEduSchoolsGrade}\n" +
                          $"Оценка доступности дополнительных источников образования (библиотеки, музыкальные и языковые школы, автошколы): {additionalEducationSchoolsGrade}",
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

    public static async Task<EvaluationCriteria> DoMedicineAssessment(List<UnitObject> objects, Population population, EvaluationCriteriaType? evalType)
    {
        await Task.Delay(1);
        var populationValue = population.NumberOfPeople;
        List<float> grades = new();

        var ambulanceStations = objects.Where(p => p.UnitObjectType.Name == "ambulance_station");
        var ambulanceGrade = 0f;
        if (ambulanceStations.Any())
        {
            ambulanceGrade = CoverAssessment(populationValue, ambulanceStations.Count() * 12000);
        }
        grades.Add(ambulanceGrade);

        var clinics = objects.Where(p => p.UnitObjectType.Name == "clinic");
        var clinicsGrade = 0f;
        if (clinics.Any())
        {
            clinicsGrade = CoverAssessment(populationValue, clinics.Count() * 50000);
        }
        grades.Add(clinicsGrade);

        var hospitals = objects.Where(p => p.UnitObjectType.Name == "hospital");
        var hospitalsGrade = 0f;
        if (hospitals.Any())
        {
            hospitalsGrade = CoverAssessment(populationValue, hospitals.Count() * 500000);
        }
        grades.Add(hospitalsGrade);

        var pharmacys = objects.Where(p => p.UnitObjectType.Name == "pharmacy");
        var pharmacysGrade = 0f;
        if (pharmacys.Any())
        {
            pharmacysGrade = CoverAssessment(populationValue, pharmacys.Count() * 13000);
        }
        grades.Add(pharmacysGrade);

        var result = grades.Average();

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
            Description = $"Оценка доступности станций скорой помощи: {ambulanceGrade}\n" +
                          $"Оценка доступности поликлиник: {clinicsGrade}\n" +
                          $"Оценка доступности больниц: {hospitalsGrade}\n" +
                          $"Оценка доступности аптек: {pharmacysGrade}",
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