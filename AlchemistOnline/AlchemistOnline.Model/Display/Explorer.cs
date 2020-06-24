using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AlchemistOnline.Model.Display
{
    public class Explorer
    {
        public int ExplorerID { get; set; }
        public string Name { get; set; }
        public double Level { get; set; }
        public double ExperiencePoints { get; set; }

        public int? AccountID { get; set; }
        public int ExplorerTypeID { get; set; }

        //TODO: Add expedition
    }
}
