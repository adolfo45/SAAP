namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaronCorrecciones : DbMigration
    {
        public override void Up()
        {
            DropIndex("Comite.Constancias", new[] { "TipoCalleId" });
            DropIndex("Comite.Constancias", new[] { "CalleId" });
            DropIndex("Comite.Constancias", new[] { "ColoniaId" });
            AddColumn("Comite.Constancias", "TomaId", c => c.Int(nullable: false));
            AddColumn("Comite.Constancias", "FechaLetra", c => c.String(maxLength: 100));
            AlterColumn("Comite.Constancias", "Propietario", c => c.String(maxLength: 100));
            AlterColumn("Comite.Constancias", "TipoCalleId", c => c.Int());
            AlterColumn("Comite.Constancias", "CalleId", c => c.Int());
            AlterColumn("Comite.Constancias", "ColoniaId", c => c.Int());
            CreateIndex("Comite.Constancias", "TomaId");
            CreateIndex("Comite.Constancias", "TipoCalleId");
            CreateIndex("Comite.Constancias", "CalleId");
            CreateIndex("Comite.Constancias", "ColoniaId");
            AddForeignKey("Comite.Constancias", "TomaId", "Comite.Tomas", "TomaId");
            DropColumn("Comite.Constancias", "Fecha");
            DropColumn("Comite.Constancias", "Total");
        }
        
        public override void Down()
        {
            AddColumn("Comite.Constancias", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Comite.Constancias", "Fecha", c => c.String());
            DropForeignKey("Comite.Constancias", "TomaId", "Comite.Tomas");
            DropIndex("Comite.Constancias", new[] { "ColoniaId" });
            DropIndex("Comite.Constancias", new[] { "CalleId" });
            DropIndex("Comite.Constancias", new[] { "TipoCalleId" });
            DropIndex("Comite.Constancias", new[] { "TomaId" });
            AlterColumn("Comite.Constancias", "ColoniaId", c => c.Int(nullable: false));
            AlterColumn("Comite.Constancias", "CalleId", c => c.Int(nullable: false));
            AlterColumn("Comite.Constancias", "TipoCalleId", c => c.Int(nullable: false));
            AlterColumn("Comite.Constancias", "Propietario", c => c.String(nullable: false, maxLength: 100));
            DropColumn("Comite.Constancias", "FechaLetra");
            DropColumn("Comite.Constancias", "TomaId");
            CreateIndex("Comite.Constancias", "ColoniaId");
            CreateIndex("Comite.Constancias", "CalleId");
            CreateIndex("Comite.Constancias", "TipoCalleId");
        }
    }
}
