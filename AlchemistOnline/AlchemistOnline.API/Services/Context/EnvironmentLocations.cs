using AlchemistOnline.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Context
{
    public class EnvironmentLocations : IDataGenerator
    {
        public const int SUNKEN_CITIES_BOG = 1;
        public const int PINE_FOREST = 2;
        public const int CLOAK_VALLEY_WILDERNESS = 3;
        public const int STORM_CROWN_PEAKS = 4;

        private readonly List<EnvironmentLocation> definitions = new List<EnvironmentLocation>
        {
            new EnvironmentLocation
            {
                EnvironmentLocationID = SUNKEN_CITIES_BOG,
                EnvironmentTypeID = EnvironmentTypes.SWAMP,
                EnvironmentDifficultyID = EnvironmentDifficulties.MUNDANE,
                Name = "Sunken Cities Bog",
                Description = "The jewel of an ancient civilization swallowed by the fetid maw of a seemingly endless sawmp.",
                ImagePath = "SunkenCitiesBog.png",
                ExpeditionSeconds = 120
            },
            new EnvironmentLocation
            {
                EnvironmentLocationID = PINE_FOREST,
                EnvironmentTypeID = EnvironmentTypes.FOREST,
                EnvironmentDifficultyID = EnvironmentDifficulties.MUNDANE,
                Name = "Great Pines Forest",
                Description = "Enourmous towering Pine trees, the ground blanketed with a dense thicket of bristling greenery.",
                ImagePath = "GreatPinesForest.png",
                ExpeditionSeconds = 135
            },
            new EnvironmentLocation
            {
                EnvironmentLocationID = CLOAK_VALLEY_WILDERNESS,
                EnvironmentTypeID = EnvironmentTypes.JUNGLE,
                EnvironmentDifficultyID = EnvironmentDifficulties.MANAGEABLE,
                Name = "Cloak Valley Wilderness",
                Description = "A dark jungle nestled in a deep valley, the foliage so densly packed no light touches the undergrowth",
                ImagePath = "CloakValleyWilderness.png",
                ExpeditionSeconds = 300
            },
            new EnvironmentLocation
            {
                EnvironmentLocationID = STORM_CROWN_PEAKS,
                EnvironmentTypeID = EnvironmentTypes.MOUNTAIN,
                EnvironmentDifficultyID = EnvironmentDifficulties.CHALLENGING,
                Name = "Storm Crown Peaks",
                Description = "For years an undending lightning storm has swirled arround the peaks of the Storm Crown",
                ImagePath = "StormCrownPeaks.png",
                ExpeditionSeconds = 480
            }
        };

        public void Generate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnvironmentLocation>().HasData(definitions);
        }
    }
}
