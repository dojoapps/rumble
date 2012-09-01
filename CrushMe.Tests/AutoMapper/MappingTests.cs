using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using CrushMe.Api.App_Start;
using Xunit;

namespace CrushMe.Tests.AutoMapper
{
    public class MappingTests
    {
        public MappingTests()
        {
            Mapper.Reset(); 
        }

        [Fact]
        public void CrushMappings()
        {
            AutoMapperConfig.CrushMappings();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
