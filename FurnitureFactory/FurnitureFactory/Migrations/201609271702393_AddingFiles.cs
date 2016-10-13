namespace FurnitureFactory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileName = c.String(nullable: false, maxLength: 128),
                        FilePath = c.String(),
                        ContentType = c.String(),
                        FileType = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FileName)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "User_Id", "dbo.Users");
            DropIndex("dbo.Files", new[] { "User_Id" });
            DropTable("dbo.Files");
        }
    }
}
