namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDescuentoProntoPago : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.DescuentosProntoPago",
                c => new
                    {
                        DescuentoId = c.Int(nullable: false, identity: true),
                        MesAno = c.DateTime(nullable: false),
                        MontoPoncentaje = c.Int(nullable: false),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.DescuentoId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
            AddColumn("Comite.Pagos", "DescuentoId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.DescuentosProntoPago", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.DescuentosProntoPago", "UsuarioAltaId", "Seguridad.Usuarios");
            DropIndex("Comite.DescuentosProntoPago", new[] { "UsuarioCambioId" });
            DropIndex("Comite.DescuentosProntoPago", new[] { "UsuarioAltaId" });
            DropColumn("Comite.Pagos", "DescuentoId");
            DropTable("Comite.DescuentosProntoPago");
        }
    }
}
