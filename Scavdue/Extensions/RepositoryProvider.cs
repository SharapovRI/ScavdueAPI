using Scavdue.Core.Interfaces;
using Scavdue.Data.Repositories;

namespace Scavdue.Extensions;

public static class RepositoryProvider
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAdministrativeUnitPolygonRepository, AdministrativeUnitPolygonRepository>();
        services.AddScoped<IAdministrativeUnitRepository, AdministrativeUnitRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IPopulationRepository, PopulationRepository>();
        services.AddScoped<IUnitObjectClassRepository, UnitObjectClassRepository>();
        services.AddScoped<IUnitObjectPolygonRepository, UnitObjectPolygonRepository>();
        services.AddScoped<IUnitObjectRepository, UnitObjectRepository>();
        services.AddScoped<IUnitObjectTypeRepository, UnitObjectTypeRepository>();
        services.AddScoped<IEvaluationCriteriaTypeRepository, EvaluationCriteriaTypeRepository>();
        services.AddScoped<IEvaluationCriteriaRepository, EvaluationCriteriaRepository>();
        services.AddScoped<ILifeIndexRepository, LifeIndexRepository>();

        return services;
    }
}