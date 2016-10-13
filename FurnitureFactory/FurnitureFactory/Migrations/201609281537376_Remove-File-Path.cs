namespace FurnitureFactory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFilePath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Files", "ContentType", c => c.String(maxLength: 50));
            DropColumn("dbo.Files", "FilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "FilePath", c => c.String(maxLength: 255));
            AlterColumn("dbo.Files", "ContentType", c => c.String(maxLength: 30));
        }
    }
}
