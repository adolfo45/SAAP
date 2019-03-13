namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TomaIdNullPagos : DbMigration
    {
        public override void Up()
        {
            DropIndex("Comite.Pagos", new[] { "TomaId" });
            AlterColumn("Comite.Pagos", "TomaId", c => c.Int());
            CreateIndex("Comite.Pagos", "TomaId");
        }
        
        public override void Down()
        {
            DropIndex("Comite.Pagos", new[] { "TomaId" });
            AlterColumn("Comite.Pagos", "TomaId", c => c.Int(nullable: false));
            CreateIndex("Comite.Pagos", "TomaId");
        }
    }
}
