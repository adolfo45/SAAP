namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableUsuarios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Seguridad.Usuarios", "Activo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("Seguridad.Usuarios", "Activo", c => c.Boolean());
        }
    }
}
