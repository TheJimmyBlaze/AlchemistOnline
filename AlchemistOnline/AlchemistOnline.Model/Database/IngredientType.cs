using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class IngredientType
    {
        public int IngredientTypeID { get; set; }
        public string Name { get; set; }
        public string ColourHex { get; set; }
        public string ImagePath { get; set; }
    }
}
