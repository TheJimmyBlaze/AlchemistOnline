using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class Explorer
    {
        public int ExplorerID { get; set; }
        public string Name { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }

        public virtual ExplorerType Type { get; set; }
        public virtual Expedition Expedition { get; set; }
    }
}
