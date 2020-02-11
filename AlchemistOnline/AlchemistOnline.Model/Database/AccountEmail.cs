using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class AccountEmail
    {
        [Key, ForeignKey("Account")]
        public int AccountEmailID { get; set; }
        [MaxLength(256)]
        [Index(IsUnique = true)]
        public string EmailAddress { get; set; }

        public virtual Account Account { get; set; }
    }
}
