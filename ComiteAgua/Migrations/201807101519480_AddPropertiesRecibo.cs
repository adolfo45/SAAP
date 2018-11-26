namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertiesRecibo : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comite.Recibos", "RenglonAdicional1", c => c.String(maxLength: 50));
            AddColumn("Comite.Recibos", "RenglonAdicional2", c => c.String(maxLength: 50));
            AddColumn("Comite.Recibos", "RenglonAdicional3", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("Comite.Recibos", "RenglonAdicional3");
            DropColumn("Comite.Recibos", "RenglonAdicional2");
            DropColumn("Comite.Recibos", "RenglonAdicional1");
        }
    }
}
