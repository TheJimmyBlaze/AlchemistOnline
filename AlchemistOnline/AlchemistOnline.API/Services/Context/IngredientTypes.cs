using AlchemistOnline.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Context
{
    public class IngredientTypes : IDataGenerator
    {
        public const int FLOWER = 1;
        public const int LEAF = 2;
        public const int STEM = 3;
        public const int SAP = 4;

        private static readonly List<IngredientType> definitions = new List<IngredientType>
        {
            new IngredientType
            {
                IngredientTypeID = FLOWER,
                Name = "Flower",
                ColourHex = "#f44336",
                ImagePath = "Flower.png"
            },
            new IngredientType
            {
                IngredientTypeID = LEAF,
                Name = "Leaf",
                ColourHex = "#8bc34a",
                ImagePath = "Leaf.png"
            },
            new IngredientType
            {
                IngredientTypeID = STEM,
                Name = "Stem",
                ColourHex = "#795548",
                ImagePath = "Stem.png"
            },
            new IngredientType
            {
                IngredientTypeID = SAP,
                Name = "Sap",
                ColourHex = "#ffc107",
                ImagePath = "Sap.png"
            }
        };

        public void Generate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientType>().HasData(definitions);
        }
    }
}
