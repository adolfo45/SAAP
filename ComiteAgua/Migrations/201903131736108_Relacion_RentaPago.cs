namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relacion_RentaPago : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comite.Pagos", "RentaId", c => c.Int());
            CreateIndex("Comite.Pagos", "RentaId");
            AddForeignKey("Comite.Pagos", "RentaId", "Comite.Rentas", "RentaId");
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.Pagos", "RentaId", "Comite.Rentas");
            DropIndex("Comite.Pagos", new[] { "RentaId" });
            DropColumn("Comite.Pagos", "RentaId");
        }
    }
}
