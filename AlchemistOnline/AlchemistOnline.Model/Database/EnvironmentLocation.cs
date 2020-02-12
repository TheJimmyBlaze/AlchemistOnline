using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class EnvironmentLocation
    {
        public int EnvironmentLocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int ExpeditionSeconds { get; set; }

        public int EnvironmentTypeID { get; set; }
        public EnvironmentType EnvironmentType { get; set; }
        public int EnvironmentDifficultyID { get; set; }
        public EnvironmentDifficulty EnvironmentDifficulty { get; set; }
    }
}
