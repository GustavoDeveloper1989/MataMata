namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CampeoesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Torneio = c.Int(nullable: false),
                        Campeao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CampeoesModels");
        }
    }
}
