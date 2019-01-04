namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionConvenioRecibos : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comite.Recibos", "ConvenioId", c => c.Int());
            CreateIndex("Comite.Recibos", "ConvenioId");
            AddForeignKey("Comite.Recibos", "ConvenioId", "Comite.Convenios", "ConvenioId");
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.Recibos", "ConvenioId", "Comite.Convenios");
            DropIndex("Comite.Recibos", new[] { "ConvenioId" });
            DropColumn("Comite.Recibos", "ConvenioId");
        }
    }
}
