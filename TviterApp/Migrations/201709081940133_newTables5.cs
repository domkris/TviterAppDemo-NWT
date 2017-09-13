namespace TviterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTables5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Bljaks");
            DropPrimaryKey("dbo.Likes");
            AlterColumn("dbo.Bljaks", "ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Likes", "ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Bljaks", new[] { "PostId", "ApplicationUserID" });
            AddPrimaryKey("dbo.Likes", new[] { "PostId", "ApplicationUserID" });
            DropColumn("dbo.Bljaks", "Id");
            DropColumn("dbo.Likes", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Likes", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Bljaks", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Likes");
            DropPrimaryKey("dbo.Bljaks");
            AlterColumn("dbo.Likes", "ApplicationUserID", c => c.String());
            AlterColumn("dbo.Bljaks", "ApplicationUserID", c => c.String());
            AddPrimaryKey("dbo.Likes", "Id");
            AddPrimaryKey("dbo.Bljaks", "Id");
        }
    }
}
