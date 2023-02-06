namespace HealthyBoost.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityTypes",
                c => new
                    {
                        ActivityTypeID = c.Int(nullable: false, identity: true),
                        ActivityName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ActivityTypeID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        KiloGoal = c.Double(nullable: false),
                        ActivityTypeID = c.Int(nullable: false),
                        State = c.Boolean(nullable: false),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.ActivityTypes", t => t.ActivityTypeID, cascadeDelete: true)
                .Index(t => t.ActivityTypeID);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        FoodID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Per100Cal = c.Int(nullable: false),
                        Grams = c.Int(nullable: false),
                        State = c.Boolean(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        PortionSize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FoodID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.MealDetails",
                c => new
                    {
                        MealDetailID = c.Int(nullable: false, identity: true),
                        MealID = c.Int(),
                        FoodID = c.Int(nullable: false),
                        Calory = c.Int(nullable: false),
                        Grams = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MealDetailID)
                .ForeignKey("dbo.Foods", t => t.FoodID, cascadeDelete: true)
                .ForeignKey("dbo.Meals", t => t.MealID)
                .Index(t => t.MealID)
                .Index(t => t.FoodID);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        MealID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        MealTypeID = c.Int(nullable: false),
                        RegisterationDate = c.DateTime(nullable: false),
                        State = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MealID)
                .ForeignKey("dbo.MealTypes", t => t.MealTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.MealTypeID);
            
            CreateTable(
                "dbo.MealTypes",
                c => new
                    {
                        MealTypeID = c.Int(nullable: false, identity: true),
                        MealTypeName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.MealTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Foods", "UserID", "dbo.Users");
            DropForeignKey("dbo.Meals", "UserID", "dbo.Users");
            DropForeignKey("dbo.Meals", "MealTypeID", "dbo.MealTypes");
            DropForeignKey("dbo.MealDetails", "MealID", "dbo.Meals");
            DropForeignKey("dbo.MealDetails", "FoodID", "dbo.Foods");
            DropForeignKey("dbo.Foods", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Users", "ActivityTypeID", "dbo.ActivityTypes");
            DropIndex("dbo.Meals", new[] { "MealTypeID" });
            DropIndex("dbo.Meals", new[] { "UserID" });
            DropIndex("dbo.MealDetails", new[] { "FoodID" });
            DropIndex("dbo.MealDetails", new[] { "MealID" });
            DropIndex("dbo.Foods", new[] { "UserID" });
            DropIndex("dbo.Foods", new[] { "CategoryID" });
            DropIndex("dbo.Users", new[] { "ActivityTypeID" });
            DropTable("dbo.MealTypes");
            DropTable("dbo.Meals");
            DropTable("dbo.MealDetails");
            DropTable("dbo.Categories");
            DropTable("dbo.Foods");
            DropTable("dbo.Users");
            DropTable("dbo.ActivityTypes");
        }
    }
}
