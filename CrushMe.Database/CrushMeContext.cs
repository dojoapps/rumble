using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using CrushMe.Database.Models;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using CrushMe.Database.Migrations;

namespace CrushMe.Database
{
    public class CrushMeContext : DbContext
    {
        public CrushMeContext() : base(GetConnectionString())
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<CrushMeContext, CrushMe.Database.Migrations.Configuration>());
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
}
