using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlchemistOnline.Model
{
    public class Account
    {
        public int AccountID { get; set; }
        public string DisplayName { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public DateTime LastOnline { get; set; }

        public virtual AccountEmail Email { get; set; }
        public virtual AccountKey Key { get; set; }

        public virtual ICollection<Explorer> Explorers { get; set; }
        public virtual ICollection<IngredientAmount> Ingredients { get; set; }
    }
}
