using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class AccountKey
    {
        public int AccountKeyID { get; set; }
        public byte[] Key { get; set; }
        public DateTime KeyCreationDate { get; set; }

        public int AccountID { get; set; }
        public Account Account { get; set; }
    }
}
