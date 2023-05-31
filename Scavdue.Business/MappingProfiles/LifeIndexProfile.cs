using AutoMapper;
using Scavdue.Business.Models.Response;
using Scavdue.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Business.MappingProfiles;

public class LifeIndexProfile : Profile
{
    public LifeIndexProfile()
    {
        CreateMap<ICollection<LifeIndex>, LifeIndexResponseModel>()
            .ForMember(dest => dest.EvaluationCriterias, act => act.MapFrom(src => src.OrderBy(p => p.ReceivingDate).LastOrDefault()));
    }
}