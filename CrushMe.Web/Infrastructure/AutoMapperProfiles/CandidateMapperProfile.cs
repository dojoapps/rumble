using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CrushMe.Database.Models;
using CrushMe.Web.Models;

namespace CrushMe.Web.Infrastructure.AutoMapperProfiles
{
    public class CandidateMapperProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<UserFriend, CandidateViewModel>()
                .ForMember(x => x.FbId, o => o.MapFrom(m => m.FbId))
                .ForMember(x => x.Name, o => o.MapFrom(m => m.Name));
        }
    }
}