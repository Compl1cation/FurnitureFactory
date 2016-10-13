namespace FurnitureFactory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "Client_Id" });
            DropColumn("dbo.Orders", "ClientId");
            RenameColumn(table: "dbo.Orders", name: "Client_Id", newName: "ClientId");
            AlterColumn("dbo.Orders", "ClientId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Orders", "ClientId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Orders", "ClientId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "ClientId" });
            AlterColumn("dbo.Orders", "ClientId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Orders", "ClientId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "ClientId", newName: "Client_Id");
            AddColumn("dbo.Orders", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "Client_Id");
        }
    }
}
