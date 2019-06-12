namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDescuentosPago : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Comite.Pagos", "DescuentoVariosId", "Comite.Descuentos");
            DropIndex("Comite.Pagos", new[] { "DescuentoVariosId" });
            CreateTable(
                "Comite.DescuentosPagos",
                c => new
                    {
                        DescuentoPagoId = c.Int(nullable: false, identity: true),
                        DescuentoVariosId = c.Int(nullable: false),
                        PagoId = c.Int(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.DescuentoPagoId)
                .ForeignKey("Comite.Descuentos", t => t.DescuentoVariosId)
                .ForeignKey("Comite.Pagos", t => t.PagoId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.DescuentoVariosId)
                .Index(t => t.PagoId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
            DropColumn("Comite.Pagos", "DescuentoVariosId");
        }
        
        public override void Down()
        {
            AddColumn("Comite.Pagos", "DescuentoVariosId", c => c.Int());
            DropForeignKey("Comite.DescuentosPagos", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.DescuentosPagos", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.DescuentosPagos", "PagoId", "Comite.Pagos");
            DropForeignKey("Comite.DescuentosPagos", "DescuentoVariosId", "Comite.Descuentos");
            DropIndex("Comite.DescuentosPagos", new[] { "UsuarioCambioId" });
            DropIndex("Comite.DescuentosPagos", new[] { "UsuarioAltaId" });
            DropIndex("Comite.DescuentosPagos", new[] { "PagoId" });
            DropIndex("Comite.DescuentosPagos", new[] { "DescuentoVariosId" });
            DropTable("Comite.DescuentosPagos");
            CreateIndex("Comite.Pagos", "DescuentoVariosId");
            AddForeignKey("Comite.Pagos", "DescuentoVariosId", "Comite.Descuentos", "DescuentoVariosId");
        }
    }
}
