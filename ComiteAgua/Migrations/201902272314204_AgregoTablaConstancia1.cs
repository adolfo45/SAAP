namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoTablaConstancia1 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.ConstanciaId)
                .ForeignKey("Comite.CtoCalles", t => t.CalleId)
                .ForeignKey("Comite.CtoColonias", t => t.ColoniaId)
                .ForeignKey("Comite.CtoTipoCalles", t => t.TipoCalleId)
                .Index(t => t.TipoCalleId)
                .Index(t => t.CalleId)
                .Index(t => t.ColoniaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Constancias", "TipoCalleId", "Comite.CtoTipoCalles");
            DropForeignKey("dbo.Constancias", "ColoniaId", "Comite.CtoColonias");
            DropForeignKey("dbo.Constancias", "CalleId", "Comite.CtoCalles");
            DropIndex("dbo.Constancias", new[] { "ColoniaId" });
            DropIndex("dbo.Constancias", new[] { "CalleId" });
            DropIndex("dbo.Constancias", new[] { "TipoCalleId" });
            DropTable("dbo.Constancias");
        }
    }
}
