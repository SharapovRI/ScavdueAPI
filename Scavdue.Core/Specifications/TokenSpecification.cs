using Scavdue.Core.Models;

namespace Scavdue.Core.Specifications;

public class TokenSpecification : BaseSpecification<User>
{
    public TokenSpecification(string token)
        : base(p => p.RefreshTokens.OrderBy(p => p.Created).LastOrDefault().Token == token)
    {
        AddInclude(p => p.Role);
        AddInclude(p => p.RefreshTokens);
    }
}
