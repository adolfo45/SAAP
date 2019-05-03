namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoTablaCambioPropietario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.CambiosPropietario",
                c => new
                    {
                        CambioPropietarioId = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 1000),
                        ApellidoPaterno = c.String(maxLength: 100),
                        ApellidoMaterno = c.String(maxLength: 100),
                        FechaNacimiento = c.DateTime(),
                        EstadoCivilId = c.Int(),
                        Telefono = c.String(maxLength: 15),
                        CorreoElectronico = c.String(maxLength: 50),
                        Rfc = c.String(maxLength: 50),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.CambioPropietarioId)
                .ForeignKey("Global.EstadosCiviles", t => t.EstadoCivilId)
                .ForeignKey("Comite.PersonasFisicas", t => t.PersonaId)
                .Index(t => t.PersonaId)
                .Index(t => t.EstadoCivilId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.CambiosPropietario", "PersonaId", "Comite.PersonasFisicas");
            DropForeignKey("Comite.CambiosPropietario", "EstadoCivilId", "Global.EstadosCiviles");
            DropIndex("Comite.CambiosPropietario", new[] { "EstadoCivilId" });
            DropIndex("Comite.CambiosPropietario", new[] { "PersonaId" });
            DropTable("Comite.CambiosPropietario");
        }
    }
}
