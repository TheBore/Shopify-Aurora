namespace IT_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clothes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Brand = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        AvailablePieces = c.Boolean(nullable: false),
                        ShoppingCart_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_ID)
                .Index(t => t.ShoppingCart_ID);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Color = c.String(),
                        ClothesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clothes", t => t.ClothesId, cascadeDelete: true)
                .Index(t => t.ClothesId);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        dateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Size = c.String(),
                        ClothesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clothes", t => t.ClothesId, cascadeDelete: true)
                .Index(t => t.ClothesId);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClothesID = c.Int(nullable: false),
                        price = c.Int(nullable: false),
                        from = c.DateTime(nullable: false),
                        to = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clothes", t => t.ClothesID, cascadeDelete: true)
                .Index(t => t.ClothesID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        ShoppingCart_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.ShoppingCart_ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ShoppingCart_ID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Prices", "ClothesID", "dbo.Clothes");
            DropForeignKey("dbo.Sizes", "ClothesId", "dbo.Clothes");
            DropForeignKey("dbo.Clothes", "ShoppingCart_ID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.Colors", "ClothesId", "dbo.Clothes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "ShoppingCart_ID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Prices", new[] { "ClothesID" });
            DropIndex("dbo.Sizes", new[] { "ClothesId" });
            DropIndex("dbo.Colors", new[] { "ClothesId" });
            DropIndex("dbo.Clothes", new[] { "ShoppingCart_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Prices");
            DropTable("dbo.Sizes");
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.Colors");
            DropTable("dbo.Clothes");
        }
    }
}
