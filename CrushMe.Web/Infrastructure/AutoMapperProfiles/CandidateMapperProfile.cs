using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CrushMe.Core.Models;
using CrushMe.Web.Models;
using CrushMe.Core.Helpers;

namespace CrushMe.Web.Infrastructure.AutoMapperProfiles
{
    public class CandidateMapperProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, CandidateViewModel>()
                .ForMember(x => x.FbId, o => o.MapFrom(m => m.Id.ToLongId()))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name));
        }
    }
}