using AlchemistOnline.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Explorers
{
    public interface IExplorerService
    {
        public Explorer GenerateExplorer();
    }
}
