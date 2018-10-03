namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JogosModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Torneio = c.Int(nullable: false),
                        TimeUm = c.String(),
                        EscudoUm = c.String(),
                        TimeDois = c.String(),
                        EscudoDois = c.String(),
                        GolsUm = c.Int(nullable: false),
                        GolsDois = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JogosModels");
        }
    }
}
