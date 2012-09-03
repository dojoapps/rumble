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
            System.Data.Entity.Database.SetInitializer(new ValidateDatabase<CrushMeContext>());
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

    public class ValidateDatabase<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
            if (!context.Database.Exists())
            {
                throw new Exception("Database does not exist. Try using Update-Database.");
            }
            else
            {
                if (!context.Database.CompatibleWithModel(true))
                {
                    throw new InvalidOperationException("The database is not compatible with the entity model. Try using Update-Database.");
                }
            }
        }
    }
}
