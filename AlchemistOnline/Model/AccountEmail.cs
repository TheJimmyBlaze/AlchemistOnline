using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlchemistOnline.Model
{
    public class AccountEmail
    {
        public int AccountEmailID { get; set; }
        public string EmailAddress { get; set; }
    }
}
