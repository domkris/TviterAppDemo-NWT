namespace TviterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageFile2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "imagePath", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserPhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserPhoto", c => c.Binary());
            DropColumn("dbo.AspNetUsers", "imagePath");
        }
    }
}
