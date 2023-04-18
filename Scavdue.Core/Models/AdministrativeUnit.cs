namespace Scavdue.Core.Models;

public partial class AdministrativeUnit : Entity
{
    public string Name { get; set; } = null!;

    public int AdministrativeLevel { get; set; }

    public string? Place { get; set; }

    public int? ParentAdministrativeUnitId { get; set; }

    public virtual AdministrativeUnit? ParentAdministrativeUnit { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<UnitObject> UnitObjects { get; } = new List<UnitObject>();

    public virtual ICollection<AdministrativeUnitPolygon> AdministrativeUnitPolygons { get; set; } = new List<AdministrativeUnitPolygon>();

    public virtual ICollection<Population> Populations { get; } = new List<Population>();

    public virtual ICollection<AdministrativeUnit> ChildAdministrativeUnits { get; set; } = new List<AdministrativeUnit>();
}
