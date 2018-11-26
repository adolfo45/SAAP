namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTablePagos : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comite.Pagos", "CostoToma", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("Comite.Pagos", "CostoToma");
        }
    }
}
