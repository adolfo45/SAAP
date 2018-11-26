namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationUsuarios : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Comite.Servicios", "UsuarioAltaId");
            CreateIndex("Comite.Servicios", "UsuarioCambioId");
            AddForeignKey("Comite.Servicios", "UsuarioAltaId", "Seguridad.Usuarios", "UsuarioId");
            AddForeignKey("Comite.Servicios", "UsuarioCambioId", "Seguridad.Usuarios", "UsuarioId");
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.Servicios", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Servicios", "UsuarioAltaId", "Seguridad.Usuarios");
            DropIndex("Comite.Servicios", new[] { "UsuarioCambioId" });
            DropIndex("Comite.Servicios", new[] { "UsuarioAltaId" });
        }
    }
}
