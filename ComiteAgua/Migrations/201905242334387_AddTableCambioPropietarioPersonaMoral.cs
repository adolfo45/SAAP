namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableCambioPropietarioPersonaMoral : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.CambiosPropietarioPersonaMoral",
                c => new
                    {
                        CambioPropietarioPersonaMoralId = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 255),
                        Rfc = c.String(maxLength: 50),
                        CorreoElectronico = c.String(maxLength: 50),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.CambioPropietarioPersonaMoralId)
                .ForeignKey("Comite.PersonasMorales", t => t.PersonaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.PersonaId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.CambiosPropietarioPersonaMoral", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.CambiosPropietarioPersonaMoral", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.CambiosPropietarioPersonaMoral", "PersonaId", "Comite.PersonasMorales");
            DropIndex("Comite.CambiosPropietarioPersonaMoral", new[] { "UsuarioCambioId" });
            DropIndex("Comite.CambiosPropietarioPersonaMoral", new[] { "UsuarioAltaId" });
            DropIndex("Comite.CambiosPropietarioPersonaMoral", new[] { "PersonaId" });
            DropTable("Comite.CambiosPropietarioPersonaMoral");
        }
    }
}
