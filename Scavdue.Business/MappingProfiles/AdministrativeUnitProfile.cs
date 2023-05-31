using AutoMapper;
using Scavdue.Business.Models.Response;
using Scavdue.Core.Models;

namespace Scavdue.Business.MappingProfiles;

public class AdministrativeUnitProfile : Profile
{
    public AdministrativeUnitProfile()
    {
        CreateMap<AdministrativeUnit, UnitByNameResponseModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.ParentAdministrativeUnitName,
                act => act.MapFrom(src =>
                    src.ParentAdministrativeUnit == null ? string.Empty : src.ParentAdministrativeUnit.Name))
            .ForMember(dest => dest.CountryName, act => act.MapFrom(src => src.Country.Name))
            .ForMember(dest => dest.AdministrativeLevel, act => act.MapFrom(src => src.AdministrativeLevel));

        CreateMap<AdministrativeUnit, UnitWithCoordinatesResponseModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Polygons, act => act.MapFrom(src => src.AdministrativeUnitPolygons))
            .ForMember(dest => dest.CountryName, act => act.MapFrom(src => src.Country.Name))
            .ForMember(dest => dest.CountryId, act => act.MapFrom(src => src.Country.Id))
            .ForMember(dest => dest.ISO, act => act.MapFrom(src => src.Country.Iso3166))
            .ForMember(dest => dest.Place, act => act.MapFrom(src => src.Place ?? ""));

        CreateMap<AdministrativeUnit, UnitWithLifeIndexResponseModel>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.CountryName, act => act.MapFrom(src => src.Country.Name))
            .ForMember(dest => dest.ISO, act => act.MapFrom(src => src.Country.Iso3166))
            .ForMember(dest => dest.Place, act => act.MapFrom(src => src.Place ?? ""))
            .ForMember(dest => dest.LifeIndex, act => act.MapFrom(src => src.LifeIndexes));


        CreateMap<AdministrativeUnitPolygon, UnitPolygonResponseModel>();

    }
}