namespace FurnitureFactory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIndexBulstat : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Bulstat" });
            AlterColumn("dbo.Users", "Bulstat", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Bulstat", c => c.String(maxLength: 12));
            CreateIndex("dbo.Users", "Bulstat", unique: true);
        }
    }
}
