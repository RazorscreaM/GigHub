namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Participance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Participances",
                c => new
                    {
                        GigId = c.Int(nullable: false),
                        ParticipantId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GigId, t.ParticipantId })
                .ForeignKey("dbo.Gigs", t => t.GigId)
                .ForeignKey("dbo.AspNetUsers", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.GigId)
                .Index(t => t.ParticipantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participances", "ParticipantId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Participances", "GigId", "dbo.Gigs");
            DropIndex("dbo.Participances", new[] { "ParticipantId" });
            DropIndex("dbo.Participances", new[] { "GigId" });
            DropTable("dbo.Participances");
        }
    }
}
