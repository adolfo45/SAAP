namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableArchivosPersona : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.ArchivosPersona",
                c => new
                    {
                        ArchivoPersonaId = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        UrlArchivo = c.String(nullable: false, maxLength: 250),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.ArchivoPersonaId)
                .ForeignKey("Comite.Personas", t => t.PersonaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.PersonaId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.ArchivosPersona", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.ArchivosPersona", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.ArchivosPersona", "PersonaId", "Comite.Personas");
            DropIndex("Comite.ArchivosPersona", new[] { "UsuarioCambioId" });
            DropIndex("Comite.ArchivosPersona", new[] { "UsuarioAltaId" });
            DropIndex("Comite.ArchivosPersona", new[] { "PersonaId" });
            DropTable("Comite.ArchivosPersona");
        }
    }
}
