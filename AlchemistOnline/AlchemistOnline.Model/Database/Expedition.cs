using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class Expedition
    {
        public int ExpeditionID { get; set; }
        public DateTime DepartureTime { get; set; }

        public int ExplorerID { get; set; }
        public Explorer Explorer { get; set; }
        public int EnvironmentLocationID { get; set; }
        public EnvironmentLocation EnvironmentLocation { get; set; }
    }
}
