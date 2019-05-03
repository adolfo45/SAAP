namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoCampoRenta : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comite.Rentas", "Observaciones", c => c.String(maxLength: 47));
        }
        
        public override void Down()
        {
            DropColumn("Comite.Rentas", "Observaciones");
        }
    }
}
