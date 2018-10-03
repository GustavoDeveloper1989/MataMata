namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigatinLast3 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.TorneioModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TorneioModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Quantidade_times = c.Int(nullable: false),
                        Tipo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
