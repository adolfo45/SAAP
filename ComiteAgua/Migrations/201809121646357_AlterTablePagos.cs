namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTablePagos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Comite.Pagos", "DescuentoId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("Comite.Pagos", "DescuentoId", c => c.Int(nullable: false));
        }
    }
}
