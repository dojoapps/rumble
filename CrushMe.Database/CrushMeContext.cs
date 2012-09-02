using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using CrushMe.Database.Models;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace CrushMe.Database
{
    public class CrushMeContext : DbContext
    {
        public CrushMeContext() : base(GetConnectionString())
        {
            System.Data.Entity.Database.SetInitializer(new DbInitializer<CrushMeContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Crush> Crushes { get; set; }
        public DbSet<CrushCandidate> CrushCandidates { get; set; }
        public DbSet<UserFriend> Friends { get; set; }

        private static string GetConnectionString() {
            try
            {
                if (!HttpContext.Current.Request.IsLocal)
                {
                    var uriString = ConfigurationManager.AppSettings["SQLSERVER_URI"];
                    var uri = new Uri(uriString);
                    var connectionString = new SqlConnectionStringBuilder
                    {
                        DataSource = uri.Host,
                        InitialCatalog = uri.AbsolutePath.Trim('/'),
                        UserID = uri.UserInfo.Split(':').First(),
                        Password = uri.UserInfo.Split(':').Last(),
                    }.ConnectionString;

                    return connectionString;
                }
            } catch(Exception) {
                
            }

            return "name=SQLSERVER_CONNECTION_STRING";
        }
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
            if (db.Users.FirstOrDefault(x => x.Name == name && x.Id == fbId) == null)
                db.Users.Add(new User()
                {
                    Id = fbId,
                    Name = name
                });
        }
    }
}
