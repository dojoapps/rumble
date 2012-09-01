using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using CrushMe.Database.Models;

namespace CrushMe.Database
{
    public class CrushMeContext : DbContext
    {
        public CrushMeContext()
        {
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CrushMeContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Crush> Crushes { get; set; }
        public DbSet<CrushOption> CrushOptions { get; set; }
    }
}
