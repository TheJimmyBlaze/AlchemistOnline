using AlchemistOnline.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Context
{
    public class EnvironmentTypes : IDataGenerator
    {
        public const int SWAMP = 1;
        public const int FOREST = 2;
        public const int JUNGLE = 3;
        public const int MOUNTAIN = 4;

        private readonly List<EnvironmentType> definitions = new List<EnvironmentType>
        {
            new EnvironmentType
            {
                EnvironmentTypeID = SWAMP,
                Name = "Swamp",
                ColourHex = "#cddc39",
                ImagePath = "Swamp.png"
            },
            new EnvironmentType
            {
                EnvironmentTypeID = FOREST,
                Name = "Forest",
                ColourHex = "#795548",
                ImagePath = "Forest.png"
            },
            new EnvironmentType
            {
                EnvironmentTypeID = JUNGLE,
                Name = "Jungle",
                ColourHex = "#4caf50",
                ImagePath = "Jungle.png"
            },
            new EnvironmentType
            {
                EnvironmentTypeID = MOUNTAIN,
                Name = "Mountain",
                ColourHex = "#9e9e9e",
                ImagePath = "Mountain.png"
            }
        };

        public void Generate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnvironmentType>().HasData(definitions);
        }
    }
}
