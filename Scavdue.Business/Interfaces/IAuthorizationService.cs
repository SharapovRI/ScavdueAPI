using Scavdue.Business.Models.Request;
using Scavdue.Business.Models.Response;

namespace Scavdue.Business.Interfaces;

public interface IAuthorizationService
{
    Task<AuthenticateResponseModel> AuthenticateAsync(AuthenticateRequestModel model);

    Task<AuthenticateResponseModel> RefreshTokenAsync(string token);
}
