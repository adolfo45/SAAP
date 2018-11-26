namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelation : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Comite.Pagos", "DescuentoId");
            AddForeignKey("Comite.Pagos", "DescuentoId", "Comite.DescuentosProntoPago", "DescuentoId");
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.Pagos", "DescuentoId", "Comite.DescuentosProntoPago");
            DropIndex("Comite.Pagos", new[] { "DescuentoId" });
        }
    }
}
