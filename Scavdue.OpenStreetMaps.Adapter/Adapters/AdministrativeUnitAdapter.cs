﻿using Newtonsoft.Json;
using Scavdue.Core.Interfaces;
using Scavdue.Core.Models;
using Scavdue.OpenStreetMaps.Adapter.Constants;
using Scavdue.OpenStreetMaps.Adapter.Converters;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.NominantimModels;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Elements;
using Scavdue.OpenStreetMaps.Adapter.DeserializationModels.OverpassModels.Tags;

namespace Scavdue.OpenStreetMaps.Adapter.Adapters;

public class AdministrativeUnitAdapter : BaseAdapter, IAdministrativeUnitAdapter
{
    public async Task<AdministrativeUnit> GetCountry(string country)
    {
        string requestUrl = URLs.OVERPASS_API_URL + $"[out:json];rel[admin_level=2][\"name:ru\"=\"{country.Replace(" ", "+")}\"];out;";
        string response = await DoRequest(requestUrl);
        var rootobject = JsonConvert.DeserializeObject<Rootobject<UnitElement<CountryUnitTags>, CountryUnitTags>>(response.Replace("ISO3166-", "ISO3166"));
        if (rootobject.Elements.Length < 1) return null;

        var coordinates = await GetUnitCoordinates(rootobject.Elements[0].Tags.Name);
        rootobject.Elements[0].NominatimRoot = coordinates;

        var result = AdministrativeUnitsConverter.ConvertToAdministrativeUnit(rootobject.Elements[0]);
        return result;
    }

    public async Task<List<AdministrativeUnit>> GetChildUnits(int id, string parentName, int admin_level, int countryId, string iso)
    {
        Rootobject<UnitElement<ChildUnitTags>, ChildUnitTags> responseObject = new();

        if (admin_level < PropertyRestrictions.MIN_ADMIN_LEVEL || admin_level >= PropertyRestrictions.MAX_ADMIN_LEVEL)
        {
            return null;
        }

        bool isChildFinded = false;
        while (!isChildFinded && admin_level < PropertyRestrictions.MAX_ADMIN_LEVEL)
        {
            admin_level++;
            string requestUrl = URLs.OVERPASS_API_URL + $"[out:json];area[\"name:ru\"=\"{parentName.Replace(" ", "+")}\"];(rel[admin_level={admin_level}](area););out;";
            if (admin_level < 9) // только для Беларуси, там баг что при запросе возвращает часть Польши
            {
                requestUrl = URLs.OVERPASS_API_URL + $"[out:json];area[\"name:ru\"=\"{parentName.Replace(" ", "+")}\"];(rel[admin_level={admin_level}][\"addr:country\" = \"BY\"](area););out;";
            }
            string response = await DoRequest(requestUrl);
            var rootobject = JsonConvert.DeserializeObject<Rootobject<UnitElement<ChildUnitTags>, ChildUnitTags>>(response);
            if (rootobject.Elements.Length < 1) continue;
            else isChildFinded = true;
            responseObject = rootobject;
        }

        foreach (var item in responseObject.Elements)
        {
            var coordinates = await GetUnitCoordinates(item.Tags.Name);
            item.NominatimRoot = coordinates;
        }

        var result = AdministrativeUnitsConverter.ConvertToAdministrativeUnit(id, countryId, responseObject);

        return result;
    }

    private static async Task<Place> GetUnitCoordinates(string unitName)
    {
        string requestUrl = URLs.NOMINATIM_API_URL + $"q={unitName}&polygon_geojson=1&format=jsonv2";
        string response = await DoRequest(requestUrl);
        var rootobject = JsonConvert.DeserializeObject<List<Place>>(response);
        return rootobject[0];
    }
}