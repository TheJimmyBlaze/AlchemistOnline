create database Alchemist
go

use Alchemist
go


/** An Account is a record of a single user's credentials used to access Alchemist Online. **/
create table Account (
	AccountID uniqueidentifier not null,
	DisplayName varchar(32) not null,
	EmailAddress varchar(128) not null,
	AccountCreationDate datetime not null,
	lastOnline datetime null,

	constraint PKAccountID primary key (AccountID),

	constraint UniqueAccountDisplayName unique (DisplayName),
	constraint UniqueAccountEmailAddress unique (EmailAddress)
)


/** An AccountKey is a secret key an account holder must provide to verify they are the owner of the corresponding Alchemist Online Account. **/
create table AccountKey (
	AccountKeyID uniqueidentifier not null,
	AccountID uniqueidentifier not null,
	[Key] varchar(2048) not null,
	KeyCreationDate datetime not null,

	constraint PJAccountKeyID primary key (AccountKeyID),
	constraint FKAccountKeyAccountID foreign key (AccountID) references Account (AccountID)
)


/** An Environment Type defines the prominent land feature contained within a given environment. **/
create table EnvironmentType (
	EnvironmentTypeID uniqueidentifier not null,
	[Name] varchar(32) not null,
	ColourHex varchar(32) not null,
	ImagePath varchar(2048) not null,

	constraint PKEnviornmentTypeID primary key (EnvironmentTypeID),

	constraint UniqueEnvironmentTypeName unique ([Name]),
	constraint UniqueEnvironmentTypeColour unique (ColourHex)
)


/** An EnvironmentDifficulty defines the how difficult and Environment is to explore. 
	Difficulty affects time taken to complete the expedition, and the rewards returned. 
	An explorer must meet the minimum required skill level required for a difficulty before it becomes available to them. **/
create table EnvironmentDifficulty (
	EnvironmentDifficultyID uniqueidentifier not null,
	[Name] varchar(32) not null,
	[Description] varchar(256) not null,
	ColourHex varchar(16) not null,
	ImagePath varchar(2048) not null,
	SkillRequirement int not null,
	RewardMultiplier int not null,

	constraint PKEnvironmentDifficultyID primary key (EnvironmentDifficultyID),
	
	constraint UniqueEnvironmentDifficultyName unique ([Name]),
	constraint UniqueEnvironmentDifficultyColour unique (ColourHex)
)


/** An Environment is a location which can be explored to find Ingredients. **/
create table Environment (
	EnvironmentID uniqueidentifier not null,
	EnvironmentTypeID uniqueidentifier not null,
	EnvironmentDifficultyID uniqueidentifier not null,
	[Name] varchar(32) not null,
	[Description] varchar(256),
	ImagePath varchar(2048) not null,
	ExpeditionSeconds int not null, /** The ammount of time an expidition to this environment will take, measured in seconds. **/

	constraint PKEnvironmentID primary key (EnvironmentID),
	constraint FKEnvironmentEnvironmentTypeID foreign key (EnvironmentTypeID) references EnvironmentType (EnvironmentTypeID),
	constraint FKEnvironmentEnvironmentDifficultyID foreign key (EnvironmentDifficultyID) references EnvironmentDifficulty (EnvironmentDifficultyID),

	constraint UniqueEnvironmentName unique ([Name])
)

/** An Ingredient Type defines the origin of where an ingredient has been collected from. **/
create table IngredientType (
	IngredientTypeID uniqueidentifier not null,
	[Name] varchar(32) not null, 
	ColourHex varchar(16) not null,
	ImagePath varchar(2048) not null,

	constraint PKIngredientTypeID primary key (IngredientTypeID),

	constraint UniqueIngredientTypeName unique ([Name]),
	constraint UniqueIngredientTypeColour unique (ColourHex)
)


/** And Ingredient is a singular component which when combined with others: creates a potion. **/
create table Ingredient (
	IngredientID uniqueidentifier not null,
	IngredientTypeID uniqueidentifier not null,
	EnvironmentTypeID uniqueidentifier null, /** An Ingredient with a null EnvironmentType can be found in any Environment. **/
	[Name] varchar(32) not null,
	ImagePath varchar(2048) not null,

	constraint PKIngredientID primary key (IngredientID),
	constraint FKIngredientIngredientTypeID foreign key (IngredientTypeID) references IngredientType (IngredientTypeID),
	constraint FKIngredientEnvironmentTypeID foreign key (EnvironmentTypeID) references EnvironmentType (EnvironmentTypeID),

	constraint UniqueIngredientName unique ([Name])
)


/** An ExplorerType defines the appearance of an Explorer, and leaves open the opertunity for later customization of Explorers later on. **/
create table ExplorerType (
	ExplorerTypeID uniqueidentifier not null,
	[Name] varchar(32) not null,
	ColourHex varchar(16) not null,
	ImagePath varchar(2048) not null,

	constraint PKExplorerTypeID primary key (ExplorerTypeID),
	
	constraint UniqueExplorerTypeName unique([Name])
)


/** An Explorer is a unit, belonging to an Account, that can be sent on Expiditions to Environments to retrieve Ingredients. **/
create table Explorer (
	ExplorerID uniqueidentifier not null,
	ExplorerTypeID uniqueidentifier not null,
	AccountID uniqueidentifier not null,
	[Name] varchar(32) not null,
	ExperiencePoints int not null,
	[Level] int not null,

	constraint PKExplorerID primary key (ExplorerID),
	constraint FKExplorerExplorerTypeID foreign key (ExplorerTypeID) references ExplorerType (ExplorerTypeID),
	constraint FKExplorerAccountID foreign key (AccountID) references Account (AccountID)
)


/** An Expidition is a record of an Explorer, sent to explor and Environment. **/
create table Expedition (
	ExpeditionID uniqueidentifier not null,
	ExplorerID uniqueidentifier not null,
	EnvironmentID uniqueidentifier not null,
	DepartureTime datetime not null,
	ExpectedReturnTime datetime not null,

	constraint PKExpeditionID primary key (ExpeditionID),
	constraint FKExpeditionExplorerID foreign key (ExplorerID) references Explorer (ExplorerID),
	constraint FKExpeditionEnvironmentID foreign key (EnvironmentID) references Environment (EnvironmentID),

	constraint UniqueExporerID unique (ExplorerID)
)