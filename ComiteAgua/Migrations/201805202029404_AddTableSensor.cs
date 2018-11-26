namespace ComiteAgua.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableSensor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Comite.Sensores",
                c => new
                    {
                        LecturaSensorId = c.Int(nullable: false, identity: true),
                        Valor = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.LecturaSensorId);
            
        }
        
        public override void Down()
        {
            DropTable("Comite.Sensores");
        }
    }
}
