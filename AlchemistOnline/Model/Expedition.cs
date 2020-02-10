using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model
{
    public class Expedition
    {
        public int ExpeditionID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectedReturnTime { get; set; }

        public Environment Environment { get; set; }
    }
}
