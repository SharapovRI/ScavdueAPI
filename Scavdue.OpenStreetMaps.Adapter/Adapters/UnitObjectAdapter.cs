using Newtonsoft.Json;
using Scavdue.OpenStreetMaps.Adapter.Constants;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Elements;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Tags;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels;
using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;
using Scavdue.OpenStreetMaps.Adapter.Converters;

namespace Scavdue.OpenStreetMaps.Adapter.Adapters;

public class UnitObjectAdapter : BaseAdapter, IUnitObjectAdapter
{
    public async Task<List<UnitObject>> GetUnitObjects(string unitName, int adminLevel, List<UnitObjectClass> classes)
    {
        Dictionary<string, Rootobject<BuildingElement<BuildingTags>, BuildingTags>> buildings = new();
        foreach (var buildingClass in TagDictionaries.BuildingClasses)
        {
            string requestUrl = URLs.OVERPASS_API_URL + $"[out:json];area[admin_level={adminLevel}][\"name:ru\"=\"{unitName}\"];(";
            foreach (var typeClass in buildingClass.Value)
            {
                requestUrl += $"nwr[{typeClass.Value}={typeClass.Key}](area) -> .{typeClass.Key};";
            }
            requestUrl += ");out geom;";
            string response = await DoRequest(requestUrl);
            var rootobject = JsonConvert.DeserializeObject<Rootobject<BuildingElement<BuildingTags>, BuildingTags>>(response);
            buildings.Add(buildingClass.Key, rootobject);
        }
        
        return UnitObjectConverter.ConvertToUnitObjectList(buildings, classes);
    }
}