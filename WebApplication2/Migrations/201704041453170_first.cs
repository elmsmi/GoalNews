namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Clients", "Tipo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Tipo", c => c.String(nullable: false));
        }
    }
}
