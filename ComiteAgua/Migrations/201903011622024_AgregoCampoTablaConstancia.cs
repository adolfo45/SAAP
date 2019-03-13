namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoCampoTablaConstancia : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comite.Constancias", "ReciboImpreso", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Comite.Constancias", "ReciboImpreso");
        }
    }
}
