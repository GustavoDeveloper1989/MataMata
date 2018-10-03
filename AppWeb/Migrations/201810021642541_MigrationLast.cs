namespace AppWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationLast : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.IniciarTorneioModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IniciarTorneioModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
