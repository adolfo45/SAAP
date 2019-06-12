namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DecimalToIntTableDescuentos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Comite.Descuentos", "Porcentaje", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("Comite.Descuentos", "Porcentaje", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
