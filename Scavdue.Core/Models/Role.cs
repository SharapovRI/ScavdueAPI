namespace Scavdue.Core.Models;

public partial class Role : Entity
{
    public string Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
