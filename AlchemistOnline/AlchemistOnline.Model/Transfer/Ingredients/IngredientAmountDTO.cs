using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Transfer.Ingredients
{
    public class IngredientAmountDTO
    {
        public int IngredientAmountID { get; set; }
        public int Amount { get; set; }

        public int AccountID { get; set; }
        public int IngredientID { get; set; }
    }
}
