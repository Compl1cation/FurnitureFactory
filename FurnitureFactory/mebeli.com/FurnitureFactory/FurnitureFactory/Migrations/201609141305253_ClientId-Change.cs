namespace FurnitureFactory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientIdChange : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "ClientId" });
            DropColumn("dbo.Users", "ClientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ClientId", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Users", "ClientId", unique: true);
        }
    }
}
