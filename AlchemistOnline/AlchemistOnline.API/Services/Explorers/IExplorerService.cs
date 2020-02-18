using AlchemistOnline.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Explorers
{
    public interface IExplorerService
    {
        public Explorer GenerateExplorer();

        public bool OwnsExplorer(IIdentity identity, int explorerID);

        public double CalculateLevel(double xp);
    }
}
