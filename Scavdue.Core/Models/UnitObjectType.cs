namespace Scavdue.Core.Models
{
    public partial class UnitObjectType : Entity
    {
        public string Name { get; set; } = null!;

        public int UnitObjectClassId { get; set; }

        public virtual UnitObjectClass UnitObjectClass { get; set; } = null!;

        public virtual ICollection<UnitObject> UnitObjects { get; } = new List<UnitObject>();
    }
}
