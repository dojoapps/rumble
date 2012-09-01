using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CrushMe.Api.Models.Crushes;
using CrushMe.Database.Models;

namespace CrushMe.Api.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            CrushMappings();
        }

        public static void CrushMappings()
        {
            Mapper.CreateMap<CrushOption, CrushOptionApiModel>()
                .ForMember(x => x.FbId, o => o.MapFrom(co => co.UserId))
                .ForMember(x => x.Name, o => o.MapFrom(co => co.User.Name));

            Mapper.CreateMap<Crush, CrushReceivedApiModel>()
                .ForMember(x => x.CrushDate, o => o.MapFrom(c => c.DateCreated))
                .ForMember(x => x.CrusherFbId, o => o.MapFrom(c => c.CrusherId))
                .ForMember(x => x.Options, o => o.MapFrom(c => c.Options))
                .ForMember(x => x.TargetFbId, o => o.MapFrom(c => c.TargetId));
        }
    }
}