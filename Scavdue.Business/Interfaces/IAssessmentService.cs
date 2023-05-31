namespace Scavdue.Business.Interfaces;

public interface IAssessmentService
{
    Task<List<int>> DoComplexAssessment();
}