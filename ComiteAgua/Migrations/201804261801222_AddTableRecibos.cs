namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableRecibos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.Recibos",
                c => new
                    {
                        ReciboId = c.Int(nullable: false, identity: true),
                        PagoId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        CodigoQRurl = c.String(maxLength: 500),
                        NoRecibo = c.Int(nullable: false),
                        Observaciones = c.String(maxLength: 255),
                        Adicional = c.String(maxLength: 255),
                        CantidadLetra = c.String(maxLength: 255),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.ReciboId)
                .ForeignKey("Comite.Pagos", t => t.PagoId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.PagoId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.Recibos", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Recibos", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Recibos", "PagoId", "Comite.Pagos");
            DropIndex("Comite.Recibos", new[] { "UsuarioCambioId" });
            DropIndex("Comite.Recibos", new[] { "UsuarioAltaId" });
            DropIndex("Comite.Recibos", new[] { "PagoId" });
            DropTable("Comite.Recibos");
        }
    }
}
