namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminoTablaConstancia : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Constancias", "CalleId", "Comite.CtoCalles");
            DropForeignKey("dbo.Constancias", "ColoniaId", "Comite.CtoColonias");
            DropForeignKey("dbo.Constancias", "TipoCalleId", "Comite.CtoTipoCalles");
            DropIndex("dbo.Constancias", new[] { "TipoCalleId" });
            DropIndex("dbo.Constancias", new[] { "CalleId" });
            DropIndex("dbo.Constancias", new[] { "ColoniaId" });
            DropTable("dbo.Constancias");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Constancias",
                c => new
                    {
                        ConstanciaId = c.Int(nullable: false, identity: true),
                        Propietario = c.String(),
                        TipoCalleId = c.Int(nullable: false),
                        CalleId = c.Int(nullable: false),
                        ColoniaId = c.Int(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.ConstanciaId);
            
            CreateIndex("dbo.Constancias", "ColoniaId");
            CreateIndex("dbo.Constancias", "CalleId");
            CreateIndex("dbo.Constancias", "TipoCalleId");
            AddForeignKey("dbo.Constancias", "TipoCalleId", "Comite.CtoTipoCalles", "TipoCalleId");
            AddForeignKey("dbo.Constancias", "ColoniaId", "Comite.CtoColonias", "ColoniaId");
            AddForeignKey("dbo.Constancias", "CalleId", "Comite.CtoCalles", "CalleId");
        }
    }
}
