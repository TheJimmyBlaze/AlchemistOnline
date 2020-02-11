using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Transfer
{
    public class DisplayAccount
    {
        public int AccountID { get; set; }
        public string DisplayName { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public DateTime LastOnline { get; set; }
    }
}
