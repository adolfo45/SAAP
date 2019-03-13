namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaronRelaciones : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Constancias", newSchema: "Comite");
            AddColumn("Comite.Pagos", "ConstanciaId", c => c.Int());
            AddColumn("Comite.Constancias", "NoInt", c => c.String(maxLength: 10));
            AddColumn("Comite.Constancias", "NoExt", c => c.String(maxLength: 10));
            AddColumn("Comite.Constancias", "Fecha", c => c.String());
            AddColumn("Comite.Constancias", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("Comite.Constancias", "Propietario", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("Comite.Pagos", "ConstanciaId");
            CreateIndex("Comite.Constancias", "UsuarioAltaId");
            CreateIndex("Comite.Constancias", "UsuarioCambioId");
            AddForeignKey("Comite.Pagos", "ConstanciaId", "Comite.Constancias", "ConstanciaId");
            AddForeignKey("Comite.Constancias", "UsuarioAltaId", "Seguridad.Usuarios", "UsuarioId");
            AddForeignKey("Comite.Constancias", "UsuarioCambioId", "Seguridad.Usuarios", "UsuarioId");
        }
        
        public override void Down()
        {
            DropForeignKey("Comite.Constancias", "UsuarioCambioId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Constancias", "UsuarioAltaId", "Seguridad.Usuarios");
            DropForeignKey("Comite.Pagos", "ConstanciaId", "Comite.Constancias");
            DropIndex("Comite.Constancias", new[] { "UsuarioCambioId" });
            DropIndex("Comite.Constancias", new[] { "UsuarioAltaId" });
            DropIndex("Comite.Pagos", new[] { "ConstanciaId" });
            AlterColumn("Comite.Constancias", "Propietario", c => c.String());
            DropColumn("Comite.Constancias", "Total");
            DropColumn("Comite.Constancias", "Fecha");
            DropColumn("Comite.Constancias", "NoExt");
            DropColumn("Comite.Constancias", "NoInt");
            DropColumn("Comite.Pagos", "ConstanciaId");
            MoveTable(name: "Comite.Constancias", newSchema: "dbo");
        }
    }
}
