using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Transfer.Accounts
{
    public class AccountDTO
    {
        public int AccountID { get; set; }
        public string DisplayName { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public DateTime LastOnline { get; set; }
    }
}
