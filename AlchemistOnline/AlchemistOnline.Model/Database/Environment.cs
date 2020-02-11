using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlchemistOnline.Model.Database
{
    public class Environment
    {
        public int EnvironmentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int ExpeditionSeconds { get; set; }

        public virtual EnvironmentType Type { get; set; }
        public virtual EnvironmentDifficulty Difficulty { get; set; }
    }
}
