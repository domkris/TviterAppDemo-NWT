namespace TviterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTables2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bljaks",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ApplicationUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.ApplicationUserID });
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ApplicationUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.ApplicationUserID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Likes");
            DropTable("dbo.Bljaks");
        }
    }
}
