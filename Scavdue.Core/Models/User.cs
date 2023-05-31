namespace Scavdue.Core.Models;

public partial class User : Entity
{
    public string Login { get; set; }

    public string Password { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; } = new List<RefreshToken>();
}
