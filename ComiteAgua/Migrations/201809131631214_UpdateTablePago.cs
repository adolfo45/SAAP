namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTablePago : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comite.Pagos", "DescuentProntoPago", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("Comite.Pagos", "DescuentProntoPago");
        }
    }
}
