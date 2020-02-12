using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public int IngredientTypeID { get; set; }
        public IngredientType IngredientType { get; set; }
        public int EnvironmentTypeID { get; set; }
        public EnvironmentType EnvironmentType { get; set; }
    }
}
