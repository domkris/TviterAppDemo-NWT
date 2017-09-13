namespace TviterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class followtable2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        CurrentUserId = c.String(nullable: false, maxLength: 128),
                        OtherUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CurrentUserId, t.OtherUserId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Follows");
        }
    }
}
