using Scavdue.Core.Interfaces;
using Scavdue.Data;
using Scavdue.Data.Repositories;
using Scavdue.OpenStreetMaps.Adapter.Adapters;

namespace Scavdue.Extensions;

public static class AdapterProvider
{
    public static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        services.AddScoped<IAdministrativeUnitAdapter, AdministrativeUnitAdapter>();
        services.AddScoped<IUnitObjectAdapter, UnitObjectAdapter>();
        services.AddScoped<IDatabaseContext, ScavdueApiDbContext>();

        return services;
    }
}