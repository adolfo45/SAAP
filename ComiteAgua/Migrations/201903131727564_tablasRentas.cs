namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablasRentas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.Rentas",
                c => new
                    {
                        RentaId = c.Int(nullable: false, identity: true),
                        TipoRentaId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        ApellidoPaterno = c.String(nullable: false, maxLength: 50),
                        ApellidoMaterno = c.String(maxLength: 50),
                        Calle = c.String(nullable: false, maxLength: 50),
                        Colonia = c.String(nullable: false, maxLength: 50),
                        Municipio = c.String(nullable: false, maxLength: 50),
                        NoInt = c.String(maxLength: 10),
                        NoExt = c.String(maxLength: 10),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.RentaId)
                .ForeignKey("Comite.TiposRenta", t => t.TipoRentaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.TipoRentaId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
            CreateTable(
                "Comite.TiposRenta",
                c => new
                    {
                        TipoRentaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        FechaAlta = c.DateTime(nullable: false),
                        UsuarioAltaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(),
                        UsuarioCambioId = c.Int(),
                    })
                .PrimaryKey(t => t.TipoRentaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioAltaId)
                .ForeignKey("Seguridad.Usuarios", t => t.UsuarioCambioId)
                .Index(t => t.UsuarioAltaId)
                .Index(t => t.UsuarioCambioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.Rentas", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Rentas", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.TiposRenta", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.TiposRenta", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Rentas", "TipoRentaId", "Comite.TiposRenta");
            DropIndex("Comite.TiposRenta", new[] { "UsuarioCambioId" });
            DropIndex("Comite.TiposRenta", new[] { "UsuarioAltaId" });
            DropIndex("Comite.Rentas", new[] { "UsuarioCambioId" });
            DropIndex("Comite.Rentas", new[] { "UsuarioAltaId" });
            DropIndex("Comite.Rentas", new[] { "TipoRentaId" });
            DropTable("Comite.TiposRenta");
            DropTable("Comite.Rentas");
        }
    }
}
