namespace FurnitureFactory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileUpload : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "User_Id", "dbo.Users");
            DropIndex("dbo.Files", new[] { "User_Id" });
            DropColumn("dbo.Files", "UserId");
            RenameColumn(table: "dbo.Files", name: "User_Id", newName: "UserId");
            DropPrimaryKey("dbo.Files");
            AddColumn("dbo.Files", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Files", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Files", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Files", "Id");
            CreateIndex("dbo.Files", "UserId");
            AddForeignKey("dbo.Files", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.Files", "FileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "FileName", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropIndex("dbo.Files", new[] { "UserId" });
            DropPrimaryKey("dbo.Files");
            AlterColumn("dbo.Files", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Files", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Files", "Id");
            AddPrimaryKey("dbo.Files", "FileName");
            RenameColumn(table: "dbo.Files", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Files", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Files", "User_Id");
            AddForeignKey("dbo.Files", "User_Id", "dbo.Users", "Id");
        }
    }
}
