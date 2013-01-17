namespace TLStreamApp3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TLStreams",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        type = c.String(),
                        status = c.String(),
                        owner = c.String(),
                        featured = c.String(),
                        race = c.String(),
                        viewers = c.Int(nullable: false),
                        rating = c.String(),
                        channelId = c.String(),
                        channelType = c.String(),
                        channelTitle = c.String(),
                        channelLink = c.String(),
                        threadLink = c.String(),
                        Self = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TLStreams");
        }
    }
}
