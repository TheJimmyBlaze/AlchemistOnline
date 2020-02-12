using AlchemistOnline.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Context
{
    public class EnvironmentDifficulties : IDataGenerator
    {
        public const int MUNDANE = 1;
        public const int SIMPLE = 2;
        public const int MANAGEABLE = 3;
        public const int CHALLENGING = 4;
        public const int DANGEROUS = 5;
        public const int PERILOUS = 6;

        private readonly List<EnvironmentDifficulty> definitions = new List<EnvironmentDifficulty>
        {
            new EnvironmentDifficulty
            {
                EnvironmentDifficultyID = MUNDANE,
                Name = "Mundane",
                Description = "A simple drudge, not very rewarding.",
                ColourHex = "#03a9f4",
                ImagePath = "Mundane.png",
                SkillRequirement = 0,
                RewardMultiplier = 1
            },
            new EnvironmentDifficulty
            {
                EnvironmentDifficultyID = SIMPLE,
                Name = "Simple",
                Description = "A short jaunt, there may be ingredients arround.",
                ColourHex = "#2196f3",
                ImagePath = "Simple.png",
                SkillRequirement = 1,
                RewardMultiplier = 2
            },
            new EnvironmentDifficulty
            {
                EnvironmentDifficultyID = MANAGEABLE,
                Name = "Manageable",
                Description = "Theres a few ingredients, and the treck requires some experience.",
                ColourHex = "#3f51b5",
                ImagePath = "Manageable.png",
                SkillRequirement = 2,
                RewardMultiplier = 3
            },
            new EnvironmentDifficulty
            {
                EnvironmentDifficultyID = CHALLENGING,
                Name = "Challenging",
                Description = "Overcoming this challenge rewards a handful of Ingredients.",
                ColourHex = "#673ab7",
                ImagePath = "Challenging.png",
                SkillRequirement = 4,
                RewardMultiplier = 4
            },
            new EnvironmentDifficulty
            {
                EnvironmentDifficultyID = DANGEROUS,
                Name = "Dangerous",
                Description = "A difficult trek with harse terrian, the adventurous will be rewarded.",
                ColourHex = "#9c27b0",
                ImagePath = "Dangerous.png",
                SkillRequirement = 6,
                RewardMultiplier = 5
            },
            new EnvironmentDifficulty
            {
                EnvironmentDifficultyID = PERILOUS,
                Name = "Perilous",
                Description = "A bounty of flora can be found here if you can find an explorer daring enough to undertake the task.",
                ColourHex = "#e91e63",
                ImagePath = "Perilous.png",
                SkillRequirement = 8,
                RewardMultiplier = 6
            }
        };

        public void Generate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnvironmentDifficulty>().HasData(definitions);
        }
    }
}
