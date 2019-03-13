namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoTablaTiposConstancia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.TiposConstancia",
                c => new
                    {
                        TipoConstanciaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.TipoConstanciaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
            AddColumn("Comite.Constancias", "TipoConstanciaId", c => c.Int(nullable: false));
            CreateIndex("Comite.Constancias", "TipoConstanciaId");
            AddForeignKey("Comite.Constancias", "TipoConstanciaId", "Comite.TiposConstancia", "TipoConstanciaId");
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.TiposConstancia", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.TiposConstancia", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Constancias", "TipoConstanciaId", "Comite.TiposConstancia");
            DropIndex("Comite.TiposConstancia", new[] { "UsuarioCambioId" });
            DropIndex("Comite.TiposConstancia", new[] { "UsuarioAltaId" });
            DropIndex("Comite.Constancias", new[] { "TipoConstanciaId" });
            DropColumn("Comite.Constancias", "TipoConstanciaId");
            DropTable("Comite.TiposConstancia");
        }
    }
}
