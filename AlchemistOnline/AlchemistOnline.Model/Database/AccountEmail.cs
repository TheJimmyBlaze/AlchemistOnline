using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class AccountEmail
    {
        public int AccountEmailID { get; set; }
        public string EmailAddress { get; set; }

        public int AccountID { get; set; }
        public Account Account { get; set; }
    }
}
