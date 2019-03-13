namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Detalles : DbMigration
    {
        public override void Up()
        {
            DropIndex("Comite.Constancias", new[] { "TomaId" });
            AlterColumn("Comite.Constancias", "TomaId", c => c.Int());
            CreateIndex("Comite.Constancias", "TomaId");
        }
        
        public override void Down()
        {
            DropIndex("Comite.Constancias", new[] { "TomaId" });
            AlterColumn("Comite.Constancias", "TomaId", c => c.Int(nullable: false));
            CreateIndex("Comite.Constancias", "TomaId");
        }
    }
}
