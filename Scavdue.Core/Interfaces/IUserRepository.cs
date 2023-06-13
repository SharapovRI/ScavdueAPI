using Scavdue.Core.Models;

namespace Scavdue.Core.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetUserAsync(string login, string password);

    Task<bool> GetUserAsync(string login);

    Task<User> GetTokenAsync(string token);
}
