namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTipoArchivo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.TiposArchivo",
                c => new
                    {
                        TipoArchivoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.TipoArchivoId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
            AddColumn("Comite.ArchivosPersona", "TipoArchivoId", c => c.Int(nullable: false));
            CreateIndex("Comite.ArchivosPersona", "TipoArchivoId");
            AddForeignKey("Comite.ArchivosPersona", "TipoArchivoId", "Comite.TiposArchivo", "TipoArchivoId");
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.TiposArchivo", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.TiposArchivo", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.ArchivosPersona", "TipoArchivoId", "Comite.TiposArchivo");
            DropIndex("Comite.TiposArchivo", new[] { "UsuarioCambioId" });
            DropIndex("Comite.TiposArchivo", new[] { "UsuarioAltaId" });
            DropIndex("Comite.ArchivosPersona", new[] { "TipoArchivoId" });
            DropColumn("Comite.ArchivosPersona", "TipoArchivoId");
            DropTable("Comite.TiposArchivo");
        }
    }
}
