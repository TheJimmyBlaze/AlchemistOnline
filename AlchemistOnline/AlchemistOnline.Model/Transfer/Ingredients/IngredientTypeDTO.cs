using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Transfer.Ingredients
{
    public class IngredientTypeDTO
    {
        public int IngredientTypeID { get; set; }
        public string Name { get; set; }
        public string ColourHex { get; set; }
        public string ImagePath { get; set; }
    }
}
