using Scavdue.Core.Models;

namespace Scavdue.Business.Models.Response;

public class UnitWithCoordinatesResponseModel
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = null!;

    public int AdministrativeLevel { get; set; }

    public string CountryName { get; set; } = string.Empty;

    public int CountryId { get; set; }

    public string Place { get; set; }

    public string ISO { get; set; } = string.Empty;

    public virtual ICollection<UnitPolygonResponseModel> Polygons { get; } = new List<UnitPolygonResponseModel>();
}