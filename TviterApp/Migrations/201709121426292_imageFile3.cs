namespace TviterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageFile3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "otherUserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "otherUserID");
        }
    }
}
