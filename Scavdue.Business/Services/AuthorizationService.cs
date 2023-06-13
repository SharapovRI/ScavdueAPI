using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Scavdue.Business.Interfaces;
using Scavdue.Business.Models.Request;
using Scavdue.Business.Models.Response;
using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Scavdue.Business.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;

    public AuthorizationService(IConfiguration config, IMapper mapper, IUserRepository userService)
    {
        _config = config;
        _userRepository = userService;
    }

    public async Task<AuthenticateResponseModel> AuthenticateAsync(AuthenticateRequestModel model)
    {
        var user = await _userRepository.GetUserAsync(model.Username, ToSha256(model.Password));

        if (user == null) return null;

        var jwtToken = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken(user.Id.ToString());

        user.RefreshTokens.Add(refreshToken);
        await _userRepository.UpdateAsync(user);

        return new AuthenticateResponseModel(user, jwtToken, refreshToken.Token);
    }

    public async Task<AuthenticateResponseModel> RefreshTokenAsync(string token)
    {
        var user = await _userRepository.GetTokenAsync(token);

        if (user == null) return null;

        var refreshToken = user.RefreshTokens.FirstOrDefault();

        if (refreshToken is null || refreshToken.IsExpired) return null;

        var newRefreshToken = GenerateRefreshToken(user.Id.ToString());
        user.RefreshTokens.Add(newRefreshToken);
        await _userRepository.UpdateAsync(user);

        var jwtToken = GenerateJwtToken(user);

        return new AuthenticateResponseModel(user, jwtToken, newRefreshToken.Token);
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim> {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Login),
                new Claim("Role", user.Role.Name)
            };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private RefreshToken GenerateRefreshToken(string id)
    {
        using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
        {
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new RefreshToken()
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }
    }

    private string ToSha256(string text)
    {
        byte[] data = Encoding.Default.GetBytes(text);
        var result = new SHA256Managed().ComputeHash(data);
        return BitConverter.ToString(result).Replace("-", "").ToLower();
    }
}