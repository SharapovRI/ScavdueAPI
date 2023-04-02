using Scavdue.Core.Interfaces.Models;

namespace Scavdue.Core.Models;

public partial class AdministrativeUnit : Entity
{
    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<UnitObject> UnitObjects { get; } = new List<UnitObject>();

    public virtual ICollection<AdministrativeUnitPolygon> AdministrativeUnitPolygons { get; } = new List<AdministrativeUnitPolygon>();

    public virtual ICollection<Population> Populations { get; } = new List<Population>();
}
