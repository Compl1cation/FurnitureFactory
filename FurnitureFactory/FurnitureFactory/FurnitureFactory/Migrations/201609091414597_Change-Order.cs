namespace FurnitureFactory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ClientId", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Users", "ClientId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "ClientId" });
            DropColumn("dbo.Users", "ClientId");
        }
    }
}
