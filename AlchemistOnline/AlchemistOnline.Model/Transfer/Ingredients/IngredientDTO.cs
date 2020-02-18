using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Transfer.Ingredients
{
    public class IngredientDTO
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public int IngredientTypeID { get; set; }
        public int? EnvironmentTypeID { get; set; }
    }
}
