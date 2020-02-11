using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class AccountToken
    {
        [Key, ForeignKey("Account")]
        public int AccountTokenID { get; set; }
        public string Token { get; set; }

        public virtual Account Account { get; set; }
    }
}
