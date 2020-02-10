using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public IngredientType Type { get; set; }
        public EnvironmentType EnvironmentType { get; set; }
    }
}
