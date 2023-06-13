using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(string login, string password)
        : base(p => p.Login == login && p.Password == password)
    {
        AddInclude(p => p.Role);
        AddInclude(p => p.RefreshTokens);
    }

    public UserSpecification(string login)
        : base(p => p.Login == login)
    {
        AddInclude(p => p.Role);
        AddInclude(p => p.RefreshTokens);
    }
}
