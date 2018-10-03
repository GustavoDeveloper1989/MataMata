namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration12 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ChaveTorneioModels");
        }
        
        public override void Down()
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
    }
}
