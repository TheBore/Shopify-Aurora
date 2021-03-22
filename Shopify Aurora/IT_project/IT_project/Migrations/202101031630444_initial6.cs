namespace IT_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Colors", "Clothes_ID", "dbo.Clothes");
            DropForeignKey("dbo.Sizes", "Clothes_ID", "dbo.Clothes");
            DropIndex("dbo.Colors", new[] { "Clothes_ID" });
            DropIndex("dbo.Sizes", new[] { "Clothes_ID" });
            RenameColumn(table: "dbo.Colors", name: "Clothes_ID", newName: "ClothesId");
            RenameColumn(table: "dbo.Sizes", name: "Clothes_ID", newName: "ClothesId");
            AlterColumn("dbo.Colors", "ClothesId", c => c.Int(nullable: false));
            AlterColumn("dbo.Sizes", "ClothesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Colors", "ClothesId");
            CreateIndex("dbo.Sizes", "ClothesId");
            AddForeignKey("dbo.Colors", "ClothesId", "dbo.Clothes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Sizes", "ClothesId", "dbo.Clothes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sizes", "ClothesId", "dbo.Clothes");
            DropForeignKey("dbo.Colors", "ClothesId", "dbo.Clothes");
            DropIndex("dbo.Sizes", new[] { "ClothesId" });
            DropIndex("dbo.Colors", new[] { "ClothesId" });
            AlterColumn("dbo.Sizes", "ClothesId", c => c.Int());
            AlterColumn("dbo.Colors", "ClothesId", c => c.Int());
            RenameColumn(table: "dbo.Sizes", name: "ClothesId", newName: "Clothes_ID");
            RenameColumn(table: "dbo.Colors", name: "ClothesId", newName: "Clothes_ID");
            CreateIndex("dbo.Sizes", "Clothes_ID");
            CreateIndex("dbo.Colors", "Clothes_ID");
            AddForeignKey("dbo.Sizes", "Clothes_ID", "dbo.Clothes", "ID");
            AddForeignKey("dbo.Colors", "Clothes_ID", "dbo.Clothes", "ID");
        }
    }
}
