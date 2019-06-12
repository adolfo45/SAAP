namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertiesDescuento : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comite.Pagos", "DescuentoMadreSoltera", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("Comite.Pagos", "DescuentoTerceraEdad", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("Comite.Pagos", "DescuentoTerceraEdad");
            DropColumn("Comite.Pagos", "DescuentoMadreSoltera");
        }
    }
}
