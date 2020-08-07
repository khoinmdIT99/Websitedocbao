namespace ltweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsubdecription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "SubDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "SubDescription");
        }
    }
}
