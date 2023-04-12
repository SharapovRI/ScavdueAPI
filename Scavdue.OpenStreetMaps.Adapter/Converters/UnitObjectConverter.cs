using System.Numerics;
using System.Resources;
using Scavdue.Core.Models;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Elements;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Tags;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.NominantimModels;

namespace Scavdue.OpenStreetMaps.Adapter.Converters;

public static class UnitObjectConverter
{
    public static UnitObject ConvertToUnitObject(BuildingElement<BuildingTags> building, UnitObjectClass buildingClass)
    {
        var types = buildingClass.UnitObjectTypes;
        var unitobjectType = types.FirstOrDefault(b =>
            b.Name == building.tags?.Building || b.Name == building.tags?.Amenity ||
            b.Name == building.tags?.Emergency);

        UnitObject unitObject = new();
        unitObject.Name = building.tags.Name;

        if (unitobjectType is null)
        {
            unitobjectType = new UnitObjectType()
            {
                Name = building.tags?.Building ?? building.tags?.Amenity ?? building.tags?.Emergency,
                UnitObjectClass = buildingClass
            };
            buildingClass.UnitObjectTypes.Add(unitobjectType);
        }
        unitObject.UnitObjectType = unitobjectType;

        unitObject.UnitObjectPolygons = GetPolygonsFromGeoJson(building, unitObject);

        return unitObject;
    }

    public static List<UnitObject> ConvertToUnitObjectList(Dictionary<string, Rootobject<BuildingElement<BuildingTags>, BuildingTags>> buildings, IEnumerable<UnitObjectClass> classes)
    {
        List<UnitObject> unitObjects = new();
        foreach (var objectClassKey in buildings)
        {
            UnitObjectClass objectClass = classes.FirstOrDefault(b => b.Name == objectClassKey.Key);
            if (objectClass == null)
            {
                objectClass = new();
                objectClass.Name = objectClassKey.Key;
            }

            foreach (var building in objectClassKey.Value.Elements)
            {
                unitObjects.Add(ConvertToUnitObject(building, objectClass));
            }
        }

        return unitObjects;
    }

    private static List<UnitObjectPolygon> GetPolygonsFromGeoJson(BuildingElement<BuildingTags> buildingElement, UnitObject unitObject)
    {
        try
        {
            switch (buildingElement.Type)
            {
                case "node":
                {
                    UnitObjectPolygon polygon = new()
                    {
                        CenterLat = buildingElement.Lat,
                        CenterLong = buildingElement.Lon,
                        UnitObject = unitObject,
                        Coordinates = $"[[{buildingElement.Lon}, {buildingElement.Lat}]]"
                    };
                    return new List<UnitObjectPolygon> { polygon };
                }
                case "way":
                {
                    UnitObjectPolygon polygon = new()
                    {
                        UnitObject = unitObject,
                        CenterLat = (buildingElement.Bounds.MaxLat + buildingElement.Bounds.MinLat) / 2,
                        CenterLong = (buildingElement.Bounds.MaxLon + buildingElement.Bounds.MinLon) / 2,
                        Coordinates = "[" +
                                      string.Join(",", buildingElement.Geometry.Select(b => $"[{b.Lon}, {b.Lat}]")) +
                                      "]"
                    };
                    return new List<UnitObjectPolygon> { polygon };
                }
                case "relation":
                {
                    List<UnitObjectPolygon> polygons = new();

                    foreach (var member in buildingElement.Polygons.Where(b => b.Geometry is not null))
                    {
                        UnitObjectPolygon polygon = new()
                        {
                            UnitObject = unitObject,
                            CenterLat = (buildingElement.Bounds.MaxLat + buildingElement.Bounds.MinLat) / 2,
                            CenterLong = (buildingElement.Bounds.MaxLon + buildingElement.Bounds.MinLon) / 2,
                            Coordinates = "[" + string.Join(",", member.Geometry.Select(b => $"[{b.Lon}, {b.Lat}]")) +
                                          "]"
                        };
                        polygons.Add(polygon);
                    }

                    return polygons;
                }
            }
        }
        catch (Exception ex)
        {
            var a = 0;
        }

        return new List<UnitObjectPolygon>();
    }
}