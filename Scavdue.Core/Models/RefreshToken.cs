namespace Scavdue.Core.Models;

public partial class RefreshToken : Entity
{
    public int UserId { get; set; }

    public User User { get; set; }

    public string Token { get; set; }

    public DateTime Expires { get; set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;

    public DateTime Created { get; set; }
}
