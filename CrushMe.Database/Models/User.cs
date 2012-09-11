using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Facebook;

namespace CrushMe.Database.Models
{
    public enum UserGender {
        Unknown,
        Male,
        Female
    }

    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public UserGender? Gender { get; set; }

        public UserGender? GenderPreference { get; set; }

        public 
    }
}
