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
        public DbSet<CrushCandidate> CrushCandidates { get; set; }
    }

    public class DbInitializer<T> : DropCreateDatabaseAlways<T>
        where T : CrushMeContext
    {
        protected override void Seed(T db)
        {
            AddOrUpdateUser(db, 100000193426007, "Felipe Amorim");
            AddOrUpdateUser(db, 734963830, "Vicente de Alencar");
            for (int i = 0; i < 11; i++)
            {
                AddOrUpdateUser(db, 100000 + i, "Test " + i);
            }

            base.Seed(db);
        }

        private void AddOrUpdateUser(CrushMeContext db, long fbId, string name)
        {
            if (db.Users.FirstOrDefault(x => x.Name == name && x.FbId == fbId) == null)
                db.Users.Add(new User()
                {
                    FbId = fbId,
                    Name = name
                });
        }
    }
}
