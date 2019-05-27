namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTiposPersona : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.TiposPersona",
                c => new
                    {
                        TipoPersonaId = c.Int(nullable: false, identity: true),
                        TipoPersona = c.String(nullable: false, maxLength: 50),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.TipoPersonaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
            AddColumn("Comite.Personas", "TipoPersonaId", c => c.Int(nullable: false));
            DropColumn("Comite.PersonasMorales", "PaginaWeb");
        }
        
        public override void Down()
        {
            AddColumn("Comite.PersonasMorales", "PaginaWeb", c => c.String(maxLength: 150));
            DropForeignKey("Comite.TiposPersona", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.TiposPersona", "UsuarioAltaId", "Seguridad.Usuarios");
            DropIndex("Comite.TiposPersona", new[] { "UsuarioCambioId" });
            DropIndex("Comite.TiposPersona", new[] { "UsuarioAltaId" });
            DropColumn("Comite.Personas", "TipoPersonaId");
            DropTable("Comite.TiposPersona");
        }
    }
}
