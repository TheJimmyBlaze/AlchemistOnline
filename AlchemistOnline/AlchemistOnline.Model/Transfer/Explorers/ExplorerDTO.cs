﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Transfer.Explorers
{
    public class ExplorerDTO
    {
        public int ExplorerID { get; set; }
        public string Name { get; set; }
        public double Level { get; set; }
        public double ExperiencePoints { get; set; }

        public int? AccountID { get; set; }
        public int ExplorerTypeID { get; set; }

        public ExpeditionDTO Expedition { get; set; }
    }
}
