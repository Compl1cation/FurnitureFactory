namespace FurnitureFactory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MapTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Furniture", newName: "Furniture");
            AlterColumn("dbo.Furniture", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Furniture", "PricePerUnit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "TotalPrice", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            AlterColumn("dbo.Furniture", "PricePerUnit", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            AlterColumn("dbo.Furniture", "Weight", c => c.Decimal(nullable: false, precision: 12, scale: 2));
            RenameTable(name: "dbo.Furniture", newName: "Furnitures");
        }
    }
}
