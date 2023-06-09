using Newtonsoft.Json;
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
            string requestUrl = URLs.OVERPASS_API_URL +
                                $"[out:json];area[\"name:ru\"=\"{parentName.Replace(" ", "+")}\"];(rel[admin_level={admin_level}](area););out;";
            if (admin_level < 9) // только для Беларуси, там баг что при запросе возвращает часть Польши
            {
                requestUrl = URLs.OVERPASS_API_URL +
                             $"[out:json];area[\"name:ru\"=\"{parentName.Replace(" ", "+")}\"];(rel[admin_level={admin_level}][\"addr:country\" = \"BY\"](area););out;";
                if (admin_level > 4)
                {
                    requestUrl = URLs.OVERPASS_API_URL +
                                 $"[out:json];area[\"name:ru\"=\"{parentName.Replace(" ", "+")}\"];(" +
                                 $"rel[admin_level={admin_level}][place=city][\"addr:country\" = \"BY\"](area)->.city;" +
                                 $"rel[admin_level={admin_level}][place=town][\"addr:country\" = \"BY\"](area)->.town;" +
                                 $");out;";
                }
            }//TODO переписать цикл, тк могут быть city 8 уровня, и др

            string response = await DoRequest(requestUrl);
            var rootobject =
                JsonConvert.DeserializeObject<Rootobject<UnitElement<ChildUnitTags>, ChildUnitTags>>(response);
            if (rootobject.Elements is null || rootobject.Elements.Length < 1) continue;
            else isChildFinded = true;
            responseObject = rootobject;
        }

        if (responseObject.Elements is null)
        {
            return new List<AdministrativeUnit>();
        }
        
        responseObject.Elements = responseObject.Elements?.Where(b => b.Tags?.Name != null).ToArray();

        foreach (var item in responseObject?.Elements)
        {
            var coordinates = await GetUnitCoordinates(item.Tags?.Name, iso);
            item.NominatimRoot = coordinates;
        }

        var result = AdministrativeUnitsConverter.ConvertToAdministrativeUnit(id, countryId, responseObject);

        return result;
    }

    public async Task<List<AdministrativeUnit>> GetCountryCities(int id, string countryName, int admin_level, int countryId, string iso)
    {
        if (admin_level < PropertyRestrictions.MIN_ADMIN_LEVEL || admin_level >= PropertyRestrictions.MAX_ADMIN_LEVEL)
        {
            return null;
        }

        Rootobject<UnitElement<ChildUnitTags>, ChildUnitTags> responseObject = new();

        if (admin_level < PropertyRestrictions.MAX_ADMIN_LEVEL)
        {
            string requestUrl = URLs.OVERPASS_API_URL +
                                $"[out:json];area[\"name:ru\"=\"{countryName.Replace(" ", "+")}\"];(" +
                                $"rel[place=city][\"addr:country\" = \"{iso}\"](area)->.city;" +
                                $"rel[place=town][\"addr:country\" = \"{iso}\"](area)->.town;" +
                                $");out;";
            

            string response = await DoRequest(requestUrl);
            var rootobject =
                JsonConvert.DeserializeObject<Rootobject<UnitElement<ChildUnitTags>, ChildUnitTags>>(response);
            responseObject = rootobject;
        }

        if (responseObject.Elements is null)
        {
            return new List<AdministrativeUnit>();
        }

        responseObject.Elements = responseObject.Elements?.Where(b => b.Tags?.Name != null && b.Tags.AdminLevel < 8).ToArray(); //TODO ограничение

        foreach (var item in responseObject?.Elements)
        {
            var coordinates = await GetUnitCoordinates(item.Tags?.Name, iso);
            item.NominatimRoot = coordinates;
        }

        var result = AdministrativeUnitsConverter.ConvertToAdministrativeUnit(id, countryId, responseObject);

        return result;
    }

    private static async Task<Place> GetUnitCoordinates(string unitName)
    {
        try
        {
            string requestUrl = URLs.NOMINATIM_API_URL + $"q={unitName}&polygon_geojson=1&format=jsonv2";
            string response = await DoRequest(requestUrl);
            var rootobject = JsonConvert.DeserializeObject<List<Place>>(response);
            return rootobject[0];
        }
        catch (Exception e)
        {
            return new Place();
        }
    }

    private static async Task<Place> GetUnitCoordinates(string unitName, string countryCode)
    {
        try
        {
            string requestUrl = URLs.NOMINATIM_API_URL + $"q={unitName}&countrycodes={countryCode}&polygon_geojson=1&format=jsonv2";
            string response = await DoRequest(requestUrl);
            var rootobject = JsonConvert.DeserializeObject<List<Place>>(response);
            return rootobject[0];
        }
        catch (Exception e)
        {
            return new Place();
        }
    }
}