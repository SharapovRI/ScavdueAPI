using AutoMapper;
using Scavdue.Business.Models.Response;
using Scavdue.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Business.MappingProfiles;

internal class EvaluationCriteriaProfile : Profile
{
    public EvaluationCriteriaProfile()
    {
        CreateMap<EvaluationCriteria, EvaluationCriteriaResponseModel>()
            .ForMember(dest => dest.Value, act => act.MapFrom(src => src.Value))
            .ForMember(dest => dest.EvaluationCriteriaTypeName, act => act.MapFrom(src => src.EvaluationCriteriaType != null ? src.EvaluationCriteriaType.Name : ""))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description));
    }
}