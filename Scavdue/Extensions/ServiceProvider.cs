using Scavdue.Business.Interfaces;
using Scavdue.Business.Services;

namespace Scavdue.Extensions;

public static class ServiceProvider
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAdministrativeUnitService, AdministrativeUnitService>();
        services.AddScoped<IUnitObjectsService, UnitObjectsService>();

        return services;
    }
}