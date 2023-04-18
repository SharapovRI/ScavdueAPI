using Scavdue.Core.Models;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Elements;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Tags;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.NominantimModels;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels;

namespace Scavdue.OpenStreetMaps.Adapter.Converters;

public static class AdministrativeUnitsConverter
{
    public static AdministrativeUnit ConvertToAdministrativeUnit(UnitElement<CountryUnitTags> element)
    {
        var tags = element?.Tags;
        var coordinates = element?.NominatimRoot;

        AdministrativeUnit unit = new()
        {
            AdministrativeLevel = tags.AdminLevel,
            Name = tags.Name,
            Country = new Country
            {
                Name = tags.Name,
                Iso3166 = tags.Iso31661
            }
        };

        Population population = new()
        {
            AdministrativeUnit = unit,
            NumberOfPeople = tags.Population,
            Date = DateOnly.FromDateTime(DateTime.UtcNow.Date)
        };

        unit.Populations.Add(population);
        unit.AdministrativeUnitPolygons = GetPolygonsFromGeoJson(coordinates, unit);

        return unit;
    }

    public static AdministrativeUnit ConvertToAdministrativeUnit(int parentId, int countryId, UnitElement<ChildUnitTags> element)
    {
        var tags = element?.Tags;
        var coordinates = element?.NominatimRoot;

        AdministrativeUnit unit = new()
        {
            AdministrativeLevel = tags.AdminLevel,
            Name = tags.Name,
            CountryId = countryId,
            ParentAdministrativeUnitId = parentId,
            Place = tags.Place
        };

        Population population = new()
        {
            AdministrativeUnit = unit,
            NumberOfPeople = tags.Population is null ? 0 : Convert.ToInt32(tags.Population.Replace(" ", "")),
            Date = DateOnly.FromDateTime(DateTime.UtcNow.Date)
        };

        unit.Populations.Add(population);
        unit.AdministrativeUnitPolygons = GetPolygonsFromGeoJson(coordinates, unit) ?? new List<AdministrativeUnitPolygon>();

        return unit;
    }

    public static List<AdministrativeUnit> ConvertToAdministrativeUnit(int parentId, int countryId, Rootobject<UnitElement<ChildUnitTags>, ChildUnitTags> element)
    {
        return element.Elements.Select(unit => ConvertToAdministrativeUnit(parentId, countryId, unit)).ToList();
    }

    private static List<AdministrativeUnitPolygon> GetPolygonsFromGeoJson(Place place, AdministrativeUnit unit)
    {
        return (place?.Geojson?.Coordinates)?.Select(coordinates =>
            new AdministrativeUnitPolygon()
            {
                AdministrativeUnit = unit,
                CenterLat = Convert.ToSingle(place?.Lat?.Replace(".", ",")),
                CenterLong = Convert.ToSingle(place?.Lon?.Replace(".", ",")),
                Coordinates = coordinates?.ToString().Replace("\r", "").Replace("\n", "").Replace(" ", "")
            }).ToList();
    }
}