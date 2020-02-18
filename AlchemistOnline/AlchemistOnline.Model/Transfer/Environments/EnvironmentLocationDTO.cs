using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Transfer.Environments
{
    public class EnvironmentLocationDTO
    {
        public int EnvironmentLocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int ExpeditionSeconds { get; set; }

        public int EnvironmentTypeID { get; set; }
        public int EnvironmentDifficultyID { get; set; }
    }
}
