
/** Environment Difficulties **/
declare @MundaneID uniqueidentifier = NewID(),
	@DangerousID uniqueidentifier = NewID(),
	@PerilousID uniqueidentifier = NewID()

insert into EnvironmentDifficulty
select @MundaneID, 
	'Mundane', 
	'A simple drudge, not very rewarding, but quite safe.', 
	'#3f51b5',
	'Mundane.png',
	0,
	1
union all select @DangerousID,
	'Dangerous',
	'A difficult trek with harsh terrain, the adventerous will be rewarded.',
	'#673ab7',
	'Dangerous.png',
	1,
	2
union all select @PerilousID,
	'Perilous',
	'A bounty of flora can be found here if you can find an explorer daring enough to undertake the task.',
	'#e91e63',
	'Perilous.png',
	2,
	3

 
 /** Environment Types **/
declare @SwampID uniqueidentifier = NewID(),
    @ForestID uniqueidentifier = NewID(),
    @JungleID uniqueidentifier = NewID(),
    @MountainID uniqueidentifier = NewID()
 
insert into EnvironmentType
select @SwampID, 'Swamp', '#cddc39', 'Swamp.png'
union all select @ForestID, 'Forest', '#795548', 'Forest.png'
union all select @JungleID, 'Jungle', '#4caf50', 'Jungle.png'
union all select @MountainID, 'Mountain', '#9e9e9e', 'Mountain.png'


/** Environments **/
declare @SunkenCitiesBogID uniqueidentifier = NewID(),
	@GreatPinesForestID uniqueIdentifier = NewID(),
	@CloakValleyWildernessID uniqueidentifier = NewID(),
	@StormCrownPeaksID uniqueidentifier = NewID()

insert into Environment
select @SunkenCitiesBogID,
	@SwampID,
	@MundaneID,
	'Sunken Cities Bog',
	'The jewel of an ancient civilization swallowed by the fetid maw of a seemingly endless swamp.',
	'SunkenCityBog.png',
	120
union all select @GreatPinesForestID,
	@ForestID,
	@MundaneID,
	'Great Pines Forest',
	'Enourmous towering Pines trees, each surrounded by a dense thicket of bristling greenery.',
	'GreatPinesForest.png',
	135
union all select @CloakValleyWildernessID,
	@JungleID,
	@DangerousID,
	'Cloak Valley Wilderness',
	'A dark jungle nestled in a deep valley, the foliage so densly packed no light touches the undergrowth.',
	'CloakValleyWilderness.png',
	300
union all select @StormCrownPeaksID,
	@MountainID,
	@PerilousID,
	'Storm Crown Peaks',
	'For years an unending lightning storm has swirled arround the peaks of the Storm Crown',
	'StormCrownPeaks.png',
	480
	
 
/** Ingredient Types **/
declare @FlowerID uniqueidentifier = NewID(),
    @LeafID uniqueidentifier = NewID(),
    @StemID uniqueidentifier = NewID(),
    @SapID uniqueidentifier = NewID()
 
insert into IngredientType
select @FlowerID, 'Flower', '#f44336', 'Flower.png'
union all select @LeafID, 'Leaf', '#8bc34a', 'Leaf.png'
union all select @StemID, 'Stem', '#795548', 'Stem.png'
union all select @SapID, 'Sap', '#ffc107', 'Sap.png'

 
 /** Ingredients **/
declare @BogRootID uniqueidentifier = NewID(),
    @PineSapID uniqueidentifier = NewID(),
    @NightBlossomID uniqueidentifier = NewID(),
    @LightningFernID uniqueidentifier = NewID()
 
insert into Ingredient
select @BogRootID, @StemID, @SwampID, 'Bog Root', 'BogRoot.png'
union all select @PineSapID, @SapID, @ForestID, 'Pine Sap', 'PineSap.png'
union all select @NightBlossomID, @FlowerID, @JungleID, 'Night Blossom', 'NightBlossom.png'
union all select @LightningFernID, @LeafID, @MountainID, 'Lightning Fern', 'LightningFern.png'


/** Explorer Types **/
declare @CartographerID uniqueidentifier = NewID(),
	@TrackerID uniqueidentifier = NewID(),
	@HunterID uniqueidentifier = NewID()

insert into ExplorerType
select @CartographerID, 'Cartographer', 'Cartographer.png'	/**Map**/
union all select @TrackerID, 'Tracker', 'Tracker.png'		/**Stick**/
union all select @HunterID, 'Hunter', 'Hunter.png'			/**Dog**/