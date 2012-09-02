using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrushMe.Web.Infrastructure.AutoMapperProfiles;

namespace CrushMe.Web
{
    public class AutoMapperConfiguration
    {
        public static void RegisterMappings()
        {
            Mapper.AddProfile<CrushesListMapperProfile>();
        }
    }
}