namespace Scavdue.Business.Models.Response;

public class UnitByNameResponseModel
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int AdministrativeLevel { get; set; } = 0;

    public string ParentAdministrativeUnitName { get; set; } = null!;

    public string CountryName { get; set; } = string.Empty;
}