using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class Explorer
    {
        public int ExplorerID { get; set; }
        public string Name { get; set; }
        public double ExperiencePoints { get; set; }

        public int? AccountID { get; set; }
        public Account Account { get; set; }
        public int ExplorerTypeID { get; set; }
        public ExplorerType ExplorerType { get; set; }

        public Expedition Expedition { get; set; }
    }
}
