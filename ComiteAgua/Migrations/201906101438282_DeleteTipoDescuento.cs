namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTipoDescuento : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Comite.Descuentos", "ModoDescuentoId", "Comite.ModoDescuento");
            DropIndex("Comite.Descuentos", new[] { "ModoDescuentoId" });
            DropTable("Comite.Descuentos");
            DropTable("Comite.ModoDescuento");
        }
        
        public override void Down()
        {
            CreateTable(
                "Comite.ModoDescuento",
                c => new
                    {
                        ModoDescuentoId = c.Int(nullable: false, identity: true),
                        ModoDescuento = c.String(nullable: false, maxLength: 100),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.ModoDescuentoId);
            
            CreateTable(
                "Comite.Descuentos",
                c => new
                    {
                        DescuentoId = c.Int(nullable: false, identity: true),
                        Descuento = c.String(nullable: false, maxLength: 100),
                        ModoDescuentoId = c.Int(nullable: false),
                        PeriodoDescuentoInicio = c.DateTime(storeType: "date"),
                        PeriodoDescuentoFin = c.DateTime(storeType: "date"),
                        MesAnoPago = c.DateTime(storeType: "date"),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Activo = c.Boolean(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.DescuentoId);
            
            CreateIndex("Comite.Descuentos", "ModoDescuentoId");
            AddForeignKey("Comite.Descuentos", "ModoDescuentoId", "Comite.ModoDescuento", "ModoDescuentoId");
        }
    }
}
