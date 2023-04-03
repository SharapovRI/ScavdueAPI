using Scavdue.Core.Models;

namespace Scavdue.Business.Models.Response;

public class UnitPolygonResponseModel
{
    public float CenterLong { get; set; }

    public float CenterLat { get; set; }

    public string Coordinates { get; set; } = string.Empty;
}