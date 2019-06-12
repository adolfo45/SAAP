namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldConcepto_Recibo : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comite.Recibos", "Concepto", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("Comite.Recibos", "Concepto");
        }
    }
}
