using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlchemistOnline.Model
{
    public class AccountKey
    {
        public int AccountKeyID { get; set; }
        public string Key { get; set; }
        public DateTime KeyCreationDate { get; set; }
    }
}
