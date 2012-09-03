namespace CrushMe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Crushes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CrusherId = c.Long(),
                        TargetId = c.Long(),
                        FatherCrushId = c.Int(),
                        DateCreated = c.DateTime(nullable: false),
                        DateReplied = c.DateTime(),
                        DateExpires = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Crushes", t => t.FatherCrushId)
                .ForeignKey("dbo.Users", t => t.CrusherId)
                .ForeignKey("dbo.Users", t => t.TargetId)
                .Index(t => t.FatherCrushId)
                .Index(t => t.CrusherId)
                .Index(t => t.TargetId);
            
            CreateTable(
                "dbo.CrushCandidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        CrushId = c.Int(nullable: false),
                        Selected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Crushes", t => t.CrushId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CrushId);
            
            CreateTable(
                "dbo.UserFriends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        Name = c.String(),
                        FbId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserFriends", new[] { "UserId" });
            DropIndex("dbo.CrushCandidates", new[] { "CrushId" });
            DropIndex("dbo.CrushCandidates", new[] { "UserId" });
            DropIndex("dbo.Crushes", new[] { "TargetId" });
            DropIndex("dbo.Crushes", new[] { "CrusherId" });
            DropIndex("dbo.Crushes", new[] { "FatherCrushId" });
            DropForeignKey("dbo.UserFriends", "UserId", "dbo.Users");
            DropForeignKey("dbo.CrushCandidates", "CrushId", "dbo.Crushes");
            DropForeignKey("dbo.CrushCandidates", "UserId", "dbo.Users");
            DropForeignKey("dbo.Crushes", "TargetId", "dbo.Users");
            DropForeignKey("dbo.Crushes", "CrusherId", "dbo.Users");
            DropForeignKey("dbo.Crushes", "FatherCrushId", "dbo.Crushes");
            DropTable("dbo.UserFriends");
            DropTable("dbo.CrushCandidates");
            DropTable("dbo.Crushes");
            DropTable("dbo.Users");
        }
    }
}
