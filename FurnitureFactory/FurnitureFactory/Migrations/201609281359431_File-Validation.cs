namespace FurnitureFactory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileValidation : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Files");
            AlterColumn("dbo.Files", "Id", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Files", "FilePath", c => c.String(maxLength: 255));
            AlterColumn("dbo.Files", "ContentType", c => c.String(maxLength: 30));
            AddPrimaryKey("dbo.Files", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Files");
            AlterColumn("dbo.Files", "ContentType", c => c.String());
            AlterColumn("dbo.Files", "FilePath", c => c.String());
            AlterColumn("dbo.Files", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Files", "Id");
        }
    }
}
