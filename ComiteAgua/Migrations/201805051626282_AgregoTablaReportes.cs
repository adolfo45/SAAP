namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoTablaReportes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.Reportes",
                c => new
                    {
                        ReporteId = c.Int(nullable: false, identity: true),
                        TomaId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Observaciones = c.String(maxLength: 500),
                        UrlImagen = c.String(nullable: false, maxLength: 255),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.ReporteId)
                .ForeignKey("Comite.Tomas", t => t.TomaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.TomaId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.Reportes", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Reportes", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Reportes", "TomaId", "Comite.Tomas");
            DropIndex("Comite.Reportes", new[] { "UsuarioCambioId" });
            DropIndex("Comite.Reportes", new[] { "UsuarioAltaId" });
            DropIndex("Comite.Reportes", new[] { "TomaId" });
            DropTable("Comite.Reportes");
        }
    }
}
