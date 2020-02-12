using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Transfer.Explorers
{
    public class ExpeditionDTO
    {
        public int ExpeditionID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectedReturnTime { get; set; }

        public int ExplorerID { get; set; }
    }
}
