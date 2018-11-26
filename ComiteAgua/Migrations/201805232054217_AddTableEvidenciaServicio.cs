namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableEvidenciaServicio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.EvidenciaServicios",
                c => new
                    {
                        EvidenciaServicioId = c.Int(nullable: false, identity: true),
                        ServicioId = c.Int(nullable: false),
                        UrlImagen = c.String(nullable: false, maxLength: 255),
                        Observaciones = c.String(maxLength: 500),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.EvidenciaServicioId)
                .ForeignKey("Comite.Servicios", t => t.ServicioId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.ServicioId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.EvidenciaServicios", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.EvidenciaServicios", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.EvidenciaServicios", "ServicioId", "Comite.Servicios");
            DropIndex("Comite.EvidenciaServicios", new[] { "UsuarioCambioId" });
            DropIndex("Comite.EvidenciaServicios", new[] { "UsuarioAltaId" });
            DropIndex("Comite.EvidenciaServicios", new[] { "ServicioId" });
            DropTable("Comite.EvidenciaServicios");
        }
    }
}
