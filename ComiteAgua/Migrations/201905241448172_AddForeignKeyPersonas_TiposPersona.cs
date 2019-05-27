namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPersonas_TiposPersona : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Comite.Personas", "TipoPersonaId");
            AddForeignKey("Comite.Personas", "TipoPersonaId", "Comite.TiposPersona", "TipoPersonaId");
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.Personas", "TipoPersonaId", "Comite.TiposPersona");
            DropIndex("Comite.Personas", new[] { "TipoPersonaId" });
        }
    }
}
