using Scavdue.Core.Models;

namespace Scavdue.Business.Models.Response;

public class UnitWithCoordinatesResponseModel
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = null!;

    public int AdministrativeLevel { get; set; }

    public virtual ICollection<UnitPolygonResponseModel> Polygons { get; } = new List<UnitPolygonResponseModel>();
}