namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PagoIdNull_Recibos : DbMigration
    {
        public override void Up()
        {
            DropIndex("Comite.Recibos", new[] { "PagoId" });
            AlterColumn("Comite.Recibos", "PagoId", c => c.Int());
            CreateIndex("Comite.Recibos", "PagoId");
        }
        
        public override void Down()
        {
            DropIndex("Comite.Recibos", new[] { "PagoId" });
            AlterColumn("Comite.Recibos", "PagoId", c => c.Int(nullable: false));
            CreateIndex("Comite.Recibos", "PagoId");
        }
    }
}
