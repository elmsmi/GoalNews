namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Cliente = c.String(nullable: false),
                        Tipo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmpClis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDEmp = c.Int(nullable: false),
                        IDCli = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => new { t.IDEmp, t.IDCli }, unique: true, name: "IX_FirstAndSecond");
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Empleado = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Noticia = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Cliente = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Peticion = c.String(nullable: false),
                        Empleado = c.String(nullable: false),
                        Resuelta = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.EmpClis", "IX_FirstAndSecond");
            DropTable("dbo.Requests");
            DropTable("dbo.News");
            DropTable("dbo.Employees");
            DropTable("dbo.EmpClis");
            DropTable("dbo.Clients");
        }
    }
}
