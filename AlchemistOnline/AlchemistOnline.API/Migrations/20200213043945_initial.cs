using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlchemistOnline.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(nullable: true),
                    AccountCreationDate = table.Column<DateTime>(nullable: false),
                    LastOnline = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentDifficulties",
                columns: table => new
                {
                    EnvironmentDifficultyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ColourHex = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    SkillRequirement = table.Column<int>(nullable: false),
                    RewardMultiplier = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentDifficulties", x => x.EnvironmentDifficultyID);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentTypes",
                columns: table => new
                {
                    EnvironmentTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ColourHex = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentTypes", x => x.EnvironmentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ExplorerTypes",
                columns: table => new
                {
                    ExplorerTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExplorerTypes", x => x.ExplorerTypeID);
                });

            migrationBuilder.CreateTable(
                name: "IngredientTypes",
                columns: table => new
                {
                    IngredientTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ColourHex = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientTypes", x => x.IngredientTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AccountEmails",
                columns: table => new
                {
                    AccountEmailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(nullable: true),
                    AccountID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountEmails", x => x.AccountEmailID);
                    table.ForeignKey(
                        name: "FK_AccountEmails_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountKeys",
                columns: table => new
                {
                    AccountKeyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<byte[]>(nullable: true),
                    KeyCreationDate = table.Column<DateTime>(nullable: false),
                    AccountID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountKeys", x => x.AccountKeyID);
                    table.ForeignKey(
                        name: "FK_AccountKeys_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentLocations",
                columns: table => new
                {
                    EnvironmentLocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    ExpeditionSeconds = table.Column<int>(nullable: false),
                    EnvironmentTypeID = table.Column<int>(nullable: false),
                    EnvironmentDifficultyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentLocations", x => x.EnvironmentLocationID);
                    table.ForeignKey(
                        name: "FK_EnvironmentLocations_EnvironmentDifficulties_EnvironmentDifficultyID",
                        column: x => x.EnvironmentDifficultyID,
                        principalTable: "EnvironmentDifficulties",
                        principalColumn: "EnvironmentDifficultyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnvironmentLocations_EnvironmentTypes_EnvironmentTypeID",
                        column: x => x.EnvironmentTypeID,
                        principalTable: "EnvironmentTypes",
                        principalColumn: "EnvironmentTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Explorers",
                columns: table => new
                {
                    ExplorerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ExperiencePoints = table.Column<double>(nullable: false),
                    AccountID = table.Column<int>(nullable: true),
                    ExplorerTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Explorers", x => x.ExplorerID);
                    table.ForeignKey(
                        name: "FK_Explorers_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Explorers_ExplorerTypes_ExplorerTypeID",
                        column: x => x.ExplorerTypeID,
                        principalTable: "ExplorerTypes",
                        principalColumn: "ExplorerTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    IngredientTypeID = table.Column<int>(nullable: false),
                    EnvironmentTypeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientID);
                    table.ForeignKey(
                        name: "FK_Ingredients_EnvironmentTypes_EnvironmentTypeID",
                        column: x => x.EnvironmentTypeID,
                        principalTable: "EnvironmentTypes",
                        principalColumn: "EnvironmentTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingredients_IngredientTypes_IngredientTypeID",
                        column: x => x.IngredientTypeID,
                        principalTable: "IngredientTypes",
                        principalColumn: "IngredientTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expeditions",
                columns: table => new
                {
                    ExpeditionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    ExplorerID = table.Column<int>(nullable: false),
                    EnvironmentLocationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expeditions", x => x.ExpeditionID);
                    table.ForeignKey(
                        name: "FK_Expeditions_EnvironmentLocations_EnvironmentLocationID",
                        column: x => x.EnvironmentLocationID,
                        principalTable: "EnvironmentLocations",
                        principalColumn: "EnvironmentLocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expeditions_Explorers_ExplorerID",
                        column: x => x.ExplorerID,
                        principalTable: "Explorers",
                        principalColumn: "ExplorerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientAmounts",
                columns: table => new
                {
                    IngredientAmountID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(nullable: false),
                    AccountID = table.Column<int>(nullable: false),
                    IngredientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientAmounts", x => x.IngredientAmountID);
                    table.ForeignKey(
                        name: "FK_IngredientAmounts_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientAmounts_Ingredients_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EnvironmentDifficulties",
                columns: new[] { "EnvironmentDifficultyID", "ColourHex", "Description", "ImagePath", "Name", "RewardMultiplier", "SkillRequirement" },
                values: new object[,]
                {
                    { 1, "#03a9f4", "A simple drudge, not very rewarding.", "Mundane.png", "Mundane", 1, 0 },
                    { 2, "#2196f3", "A short jaunt, there may be ingredients arround.", "Simple.png", "Simple", 2, 1 },
                    { 3, "#3f51b5", "Theres a few ingredients, and the treck requires some experience.", "Manageable.png", "Manageable", 3, 2 },
                    { 4, "#673ab7", "Overcoming this challenge rewards a handful of Ingredients.", "Challenging.png", "Challenging", 4, 4 },
                    { 5, "#9c27b0", "A difficult trek with harse terrian, the adventurous will be rewarded.", "Dangerous.png", "Dangerous", 5, 6 },
                    { 6, "#e91e63", "A bounty of flora can be found here if you can find an explorer daring enough to undertake the task.", "Perilous.png", "Perilous", 6, 8 }
                });

            migrationBuilder.InsertData(
                table: "EnvironmentTypes",
                columns: new[] { "EnvironmentTypeID", "ColourHex", "ImagePath", "Name" },
                values: new object[,]
                {
                    { 4, "#9e9e9e", "Mountain.png", "Mountain" },
                    { 3, "#4caf50", "Jungle.png", "Jungle" },
                    { 1, "#cddc39", "Swamp.png", "Swamp" },
                    { 2, "#795548", "Forest.png", "Forest" }
                });

            migrationBuilder.InsertData(
                table: "ExplorerTypes",
                columns: new[] { "ExplorerTypeID", "ImagePath", "Name" },
                values: new object[,]
                {
                    { 1, "Cartographer.png", "Cartographer" },
                    { 2, "Tracker.png", "Tracker" },
                    { 3, "Hunter.png", "Hunter" }
                });

            migrationBuilder.InsertData(
                table: "IngredientTypes",
                columns: new[] { "IngredientTypeID", "ColourHex", "ImagePath", "Name" },
                values: new object[,]
                {
                    { 3, "#795548", "Stem.png", "Stem" },
                    { 1, "#f44336", "Flower.png", "Flower" },
                    { 2, "#8bc34a", "Leaf.png", "Leaf" },
                    { 4, "#ffc107", "Sap.png", "Sap" }
                });

            migrationBuilder.InsertData(
                table: "EnvironmentLocations",
                columns: new[] { "EnvironmentLocationID", "Description", "EnvironmentDifficultyID", "EnvironmentTypeID", "ExpeditionSeconds", "ImagePath", "Name" },
                values: new object[,]
                {
                    { 1, "The jewel of an ancient civilization swallowed by the fetid maw of a seemingly endless sawmp.", 1, 1, 120, "SunkenCitiesBog.png", "Sunken Cities Bog" },
                    { 2, "Enourmous towering Pine trees, the ground blanketed with a dense thicket of bristling greenery.", 1, 2, 135, "GreatPinesForest.png", "Great Pines Forest" },
                    { 3, "A dark jungle nestled in a deep valley, the foliage so densly packed no light touches the undergrowth", 3, 3, 300, "CloakValleyWilderness.png", "Cloak Valley Wilderness" },
                    { 4, "For years an undending lightning storm has swirled arround the peaks of the Storm Crown", 4, 4, 480, "StormCrownPeaks.png", "Storm Crown Peaks" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientID", "EnvironmentTypeID", "ImagePath", "IngredientTypeID", "Name" },
                values: new object[,]
                {
                    { 3, 3, "NightBlossom.png", 1, "Night Blossom" },
                    { 4, 4, "LightningFern.png", 2, "Lightning Fern" },
                    { 1, 1, "BogRoot.png", 3, "Bog Root" },
                    { 2, 2, "PineSap.png", 4, "Pine Sap" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountEmails_AccountID",
                table: "AccountEmails",
                column: "AccountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountEmails_EmailAddress",
                table: "AccountEmails",
                column: "EmailAddress",
                unique: true,
                filter: "[EmailAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountKeys_AccountID",
                table: "AccountKeys",
                column: "AccountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_DisplayName",
                table: "Accounts",
                column: "DisplayName",
                unique: true,
                filter: "[DisplayName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentLocations_EnvironmentDifficultyID",
                table: "EnvironmentLocations",
                column: "EnvironmentDifficultyID");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentLocations_EnvironmentTypeID",
                table: "EnvironmentLocations",
                column: "EnvironmentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Expeditions_EnvironmentLocationID",
                table: "Expeditions",
                column: "EnvironmentLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Expeditions_ExplorerID",
                table: "Expeditions",
                column: "ExplorerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Explorers_AccountID",
                table: "Explorers",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Explorers_ExplorerTypeID",
                table: "Explorers",
                column: "ExplorerTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientAmounts_AccountID",
                table: "IngredientAmounts",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientAmounts_IngredientID",
                table: "IngredientAmounts",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_EnvironmentTypeID",
                table: "Ingredients",
                column: "EnvironmentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientTypeID",
                table: "Ingredients",
                column: "IngredientTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountEmails");

            migrationBuilder.DropTable(
                name: "AccountKeys");

            migrationBuilder.DropTable(
                name: "Expeditions");

            migrationBuilder.DropTable(
                name: "IngredientAmounts");

            migrationBuilder.DropTable(
                name: "EnvironmentLocations");

            migrationBuilder.DropTable(
                name: "Explorers");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "EnvironmentDifficulties");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "ExplorerTypes");

            migrationBuilder.DropTable(
                name: "EnvironmentTypes");

            migrationBuilder.DropTable(
                name: "IngredientTypes");
        }
    }
}
