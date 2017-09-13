namespace TviterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class followtable4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ApplicationUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ApplicationUserName");
        }
    }
}
