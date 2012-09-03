using CrushMe.Database.Models;
using CrushMe.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrushMe.Web.Helpers;

namespace CrushMe.Web.Infrastructure.AutoMapperProfiles
{
    public class CrushesListMapperProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<Crush, CrushReceivedViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.DateSent, o => o.MapFrom(m => m.DateCreated.ToShortDateString()))
                .ForMember(x => x.Status, o => o.MapFrom(m => m.Status))
                .ForMember(x => x.Candidates,
                        o => o.MapFrom(
                            m => m.Candidates
                                    .Select(x => new CrushCandidateViewModel()
                                    {
                                        FbId = x.UserId,
                                        Selected = x.Selected,
                                        Name = x.User.Name
                                    }).ToList().Shuffle()));

            CreateMap<Crush, CrushSentViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.TargetName, o => o.MapFrom(m => m.Target.Name))
                .ForMember(x => x.Status, o => o.MapFrom(m => m.Status))
                .ForMember(x => x.TargetId, o => o.MapFrom(m => m.TargetId))
                .ForMember(x => x.DateSent, o => o.MapFrom(m => m.DateCreated));

        }
    }
}