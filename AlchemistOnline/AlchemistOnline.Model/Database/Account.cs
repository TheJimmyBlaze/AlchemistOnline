using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class Account
    {
        public int AccountID { get; set; }
        public string DisplayName { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public DateTime LastOnline { get; set; }

        public AccountEmail Email { get; set; }
        public AccountKey Key { get; set; }

        public ICollection<Explorer> Explorers { get; set; }
        public ICollection<IngredientAmount> Ingredients { get; set; }
    }
}
