namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTokenFirebaseTableUsuarios : DbMigration
    {
        public override void Up()
        {
            AddColumn("Seguridad.Usuarios", "TokenFirebase", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("Seguridad.Usuarios", "TokenFirebase");
        }
    }
}
