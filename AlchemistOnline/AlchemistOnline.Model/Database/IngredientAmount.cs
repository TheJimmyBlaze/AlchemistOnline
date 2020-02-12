using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class IngredientAmount
    {
        public int IngredientAmountID { get; set; }
        public int Amount { get; set; }

        public int AccountID { get; set; }
        public Account Account { get; set; }
        public int IngredientID { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
