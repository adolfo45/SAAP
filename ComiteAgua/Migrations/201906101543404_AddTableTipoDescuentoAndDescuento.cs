namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTipoDescuentoAndDescuento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.Descuentos",
                c => new
                    {
                        DescuentoVariosId = c.Int(nullable: false, identity: true),
                        TipoDescuentoId = c.Int(nullable: false),
                        Porcentaje = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.DescuentoVariosId)
                .ForeignKey("Comite.TiposDescuento", t => t.TipoDescuentoId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.TipoDescuentoId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
            CreateTable(
                "Comite.TiposDescuento",
                c => new
                    {
                        TipoDescuentoId = c.Int(nullable: false, identity: true),
                        TipoDescuento = c.String(nullable: false, maxLength: 150),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.TipoDescuentoId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
            AddColumn("Comite.Pagos", "DescuentoVariosId", c => c.Int());
            CreateIndex("Comite.Pagos", "DescuentoVariosId");
            AddForeignKey("Comite.Pagos", "DescuentoVariosId", "Comite.Descuentos", "DescuentoVariosId");
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.Descuentos", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Descuentos", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Descuentos", "TipoDescuentoId", "Comite.TiposDescuento");
            DropForeignKey("Comite.TiposDescuento", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.TiposDescuento", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Pagos", "DescuentoVariosId", "Comite.Descuentos");
            DropIndex("Comite.TiposDescuento", new[] { "UsuarioCambioId" });
            DropIndex("Comite.TiposDescuento", new[] { "UsuarioAltaId" });
            DropIndex("Comite.Descuentos", new[] { "UsuarioCambioId" });
            DropIndex("Comite.Descuentos", new[] { "UsuarioAltaId" });
            DropIndex("Comite.Descuentos", new[] { "TipoDescuentoId" });
            DropIndex("Comite.Pagos", new[] { "DescuentoVariosId" });
            DropColumn("Comite.Pagos", "DescuentoVariosId");
            DropTable("Comite.TiposDescuento");
            DropTable("Comite.Descuentos");
        }
    }
}
