namespace FurnitureFactory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableChange : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "ApplicationUser_Id", newName: "Client_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_ApplicationUser_Id", newName: "IX_Client_Id");
            AddColumn("dbo.Orders", "ClientId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ApplicationUserId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "ClientId");
            RenameIndex(table: "dbo.Orders", name: "IX_Client_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Orders", name: "Client_Id", newName: "ApplicationUser_Id");
        }
    }
}
