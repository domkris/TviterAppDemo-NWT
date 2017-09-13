namespace TviterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTables3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Bljaks");
            DropPrimaryKey("dbo.Likes");
            AlterColumn("dbo.Bljaks", "ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Likes", "ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Bljaks", new[] { "Id", "ApplicationUserID" });
            AddPrimaryKey("dbo.Likes", new[] { "Id", "ApplicationUserID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Likes");
            DropPrimaryKey("dbo.Bljaks");
            AlterColumn("dbo.Likes", "ApplicationUserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Bljaks", "ApplicationUserID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Likes", new[] { "Id", "ApplicationUserID" });
            AddPrimaryKey("dbo.Bljaks", new[] { "Id", "ApplicationUserID" });
        }
    }
}
