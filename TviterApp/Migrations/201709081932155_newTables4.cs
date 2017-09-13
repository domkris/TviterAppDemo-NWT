namespace TviterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTables4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Bljaks");
            DropPrimaryKey("dbo.Likes");
            AddColumn("dbo.Bljaks", "PostId", c => c.Int(nullable: false));
            AddColumn("dbo.Likes", "PostId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bljaks", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Bljaks", "ApplicationUserID", c => c.String());
            AlterColumn("dbo.Likes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Likes", "ApplicationUserID", c => c.String());
            AddPrimaryKey("dbo.Bljaks", "Id");
            AddPrimaryKey("dbo.Likes", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Likes");
            DropPrimaryKey("dbo.Bljaks");
            AlterColumn("dbo.Likes", "ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Likes", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Bljaks", "ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Bljaks", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Likes", "PostId");
            DropColumn("dbo.Bljaks", "PostId");
            AddPrimaryKey("dbo.Likes", new[] { "Id", "ApplicationUserID" });
            AddPrimaryKey("dbo.Bljaks", new[] { "Id", "ApplicationUserID" });
        }
    }
}
