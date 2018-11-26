namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableTomas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Comite.Tomas", "Folio", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("Comite.Tomas", "Folio", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
