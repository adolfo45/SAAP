namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncrementoLongitudTablaRecibos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Comite.Recibos", "RenglonAdicional1", c => c.String(maxLength: 250));
            AlterColumn("Comite.Recibos", "RenglonAdicional2", c => c.String(maxLength: 250));
            AlterColumn("Comite.Recibos", "RenglonAdicional3", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("Comite.Recibos", "RenglonAdicional3", c => c.String(maxLength: 50));
            AlterColumn("Comite.Recibos", "RenglonAdicional2", c => c.String(maxLength: 50));
            AlterColumn("Comite.Recibos", "RenglonAdicional1", c => c.String(maxLength: 50));
        }
    }
}
