using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class IngredientAmount
    {
        public int IngredientAmountID { get; set; }
        public int Amount { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
