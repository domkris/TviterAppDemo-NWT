namespace TviterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class followtable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "otherUserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "otherUserID", c => c.String());
        }
    }
}
