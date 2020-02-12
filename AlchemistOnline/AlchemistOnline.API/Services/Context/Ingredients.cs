using AlchemistOnline.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Context
{
    public class Ingredients : IDataGenerator
    {
        public const int BOG_ROOT = 1;
        public const int PINE_SAP = 2;
        public const int NIGHT_BLOSSOM = 3;
        public const int LIGHTNING_FERN = 4;

        private readonly List<Ingredient> definitions = new List<Ingredient>
        {
            new Ingredient
            {
                IngredientID = BOG_ROOT,
                IngredientTypeID = IngredientTypes.STEM,
                EnvironmentTypeID = EnvironmentTypes.SWAMP,
                Name = "Bog Root",
                ImagePath = "BogRoot.png"
            },
            new Ingredient
            {
                IngredientID = PINE_SAP,
                IngredientTypeID = IngredientTypes.SAP,
                EnvironmentTypeID = EnvironmentTypes.FOREST,
                Name = "Pine Sap",
                ImagePath = "PineSap.png"
            },
            new Ingredient
            {
                IngredientID = NIGHT_BLOSSOM,
                IngredientTypeID = IngredientTypes.FLOWER,
                EnvironmentTypeID = EnvironmentTypes.JUNGLE,
                Name = "Night Blossom",
                ImagePath = "NightBlossom.png"
            },
            new Ingredient
            {
                IngredientID = LIGHTNING_FERN,
                IngredientTypeID = IngredientTypes.LEAF,
                EnvironmentTypeID = EnvironmentTypes.MOUNTAIN,
                Name = "Lightning Fern",
                ImagePath = "LightningFern.png"
            }
        };

        public void Generate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().HasData(definitions);
        }
    }
}
