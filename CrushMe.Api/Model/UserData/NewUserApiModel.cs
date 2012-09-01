using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CrushMe.Api.Model.UserData
{
    public class NewUserApiModel
    {
        public long FbId { get; set; }
        public string Name { get; set; }
    }
}