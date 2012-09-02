using CrushMe.Database.Models;
using CrushMe.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrushMe.Web.Infrastructure.AutoMapperProfiles
{
    public class CrushReceivedMapperProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<Crush, CrushReceivedViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.PrettyDateSent, o => o.MapFrom(m => m.DateCreated.ToShortDateString()))
                .ForMember(x => x.Candidates,
                        o => o.MapFrom(
                            m => m.Candidates
                                    .Select(x => new CrushCandidateViewModel()
                                    {
                                        FbId = x.UserId,
                                        Selected = x.Selected,
                                        Name = x.User.Name
                                    })));

        }
    }
}