namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoCampo_Revisado_y_Concluido : DbMigration
    {
        public override void Up()
        {
            AddColumn("Comite.Reportes", "Revisado", c => c.Boolean(nullable: false));
            AddColumn("Comite.Reportes", "Concluido", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Comite.Reportes", "Concluido");
            DropColumn("Comite.Reportes", "Revisado");
        }
    }
}
