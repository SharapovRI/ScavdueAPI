using Microsoft.Extensions.DependencyInjection;
using Scavdue.Consts;

namespace Scavdue.Extensions;

public static class AuthorizationProvider
{
    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(APIPolicies.AdminPolicy, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("Role", "Admin");
            });
            opt.AddPolicy(APIPolicies.ModeratorPolicy, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("Role", "Moderator");
            });
        });

        return services;
    }
}
