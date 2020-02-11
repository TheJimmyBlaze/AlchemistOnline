namespace AlchemistOnline.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountEmails",
                c => new
                    {
                        AccountEmailID = c.Int(nullable: false),
                        EmailAddress = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.AccountEmailID)
                .ForeignKey("dbo.Accounts", t => t.AccountEmailID)
                .Index(t => t.AccountEmailID)
                .Index(t => t.EmailAddress, unique: true);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 16),
                        AccountCreationDate = c.DateTime(nullable: false),
                        LastOnline = c.DateTime(nullable: false),
                        Key_AccountKeyID = c.Int(),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.AccountKeys", t => t.Key_AccountKeyID)
                .Index(t => t.DisplayName, unique: true)
                .Index(t => t.Key_AccountKeyID);
            
            CreateTable(
                "dbo.Explorers",
                c => new
                    {
                        ExplorerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ExperiencePoints = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Expedition_ExpeditionID = c.Int(),
                        Type_ExplorerTypeID = c.Int(),
                        Account_AccountID = c.Int(),
                    })
                .PrimaryKey(t => t.ExplorerID)
                .ForeignKey("dbo.Expeditions", t => t.Expedition_ExpeditionID)
                .ForeignKey("dbo.ExplorerTypes", t => t.Type_ExplorerTypeID)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountID)
                .Index(t => t.Expedition_ExpeditionID)
                .Index(t => t.Type_ExplorerTypeID)
                .Index(t => t.Account_AccountID);
            
            CreateTable(
                "dbo.Expeditions",
                c => new
                    {
                        ExpeditionID = c.Int(nullable: false, identity: true),
                        DepartureTime = c.DateTime(nullable: false),
                        ExpectedReturnTime = c.DateTime(nullable: false),
                        Environment_EnvironmentID = c.Int(),
                    })
                .PrimaryKey(t => t.ExpeditionID)
                .ForeignKey("dbo.Environments", t => t.Environment_EnvironmentID)
                .Index(t => t.Environment_EnvironmentID);
            
            CreateTable(
                "dbo.Environments",
                c => new
                    {
                        EnvironmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImagePath = c.String(),
                        ExpeditionSeconds = c.Int(nullable: false),
                        Difficulty_EnvironmentDifficultyID = c.Int(),
                        Type_EnvironmentTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.EnvironmentID)
                .ForeignKey("dbo.EnvironmentDifficulties", t => t.Difficulty_EnvironmentDifficultyID)
                .ForeignKey("dbo.EnvironmentTypes", t => t.Type_EnvironmentTypeID)
                .Index(t => t.Difficulty_EnvironmentDifficultyID)
                .Index(t => t.Type_EnvironmentTypeID);
            
            CreateTable(
                "dbo.EnvironmentDifficulties",
                c => new
                    {
                        EnvironmentDifficultyID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ColourHex = c.String(),
                        ImagePath = c.String(),
                        SkillRequirement = c.Int(nullable: false),
                        RewardMultiplier = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnvironmentDifficultyID);
            
            CreateTable(
                "dbo.EnvironmentTypes",
                c => new
                    {
                        EnvironmentTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ColourHex = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.EnvironmentTypeID);
            
            CreateTable(
                "dbo.ExplorerTypes",
                c => new
                    {
                        ExplorerTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ExplorerTypeID);
            
            CreateTable(
                "dbo.IngredientAmounts",
                c => new
                    {
                        IngredientAmountID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Ingredient_IngredientID = c.Int(),
                        Account_AccountID = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientAmountID)
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_IngredientID)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountID)
                .Index(t => t.Ingredient_IngredientID)
                .Index(t => t.Account_AccountID);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImagePath = c.String(),
                        EnvironmentType_EnvironmentTypeID = c.Int(),
                        Type_IngredientTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientID)
                .ForeignKey("dbo.EnvironmentTypes", t => t.EnvironmentType_EnvironmentTypeID)
                .ForeignKey("dbo.IngredientTypes", t => t.Type_IngredientTypeID)
                .Index(t => t.EnvironmentType_EnvironmentTypeID)
                .Index(t => t.Type_IngredientTypeID);
            
            CreateTable(
                "dbo.IngredientTypes",
                c => new
                    {
                        IngredientTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ColourHex = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.IngredientTypeID);
            
            CreateTable(
                "dbo.AccountKeys",
                c => new
                    {
                        AccountKeyID = c.Int(nullable: false, identity: true),
                        Key = c.Binary(),
                        KeyCreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountKeyID);
            
            CreateTable(
                "dbo.AccountTokens",
                c => new
                    {
                        AccountTokenID = c.Int(nullable: false),
                        Token = c.String(),
                    })
                .PrimaryKey(t => t.AccountTokenID)
                .ForeignKey("dbo.Accounts", t => t.AccountTokenID)
                .Index(t => t.AccountTokenID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountEmails", "AccountEmailID", "dbo.Accounts");
            DropForeignKey("dbo.AccountTokens", "AccountTokenID", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "Key_AccountKeyID", "dbo.AccountKeys");
            DropForeignKey("dbo.IngredientAmounts", "Account_AccountID", "dbo.Accounts");
            DropForeignKey("dbo.IngredientAmounts", "Ingredient_IngredientID", "dbo.Ingredients");
            DropForeignKey("dbo.Ingredients", "Type_IngredientTypeID", "dbo.IngredientTypes");
            DropForeignKey("dbo.Ingredients", "EnvironmentType_EnvironmentTypeID", "dbo.EnvironmentTypes");
            DropForeignKey("dbo.Explorers", "Account_AccountID", "dbo.Accounts");
            DropForeignKey("dbo.Explorers", "Type_ExplorerTypeID", "dbo.ExplorerTypes");
            DropForeignKey("dbo.Explorers", "Expedition_ExpeditionID", "dbo.Expeditions");
            DropForeignKey("dbo.Expeditions", "Environment_EnvironmentID", "dbo.Environments");
            DropForeignKey("dbo.Environments", "Type_EnvironmentTypeID", "dbo.EnvironmentTypes");
            DropForeignKey("dbo.Environments", "Difficulty_EnvironmentDifficultyID", "dbo.EnvironmentDifficulties");
            DropIndex("dbo.AccountTokens", new[] { "AccountTokenID" });
            DropIndex("dbo.Ingredients", new[] { "Type_IngredientTypeID" });
            DropIndex("dbo.Ingredients", new[] { "EnvironmentType_EnvironmentTypeID" });
            DropIndex("dbo.IngredientAmounts", new[] { "Account_AccountID" });
            DropIndex("dbo.IngredientAmounts", new[] { "Ingredient_IngredientID" });
            DropIndex("dbo.Environments", new[] { "Type_EnvironmentTypeID" });
            DropIndex("dbo.Environments", new[] { "Difficulty_EnvironmentDifficultyID" });
            DropIndex("dbo.Expeditions", new[] { "Environment_EnvironmentID" });
            DropIndex("dbo.Explorers", new[] { "Account_AccountID" });
            DropIndex("dbo.Explorers", new[] { "Type_ExplorerTypeID" });
            DropIndex("dbo.Explorers", new[] { "Expedition_ExpeditionID" });
            DropIndex("dbo.Accounts", new[] { "Key_AccountKeyID" });
            DropIndex("dbo.Accounts", new[] { "DisplayName" });
            DropIndex("dbo.AccountEmails", new[] { "EmailAddress" });
            DropIndex("dbo.AccountEmails", new[] { "AccountEmailID" });
            DropTable("dbo.AccountTokens");
            DropTable("dbo.AccountKeys");
            DropTable("dbo.IngredientTypes");
            DropTable("dbo.Ingredients");
            DropTable("dbo.IngredientAmounts");
            DropTable("dbo.ExplorerTypes");
            DropTable("dbo.EnvironmentTypes");
            DropTable("dbo.EnvironmentDifficulties");
            DropTable("dbo.Environments");
            DropTable("dbo.Expeditions");
            DropTable("dbo.Explorers");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountEmails");
        }
    }
}
