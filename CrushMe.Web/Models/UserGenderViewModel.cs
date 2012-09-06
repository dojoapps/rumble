using CrushMe.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrushMe.Web.Models
{
    public class UserGenderViewModel
    {
        public UserGender Gender { get; set; }

        public UserGender Preference { get; set; }
    }
}