namespace ltweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "News_Id", c => c.Int());
            AddColumn("dbo.Regions", "News_Id", c => c.Int());
            CreateIndex("dbo.Categories", "News_Id");
            CreateIndex("dbo.Regions", "News_Id");
            AddForeignKey("dbo.Categories", "News_Id", "dbo.News", "Id");
            AddForeignKey("dbo.Regions", "News_Id", "dbo.News", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Regions", "News_Id", "dbo.News");
            DropForeignKey("dbo.Categories", "News_Id", "dbo.News");
            DropIndex("dbo.Regions", new[] { "News_Id" });
            DropIndex("dbo.Categories", new[] { "News_Id" });
            DropColumn("dbo.Regions", "News_Id");
            DropColumn("dbo.Categories", "News_Id");
        }
    }
}
