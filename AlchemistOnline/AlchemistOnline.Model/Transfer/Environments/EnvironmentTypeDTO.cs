using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.Model.Transfer.Environments
{
    public class EnvironmentTypeDTO
    {
        public int EnvironmentTypeID { get; set; }
        public string Name { get; set; }
        public string ColourHex { get; set; }
        public string ImagePath { get; set; }
    }
}
