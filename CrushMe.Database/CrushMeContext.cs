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
            System.Data.Entity.Database.SetInitializer(new DbInitializer<CrushMeContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Crush> Crushes { get; set; }
        public DbSet<CrushOption> CrushOptions { get; set; }
    }

    public class DbInitializer<T> : DropCreateDatabaseAlways<T>
        where T : CrushMeContext
    {
        protected override void Seed(T db)
        {
            var testUserName = "TestUser";
            if (db.Users.FirstOrDefault(x => x.Name == testUserName) == null)
                db.Users.Add(new User()
                {
                    FbId = 100000193426007,
                    Name = "Felipe Amorim"
                });

            base.Seed(db);
        }
    }
}
