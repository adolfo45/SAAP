namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionUsuario : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Comite.CambiosPropietario", "UsuarioAltaId");
            CreateIndex("Comite.CambiosPropietario", "UsuarioCambioId");
            AddForeignKey("Comite.CambiosPropietario", "UsuarioAltaId", "Seguridad.Usuarios", "UsuarioId");
            AddForeignKey("Comite.CambiosPropietario", "UsuarioCambioId", "Seguridad.Usuarios", "UsuarioId");
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.CambiosPropietario", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.CambiosPropietario", "UsuarioAltaId", "Seguridad.Usuarios");
            DropIndex("Comite.CambiosPropietario", new[] { "UsuarioCambioId" });
            DropIndex("Comite.CambiosPropietario", new[] { "UsuarioAltaId" });
        }
    }
}
