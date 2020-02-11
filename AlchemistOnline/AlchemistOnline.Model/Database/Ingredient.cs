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

        public virtual IngredientType Type { get; set; }
        public virtual EnvironmentType EnvironmentType { get; set; }
    }
}
