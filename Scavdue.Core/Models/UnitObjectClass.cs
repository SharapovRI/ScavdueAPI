namespace Scavdue.Core.Models
{
    public partial class UnitObjectClass : Entity
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<UnitObjectType> UnitObjectTypes { get; } = new List<UnitObjectType>();
    }
}
