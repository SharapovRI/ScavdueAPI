namespace Scavdue.Core.Models
{
    public partial class UnitObject : Entity
    {
        public string Name { get; set; } = null!;

        public int AdministrativeUnitId { get; set; }

        public virtual AdministrativeUnit AdministrativeUnit { get; set; } = null!;

        public int UnitObjectTypeId { get; set; }

        public virtual UnitObjectType UnitObjectType { get; set; } = null!;

        public virtual ICollection<UnitObjectPolygon> UnitObjectPolygons { get; } = new List<UnitObjectPolygon>();
    }
}
