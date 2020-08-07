namespace ltweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryAndNewsModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        CoverImage = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        RegionId = c.Int(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Regions", t => t.RegionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.RegionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NewsImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(nullable: false),
                        Image = c.String(nullable: false),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.News", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.NewsImages", "NewsId", "dbo.News");
            DropForeignKey("dbo.News", "CategoryId", "dbo.Categories");
            DropIndex("dbo.NewsImages", new[] { "NewsId" });
            DropIndex("dbo.News", new[] { "UserId" });
            DropIndex("dbo.News", new[] { "RegionId" });
            DropIndex("dbo.News", new[] { "CategoryId" });
            DropTable("dbo.Regions");
            DropTable("dbo.NewsImages");
            DropTable("dbo.News");
            DropTable("dbo.Categories");
        }
    }
}
