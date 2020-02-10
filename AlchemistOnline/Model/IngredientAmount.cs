using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model
{
    public class IngredientAmount
    {
        public int IngredientAmountID { get; set; }
        public int Amount { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
