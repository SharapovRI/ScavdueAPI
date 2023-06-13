using Scavdue.Core.Models;

namespace Scavdue.Business.Models.Response;

public class AuthenticateResponseModel
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }

    public AuthenticateResponseModel(User user, string jwtToken, string refreshToken)
    {
        Id = user.Id;
        Login = user.Login;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }
}
