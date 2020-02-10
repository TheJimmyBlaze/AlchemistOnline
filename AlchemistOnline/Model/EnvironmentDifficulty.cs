using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model
{
    public class EnvironmentDifficulty
    {
        public int EnvironmentDifficultyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ColourHex { get; set; }
        public string ImagePath { get; set; }
        public int SkillRequirement { get; set; }
        public int RewardMultiplier { get; set; }
    }
}
