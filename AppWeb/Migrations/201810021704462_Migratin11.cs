namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migratin11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChaveTorneioModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeUm = c.String(),
                        TimeDois = c.String(),
                        GolsUm = c.Int(nullable: false),
                        GolsDois = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChaveTorneioModels");
        }
    }
}
